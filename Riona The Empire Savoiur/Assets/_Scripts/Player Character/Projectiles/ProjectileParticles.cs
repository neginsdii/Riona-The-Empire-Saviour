///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   ProjectileParticles.cs
///   Author            : Geek�s Garage
///   Last Modified     : 2022/04/07
///   Description       : Particle System Controller
///   Revision History  : 3rd ed. Added object pooling for projectiles
///----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParticles : MonoBehaviour
{
    public int projectileDamage;


    private ParticleSystem particleSystem;
    private List<ParticleCollisionEvent> collisionEvents;

    private float timer = 0f;

    public AttackTypes attackType = AttackTypes.FIRE;

    public PlayerCombat playerCombat = null;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    /// <summary>
    /// On Particle collision
    /// </summary>
    /// <param name="other"></param>
    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
          
            if (other.gameObject.name == "Paladin")
            {
                other.gameObject.GetComponent<Paladin_Behaviour>().TakeDamage(projectileDamage, attackType);
                //Debug.Log("Collided with : " + other.gameObject.name);
            }

            if (other.gameObject.name == "Phoenix")
            {
                other.gameObject.GetComponent<FlyingEnemy_Behaviour>().TakeDamage(projectileDamage, attackType);
                //Debug.Log("Collided with : " + other.gameObject.name);
            }

        } // Use other to see if anything specific needs to be added
    }
    
    private void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            timer += Time.deltaTime;

            if (timer > 4f)
            {
                if (playerCombat)
                {
                    playerCombat.EnqueueProjectile(this.gameObject, attackType);
                }

                gameObject.SetActive(false);
                timer = 0;
            }
        }
    }
}

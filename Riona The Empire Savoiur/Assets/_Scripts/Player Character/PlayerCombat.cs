///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   PlayerCombat.cs
///   Author            : Geek's Garage
///   Last Modified     : 2022/04/07
///   Description       : Player Combat Script
///   Revision History  : 5th ed. Optimization by object pooling
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    // Projectile Spawn Point
    [SerializeField] 
    private Transform projectileSpawnLocation;

    public Transform inaccessibleLocation;

    [SerializeField]
    private GameObject Projectile0_Fire;

    [SerializeField]
    private GameObject Projectile1_Ice;

    [SerializeField]
    private GameObject Projectile2_Lightning;

    private CameraController cameraController;

    public AttackTypes projectileType = AttackTypes.FIRE;

    private Queue<GameObject> FireProjectiles;
    private Queue<GameObject> IceProjectiles;
    private Queue<GameObject> LightningProjectiles;

    private List<GameObject> ActiveProjectiles;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = GetComponent<CameraController>();

        // Projectiles
        FireProjectiles = new Queue<GameObject>();
        IceProjectiles = new Queue<GameObject>();
        LightningProjectiles = new Queue<GameObject>();
        ActiveProjectiles = new List<GameObject>();

        StartProjectiles();
    }

    /// <summary>
    /// Create Projectiles on the location given
    /// </summary>
    private void StartProjectiles()
    {
        for (int i = 0; i < 10; i++)
        {
            FireProjectiles.Enqueue(Instantiate(Projectile0_Fire, inaccessibleLocation));
        }

        for (int i = 0; i < 10; i++)
        {
            IceProjectiles.Enqueue(Instantiate(Projectile1_Ice, inaccessibleLocation));
        }

        for (int i = 0; i < 10; i++)
        {
            LightningProjectiles.Enqueue(Instantiate(Projectile2_Lightning, inaccessibleLocation));
        }
    }

    private void Attack(Vector3 angle, AttackTypes type)
    {
        // Instantiate object
        GameObject bullet = null;

        switch (type)
        {
            case AttackTypes.FIRE:
                bullet = FireProjectiles.Dequeue();
                break;

            case AttackTypes.ICE:
                bullet = IceProjectiles.Dequeue();
                break;

            case AttackTypes.LIGHTNING:
                bullet = LightningProjectiles.Dequeue();
                break;

            default:
                bullet = FireProjectiles.Dequeue();
                break;

        }


        bullet.transform.position = projectileSpawnLocation.position;
        bullet.transform.rotation = Quaternion.Euler(angle);
        bullet.SetActive(true);
        bullet.GetComponent<ProjectileParticles>().attackType = AttackTypes.FIRE;
        bullet.GetComponent<ProjectileParticles>().playerCombat = this;
        ActiveProjectiles.Add(bullet);
    }


    /// <summary>
    /// Dequeue if in active
    /// </summary>
    public void EnqueueProjectile(GameObject projectile, AttackTypes type)
    {

        switch (type)
        {
            case AttackTypes.FIRE:
                FireProjectiles.Enqueue(projectile);
                break;

            case AttackTypes.ICE:
                IceProjectiles.Enqueue(projectile);
                break;

            case AttackTypes.LIGHTNING:
                IceProjectiles.Enqueue(projectile);
                break;

        }

    }


    // Update is called once per frame
    void Update()
    {
        projectileSpawnLocation.rotation = transform.rotation;
    }

    /// <summary>
    /// Spawn Projectile
    /// </summary>
    void SpawnProjectile()
    {
        // Adjust rotation
        Vector3 bulletRotation = projectileSpawnLocation.rotation.eulerAngles + new Vector3(cameraController.mouseY * cameraController.invertY, 0f, 0f);

        // Instantiate object
        GameObject bullet;

        // of Type
        switch (projectileType)
        {

            case AttackTypes.FIRE:
                Attack(bulletRotation, AttackTypes.FIRE);
                break;

            case AttackTypes.ICE:
                bullet = Instantiate(Projectile1_Ice, projectileSpawnLocation.position, Quaternion.Euler(bulletRotation));
                bullet.GetComponent<ProjectileParticles>().attackType = AttackTypes.ICE;
                break;

            case AttackTypes.LIGHTNING:
                bullet = Instantiate(Projectile2_Lightning, projectileSpawnLocation.position, Quaternion.Euler(bulletRotation));
                bullet.GetComponent<ProjectileParticles>().attackType = AttackTypes.LIGHTNING;
                break;

            default:
                bullet = Instantiate(Projectile0_Fire, projectileSpawnLocation.position, Quaternion.Euler(bulletRotation));
                bullet.GetComponent<ProjectileParticles>().attackType = AttackTypes.FIRE;
                break;
        }
        
    }
}

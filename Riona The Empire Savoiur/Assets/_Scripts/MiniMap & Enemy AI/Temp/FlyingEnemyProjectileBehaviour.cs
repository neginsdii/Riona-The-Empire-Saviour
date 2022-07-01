//----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   FlyingEnemyProjectileBehaviour.cs
///   Author            : Geek�s Garage
///   Last Modified     : 2022/04/07
///   Description       : A Ground enemy type, including the animationstates.
///   Revision History  : 2nd ed. Commented out Debug.Log & Removed the Empty Awake function
///----------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyProjectileBehaviour : MonoBehaviour
{
    float liveTime = 5.0f;
    public GameObject Riona;
    public PlayerBehaviour RionaPlayerBehaviour;
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        Riona = GameObject.Find("Riona").gameObject;
        RionaPlayerBehaviour = Riona.GetComponent<PlayerBehaviour>();
    }
   
    // Update is called once per frame
    void Update()
    {
        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Riona" )
        {
           
            RionaPlayerBehaviour.UpdateHP(-damage);
            //Debug.Log("Collided with" + other.gameObject.name);
            //Destroy(other.gameObject);
        }
    }
}

///-----------------------------
///     Riona the Empire Saviour
///     LevitatingPlatform.cs
///     Author          : Geek's Garage
///     Last Modified   : 2022/2/11
///     Description     : Script for moving the platform along an axis
///     Revision History: 2nd ed. Setting the player parent onTrigger event.-  code optimization
/// ----------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitatingPlatform : MonoBehaviour
{

    [Header("Movement"), SerializeField]
    private PlatformDirection direction;
    [Range(0.1f, 10.0f), SerializeField]
    private float speed;
    [Range(1, 20), SerializeField]
    private float distance;
    [Range(0.05f, 0.1f), SerializeField]
    private float distanceOffset;
    public bool isLooping;

    private Vector3 startingPosition;
    private bool isMoving;
    private float pingPongValue;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();

        if (isLooping)
        {
            isMoving = true;
        }
    }
    private void MovePlatform()
    {
        pingPongValue = (isMoving) ? Mathf.PingPong(Time.time * speed, distance) : distance;

        if ((!isLooping) && (pingPongValue >= distance - distanceOffset))
        {
            isMoving = false;
        }

        switch (direction)
        {
            case PlatformDirection.X_AXIS:
                transform.position = new Vector3(startingPosition.x + pingPongValue, transform.position.y, transform.position.z);
                break;
            case PlatformDirection.Y_AXIS:
                transform.position = new Vector3(transform.position.x , startingPosition.y + pingPongValue, transform.position.z);
                break;
            case PlatformDirection.Z_AXIS:
                transform.position = new Vector3(transform.position.x, transform.position.y , startingPosition.z + pingPongValue);
                break;
            case PlatformDirection.NEGATIVE_X_AXIS:
                transform.position = new Vector3(startingPosition.x - pingPongValue, transform.position.y, transform.position.z);
                break;
            case PlatformDirection.NEGATIVE_Y_AXIS:
                transform.position = new Vector3(transform.position.x, startingPosition.y - pingPongValue, transform.position.z);
                break;
            case PlatformDirection.NEGATIVE_Z_AXIS:
                transform.position = new Vector3(transform.position.x, transform.position.y, startingPosition.z - pingPongValue);
                break;

        }
    }
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
            other.transform.SetParent(this.transform);
            other.GetComponent<PlayerMovement>().IsOnPlatform = true;


        }
    }

	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            other.GetComponent<PlayerMovement>().IsOnPlatform = false;


        }
    }

}

public enum PlatformDirection
{
    X_AXIS,
    Y_AXIS,
    Z_AXIS,
    NEGATIVE_X_AXIS,
    NEGATIVE_Y_AXIS,
    NEGATIVE_Z_AXIS,
    NUM_DIRECTIONS
}

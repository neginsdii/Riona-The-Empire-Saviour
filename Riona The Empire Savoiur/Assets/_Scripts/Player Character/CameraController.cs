///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   CameraController.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/03/10
///   Description       : Camera Controller for both explore and combat mode
///   Revision History  : 7th ed. Implementing touch for mobile devices
///----------------------------------------------------------------------------------


using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Main Camera")] 
    [SerializeField]
    private Transform MainCamera;

    [Header("Virtual Cameras")]
    [SerializeField] 
    private CinemachineVirtualCamera ExploreCamera;

    [SerializeField]
    private CinemachineVirtualCamera CombatCamera;

    [Header("Follow Target Camera Movement")]
    public GameObject followTarget;
    public float mouseSensitivity = 0.5f; 
    public bool invertMouseYAxis = true;
    public float invertY = 1f;

    // combat camera sensitivity
    // To Do: Create a slider from the option menu that will change the sensitivity of the camera
    // then tie that slider value to the mouse sensitivity (multiply slider value to combat camera).

    private float mouseX;
    public float mouseY;
    private float mouseXCombat;

    private PlayerBehaviour _playerBehaviour;
    private PlayerMovement _playerMovement;

    [Header("Combat Vertical Angle Clamp")]
    public float Combat_VerticalAngleClamp = 12f;

    [Header("Attack Joystick")] 
    public Joystick joystick;

    [Header("Touch")] 
    private Touch touch;

    private Vector2 touchDelta;

    private void OnEnable()
    {
        ExploreCamera.Priority = 1;
        CombatCamera.Priority = 0;
    }

    // Using Awake
    void Awake()
    {
        // Get player behaviour component (same game object)
        _playerBehaviour = GetComponent<PlayerBehaviour>();

        // Get Player movement component
        _playerMovement = GetComponent<PlayerMovement>();

        // Set Invert Y Axis
        if (invertMouseYAxis)
        {
            invertY = 1f;
        }
        else
        {
            invertY = -1f;
        }

        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            Data.joystickSensitivity = PlayerPrefs.GetFloat("Sensitivity");
        }

    }

   
    /// <summary>
    /// Using Late Update
    /// </summary>
    void LateUpdate()
    {
        // Camera Look function to control our camera
        // For now using only mouse, will implement touch soon
        // Activate only when in explore mode !
        if (Time.timeScale >= 1)
        {
            if (!_playerBehaviour.isInCombatMode)
            {
                CameraLook();
            }
            else
            {
                //Debug.Log("Resetting...");
                CombatModeCameraControl();
            }
        }
    }

    /// <summary>
    /// Changes camera by taking the combat mode
    /// </summary>
    /// <param name="combatMode"></param>
    public void ChangeCamera(bool combatMode)
    {
        if (combatMode)
        {
            _playerMovement.RotateForCombatMode();
            CombatCamera.Priority = 1;
            ExploreCamera.Priority = 0;
            followTarget.transform.localEulerAngles = Vector3.zero;
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
        }
        else
        {
            CombatCamera.Priority = 0;
            ExploreCamera.Priority = 1;
        }
    }


    /// <summary>
    /// Inverts Y axis if invert function is called
    /// </summary>
    /// <param name="invert"></param>
    public void InvertYAxis(bool invert)
    {
        if (invert)
        {
            invertY = 1f;
        }
        else
        {
            invertY = -1f;
        }
    }


    /// <summary>
    /// Camera Look for Explore mode
    /// </summary>
    public void CameraLook()
    {
        // Touch to rotate
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved && !GetComponent<PlayerMovement>().joystick.isTouchingJoystick)
            {
                touchDelta = touch.deltaPosition;
                touchDelta = touchDelta.normalized;
                mouseX += touchDelta.x * 3;
                mouseY += touchDelta.y * 3;
            }
        }

        if (Application.platform != RuntimePlatform.Android)
        {
            // Store the the input for next frame
            // Get mouse input for X and Y
            mouseX += Input.GetAxisRaw("Mouse X");
            mouseY += Input.GetAxisRaw("Mouse Y");
        }

        // Rotate as per follow target using mouse input X and mouse input Y
        //followTarget.transform.rotation *= Quaternion.AngleAxis(mouseX * mouseSensitivity, Vector3.up);
        //followTarget.transform.rotation *= Quaternion.AngleAxis(mouseY * invertY * mouseSensitivity, Vector3.right);

        // Set the (x,y,0) angles for rotation taking Follow Target gameobject's rotation as we are rotating only the pitch and yaw
        //Vector3 angles = followTarget.transform.localEulerAngles;
        //angles.z = 0;
        //
        //// Angle around X axis, that is looking up and down should be clamped
        //float angleX = followTarget.transform.localEulerAngles.x;

        // Clamp the values between -40 and 40
        if (mouseY > 40f)
        {
            mouseY = 40f;
        }
        else if (mouseY < -40f)
        {
            mouseY = -40f;
        }

        // Set the angle
        followTarget.transform.rotation = Quaternion.Euler(mouseY * invertY, mouseX, 0.0f);
    }

    /// <summary>
    /// Function Take Mouse Input
    /// </summary>
    public void CombatModeCameraControl()
    {
        
        //ExploreCamera.transform.position = movementResetPosition;
        //followTarget.transform.rotation = _playerMovement.currentRotation;

        // Touch to rotate
        //if (Input.touchCount > 0)
        //{
        //    touch = Input.GetTouch(0);
        //    if (touch.phase == TouchPhase.Moved && !GetComponent<PlayerMovement>().joystick.isTouchingJoystick)
        //    {
        //        touchDelta = touch.deltaPosition;
        //        touchDelta = touchDelta.normalized;
        //        mouseX += touchDelta.x;
        //        mouseY += touchDelta.y;
        //        mouseXCombat = touchDelta.x;
        //    }
        //    else
        //    {
        //        mouseXCombat = 0f;
        //    }
        //}

        // Use Joystick to rotate
        mouseX += joystick.Horizontal * Data.joystickSensitivity;
        mouseY += joystick.Vertical * Data.joystickSensitivity;
        mouseXCombat = joystick.Horizontal * Data.joystickSensitivity;

        // Use mouse to rotate
        //mouseX += Input.GetAxisRaw("Mouse X");
        //mouseY += Input.GetAxisRaw("Mouse Y");
        //mouseXCombat = Input.GetAxisRaw("Mouse X");

        if (mouseY >= Combat_VerticalAngleClamp)
        {
            mouseY = Combat_VerticalAngleClamp;
        }
        else if (mouseY <= -Combat_VerticalAngleClamp)
        {
            mouseY = -Combat_VerticalAngleClamp;
        }

        // Set vertical angle only for looking
        followTarget.transform.rotation = Quaternion.Euler(mouseY * invertY, mouseX , 0.0f);

        // adjust player position rotation
        transform.rotation *= Quaternion.Euler(0f, mouseXCombat * mouseSensitivity, 0f); // multiply the slider value here
    }

    /// <summary>
    /// Return Follow Target
    /// </summary>
    /// <returns></returns>
    public Transform GetFollowTarget()
    {
        return followTarget.transform;
    }


    /// <summary>
    /// Return Main Camera transform
    /// </summary>
    /// <returns></returns>
    public Transform GetMainCamera()
    {
        return MainCamera.transform;
    }
}

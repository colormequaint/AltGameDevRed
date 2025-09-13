
                        using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // <summary>
    // Represents the speed at which the player can walk.
    // </summary>
    public float walkSpeed = 5.0f;

    // <summary>
    // Represents the speed at which the player can run.
    // </summary>
    public float runSpeed = 10.0f;

    // <summary>
    // Represents the sensitivity of the player's mouse movement for looking around.
    // </summary>
    public float lookSensitivity = 2.0f;

    // <summary>
    // Represents the angle at which the player can crouch.
    // </summary>
    public float crouchAngle = 45.0f;

    // <summary>
    // Represents the field of view when the player zooms in.
    // </summary>
    public float zoomFOV = 30.0f;

    private CharacterController characterController;
    private Camera playerCamera;
    private bool isCrouching = false;
    private bool isZoomed = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        // Movement controls
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move * Time.deltaTime);

        // Look around controls
        float lookX = Input.GetAxis("Mouse X") * lookSensitivity;
        float lookY = Input.GetAxis("Mouse Y") * lookSensitivity;

        transform.Rotate(Vector3.up * lookX);

        // Crouch control
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
            float targetAngle = isCrouching ? crouchAngle : 0.0f;
            characterController.height = isCrouching ? 1.0f : 2.0f;
            transform.position += Vector3.up * (isCrouching ? -0.5f : 0.5f);
            transform.eulerAngles = new Vector3(targetAngle, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        // Zoom control
        if (Input.GetMouseButtonDown(1))
        {
            isZoomed = !isZoomed;
            playerCamera.fieldOfView = isZoomed ? zoomFOV : 60.0f;
        }
    }
}
                    

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

// public class PlayerParams
// {
//     static public float runSpeed = 3.0f;
//     static public float dodgeForce = 350.0f;
// 
//     static public uint dodgeCooldownMax = 240;
//     static public float immunityTimer;
// 
//     public int currentAmmo = 30;
//     public int maxAmmo = 30;
// 
//     public enum moveState
//     {
//         Idle = 0,
//         Walking,
//         Dodging,
//         Dying
//     };
// 
// } 

public class PlayerController : MonoBehaviour
{
    public GameObject cam;
    public Cinemachine.CinemachineVirtualCamera virtualCam;

    private Rigidbody rb;

    //Physics variables
    public float MaxVel = 5;

    public float rotationSpeed = 500;
    public float lookSpeed = 150;
    public float moveSpeed = 10;
    public float jumpForce = 10;
    public float wallRunUpForce = 400;

    private float TargetPitch = 0;
    public float strafeMod = 0.2f;

    public float raycastDistance;
    public float raycastDistanceFeet;

    public int moveState = 0;

    //Vibes
    public float FOVBase = 60;
    public float FOVMod = 10;
    public float FOVMax = 120;
    public float DutchMod = 0.3f;

    public float minAngleToWall = 45;

    private bool canJump = true;
    private int wallJumpCount = 0;
    public int maxWallJumps = 1;
    public float wallJumpForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector3 input = GetInput();

        Vector3 forwardForce = transform.forward * input.z * moveSpeed;
        Vector3 sidewaysForce = transform.right * input.x * moveSpeed;


        switch (moveState)
        {
            case 0: // OnGround
                {
                    // Apply the run force to the rigidbody
                    if (rb.velocity.magnitude < MaxVel)
                    {
                         rb.AddForce(forwardForce + sidewaysForce * Time.deltaTime);
                    }

                    if (canJump && moveState == 0 && input.y > 0)
                    {
                        rb.AddForce(new Vector3(0, input.y, 0));
                        moveState = 1;
                        //DoJump(input.y);
                    }

                    break;
                }

            case 1: // InAir
                {
                    // Get the direction of the rigidbody's movement
                    Vector3 movingDirection = rb.velocity.normalized;

                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistanceFeet))
                    {
                        moveState = 0; // on ground
                    }


                    // WallRun Crap

                    // if (Physics.Raycast(transform.position, movingDirection, out hit, raycastDistance))
                    // {
                    //     // Check if the hit object is a wall
                    //     if (hit.collider.CompareTag("Wall"))
                    //     {
                    //         // Calculate the rotation to align with the wall normal
                    //         Quaternion rotationToWall = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    // 
                    //         // Calculate the angle between the character's forward direction and the wall normal
                    //         float angleToWall = Vector3.Angle(transform.forward, hit.normal);
                    // 
                    //         // Set a threshold angle (adjust as needed)
                    //         float thresholdAngle = 45f;
                    // 
                    //         // Check if the angle to the wall is within the threshold
                    //         if (angleToWall < thresholdAngle)
                    //         {
                    //             // Apply the rotation around the Y-axis (up vector)
                    //             transform.rotation = rotationToWall;
                    // 
                    //             // Ensure that the Z-axis remains at 0
                    //             transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                    //         }
                    // 
                    //         // Apply upward force to skate along an arc
                    //         Vector3 upwardForce = transform.up * wallRunUpForce * Time.deltaTime;
                    //         rb.AddForce(upwardForce);
                    // 
                    //         // Apply forward force to keep moving along the wall
                    //         Vector3 force = transform.forward * moveSpeed * Time.deltaTime;
                    //         rb.AddForce(force);
                    // 
                    //         moveState = 2;
                    //     }
                    // }
                
                    break;
                }
            case 2: // OnWall
            {
                    Debug.Log("onwall");

                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistanceFeet))
                    {
                        moveState = 0;
                    }

                    // Jump away from the wall
                    if (wallJumpCount < maxWallJumps && input.y > 0)
                    {
                        wallJumpCount += 1;
                        Vector3 wallJumpDirection = (transform.up + transform.forward).normalized;
                        rb.AddForce(wallJumpDirection * wallJumpForce, ForceMode.Impulse);
                        moveState = 1;
                    }
                    break;
            }
            case 3: // Dying
            {
                break;
            }
            default:
            {
                print("in default state, dont wanna be");
                break;
            }
        }

        //virtualCam.m_Lens.FieldOfView = FOVBase + (rb.velocity.magnitude * FOVMod);
        //virtualCam.transform.localRotation.x = DutchMod * rb.velocity.x;

        CameraLook();
    }

    void DoJump(float force)
    {
        rb.AddForce(new Vector3(0,force,0));
    }

    //Get forward and backwards Acceleration
    Vector3 GetInput()
    {
        Vector3 inputOutput = Vector3.zero;
        inputOutput.x = Input.GetAxis("Horizontal") * moveSpeed;
        inputOutput.z = Input.GetAxis("Vertical") * moveSpeed;
        inputOutput.y = Input.GetAxis("Jump") * jumpForce;
        return inputOutput;
    }

    //get the look rotation, and seperately rotate the character for drifting
    void CameraLook()
    {
        float InputX = Input.GetAxis("Mouse X");
        float InputY = Input.GetAxis("Mouse Y");

        // Update Cinemachine camera target pitch
        TargetPitch -= InputY * rotationSpeed * Time.deltaTime;
        // clamp our pitch rotation
        TargetPitch = ClampAngle(TargetPitch, -89, 89);

        cam.transform.localRotation = Quaternion.Euler(TargetPitch, 0.0f, 0.0f);
        
        float lookVelocity = InputX * lookSpeed * Time.deltaTime;
        cam.transform.Rotate(Vector3.up * lookVelocity);

        float rotationVelocity = InputX * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotationVelocity);
    }


    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

}

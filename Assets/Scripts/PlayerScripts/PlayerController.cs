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
    //public Cinemachine.CinemachineVirtualCamera virtualCam;

    private Rigidbody rb;

    //Physics variables
    public float MaxVel = 5;

    public float rotationSpeed = 500;
    public float lookSpeed = 150;
    public float moveSpeed = 10;
    public float strafeSpeed = 10;
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

        Vector3 forwardForce = transform.forward * input.z * moveSpeed * Time.deltaTime * 60f;
        Vector3 sidewaysForce = transform.right * input.x * strafeSpeed * Time.deltaTime * 60f;

        switch (moveState)
        {
            case 0: // OnGround
                {
                    // Apply the run force to the rigidbody
                    if (rb.velocity.magnitude < MaxVel)
                    {
                        rb.AddForce(forwardForce+sidewaysForce);
                    }

                    if (canJump && moveState == 0 && input.y > 0)
                    {
                        Debug.Log("He can ball");
                        rb.AddForce(new Vector3(0, input.y, 0));
                        moveState = 1;
                        DoJump(input.y * jumpForce);
                    }

                    break;
                }

            case 1: // InAir
                {
                    // Get the direction of the rigidbody's movement
                    Vector3 movingDirection = rb.velocity.normalized;

                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistanceFeet) && movingDirection.y <= 0f)
                    {
                        moveState = 0; // on ground
                    }

                    Vector3 perpendicularMove = new Vector3(movingDirection.x * -1, 0f, movingDirection.z);

                    if (Physics.Raycast(transform.position+Vector3.up, perpendicularMove, out hit, raycastDistance))
                    {
                    //     // Check if the hit object is a wall
                        WallRunCheck(hit);
                    }
                    if (Physics.Raycast(transform.position+Vector3.up, -perpendicularMove, out hit, raycastDistance))
                    {
                    //     // Check if the hit object is a wall
                        WallRunCheck(hit);
                    }

                
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

    void WallRunCheck(RaycastHit hit)
    {
        Debug.Log("Wall Runnable");
    }

    void DoJump(float force)
    {
        rb.AddForce(new Vector3(0,force,0));
    }

    //Get forward and backwards Acceleration
    Vector3 GetInput()
    {
        Vector3 inputOutput = Vector3.zero;
        inputOutput.x = Input.GetAxis("Horizontal");
        inputOutput.z = Mathf.Clamp(Input.GetAxis("Vertical"),-0.4f,1f);
        inputOutput.y = Input.GetAxis("Jump");
        return inputOutput;
    }

    //get the look rotation, and seperately rotate the character for drifting
    void CameraLook()
    {
        float InputX = Input.GetAxis("Mouse X");
        float InputY = Input.GetAxis("Mouse Y");

        float lookVelocity = InputY;// * lookSpeed * Time.deltaTime;
        cam.transform.eulerAngles = cam.transform.eulerAngles - (Vector3.right * lookVelocity);

        float rotationVelocity = InputX;// * rotationSpeed;
        transform.eulerAngles = transform.eulerAngles + Vector3.up * rotationVelocity;
    }


    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }


    public void fn_ZeroRigidBodyMomentum()
    {
        rb.velocity = Vector3.zero;
    }
}

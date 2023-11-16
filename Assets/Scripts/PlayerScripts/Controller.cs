using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour //interface MonoBehavior provides methods Start() Update() OnCollisionEnter() OnCollisionExit()
{
    //Fields marked as public are to be changed in the inspector
    public float speed = 5;
    //cms is current move speed and is here so you can control the move speed via script
    private float cms;
    //cs is current sensitivity and is here so you can control the sensitivity via script
    private float cs;
    public bool isGrounded;
    public float sensitivity = 10;
    private Vector2 movementInput;
    private Vector2 localEulerAnglesInput;
    public LayerMask ground;
    public LayerMask wall;
    public float JumpForce = 10f;
    public Transform playerCam;
    private Rigidbody rb;
    public bool isWallRunning = false;
    public float wallRunSpeedBoost = 10;
    public float wallUpwardForce = 10;
    public float wallJumpAwayForce = 50f;
    private float camRot;
    //called on start
    private void Start()
    {
        //assign rigidbody
        rb = GetComponent<Rigidbody>();
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    //called every frame
    private void Update()
    {
        float mousey = Input.GetAxisRaw("Mouse Y");
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        localEulerAnglesInput = new Vector2(Input.GetAxisRaw("Mouse X"), mousey);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (isWallRunning)
        {
            playerCam.GetComponent<Camera>().fieldOfView = 96;
            if (Physics.Raycast(transform.position, transform.right, 1f, ground))
            {
                rb.velocity += transform.right * 0.1f;

                if (playerCam.localEulerAngles.z < 15f || playerCam.localEulerAngles.z > 345f)
                {
                    //we multiply by Time.deltaTime to make frame rate not affect speed
                    playerCam.localEulerAngles += new Vector3(0, 0, 100f * Time.deltaTime);
                }

            }
            if (Physics.Raycast(transform.position, -transform.right, 1f, ground))
            {
                if (playerCam.localEulerAngles.z > 345f)
                {
                    playerCam.localEulerAngles += new Vector3(0, 0, -100f * Time.deltaTime);
                }
                playerCam.localEulerAngles += new Vector3(0, 0, -10f * Time.deltaTime);
                rb.velocity += transform.right * -0.1f;
            }
        }
        else
        {
            playerCam.GetComponent<Camera>().fieldOfView = 90;
        }

        // this makes the player go faster every frame its on the wall

        //if (isWallRunning && rb.velocity.magnitude <= 30)
        //{
        //    rb.velocity += transform.forward * 100 * Time.deltaTime + transform.up * -0.1f;
        //}

        cs = sensitivity;
        cms = speed;

        if (isWallRunning && isGrounded)
        {
            isWallRunning = false;
        }

        camRot -= localEulerAnglesInput.y * cs;
        camRot = Mathf.Clamp(camRot, -70, 70);
        isGrounded = Physics.Raycast(transform.position, -transform.up, 1.1f, ground);
        rb.velocity += transform.forward * movementInput.y * cms * Time.deltaTime + transform.right * movementInput.x * cms * Time.deltaTime;
        transform.localEulerAngles += new Vector3(0, localEulerAnglesInput.x, 0) * cs;
        playerCam.localEulerAngles = new Vector3(camRot, playerCam.localEulerAngles.y, playerCam.localEulerAngles.z);
    }



    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, JumpForce, rb.velocity.z);
        }
        if (isWallRunning) // jump off wall
        {
            if (Physics.Raycast(transform.position, transform.right, 1f, wall))
            {
                rb.velocity = new Vector3(0, JumpForce / 2, 0) + (transform.right * -wallJumpAwayForce);
            }
            if (Physics.Raycast(transform.position, -transform.right, 1f, wall))
            {
                rb.velocity = new Vector3(0, JumpForce / 2, 0) + (transform.right * wallJumpAwayForce);
            }
        }
    }

    private bool canWallRun = true;
    //called when collision is entered
    private void OnCollisionEnter(Collision other)
    {
        //using dot product to make sure we are infront of or behind the collision normal and not on top of or below
        if (Mathf.Abs(Vector3.Dot(other.GetContact(0).normal, Vector3.up)) < 0.1f && canWallRun)
        {
            rb.velocity = new Vector3(0, wallUpwardForce, 0) + transform.forward * wallRunSpeedBoost; //* rb.velocity.magnitude;
            isWallRunning = true;
        }
    }
    //called on collision exit
    void OnCollisionExit(Collision other)
    {
        isWallRunning = false;
        playerCam.localEulerAngles = new Vector3(playerCam.localEulerAngles.x, playerCam.localEulerAngles.y, 0);
        StartCoroutine(WallRunCooldown());
    }
    private IEnumerator WallRunCooldown()
    {
        canWallRun = false;
        yield return new WaitForSeconds(0.3f);
        canWallRun = true;
    }
    public void fn_ZeroRigidBodyMomentum()
    {
        rb.velocity = Vector3.zero;
    }
}
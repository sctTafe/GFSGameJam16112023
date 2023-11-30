using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tether : MonoBehaviour
{
    [Header("References")]
    public Transform camera;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerController player;
    public LineRenderer lr;
    public Image crosshair;
    public Slider timerSlider;

    [Header("Tethering")]
    public float maxTetherTime;
    public float tetherForce;
    private float tetherTimer;
    public float tetherRange;
    public float recoveryRate;

    [Header("Input")]
    public KeyCode tetherKey = KeyCode.Mouse0;

    private Vector3 tetherPos;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.forward, out hit, tetherRange))
        {
            crosshair.color = Color.green;
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                StartTether(hit.point);
            }
        }
        else
        {
            crosshair.color = Color.white;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) && player.tethered)
        {
            StopTether();
        }

        if (player.tethered && !player.disabled && tetherTimer < maxTetherTime)
        {
            rb.AddForce(Vector3.Normalize(tetherPos - camera.transform.position) * tetherForce);
            rb.velocity = new Vector3(rb.velocity.x*1.1f, Mathf.Clamp(rb.velocity.y, -20, 2f), rb.velocity.z*1.1f);
            lr.SetPosition(0, transform.position);
            tetherTimer += Time.deltaTime;
        }
        else
        {
            tetherTimer = Mathf.Clamp(tetherTimer - Time.deltaTime * recoveryRate, 0f, maxTetherTime);
        }

        timerSlider.value = 1f - tetherTimer / maxTetherTime;
    }

    void StartTether(Vector3 pos)
    {
        player.tethered = true;
        tetherPos = pos;                
        lr.gameObject.SetActive(true);
        lr.SetPosition(1, tetherPos);
    }

    void StopTether()
    {
        player.tethered = false;            
        lr.gameObject.SetActive(false);
    }
}
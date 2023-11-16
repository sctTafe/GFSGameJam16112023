using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lr;
    public GameObject pos1;
    public GameObject pos2;
    // Start is called before the first frame update
    void Start()
    {
        lr.SetPosition(0, pos1.transform.position);
        lr.SetPosition(1, pos2.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

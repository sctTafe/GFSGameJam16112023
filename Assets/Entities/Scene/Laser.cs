using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Laser : MonoBehaviour
{
    public bool disabled = false;

    public LaserGate[] gates;
    
    // Start is called before the first frame update

    [Serializable] public struct LaserGate
    {
        public GameObject pos1;
        public GameObject pos2;
        public LineRenderer line;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (disabled)
        {
            foreach (LaserGate gate in gates)
            {
                gate.line.gameObject.SetActive(false);
                gate.pos1.SetActive(false);
                gate.pos2.SetActive(false);
            }
            return;
        }
        
        foreach (LaserGate gate in gates)
        {
            gate.line.gameObject.SetActive(true);
            gate.pos1.SetActive(true);
            gate.pos2.SetActive(true);

            Vector3 gatePos1 = gate.pos1.transform.position;
            Vector3 gatePos2 = gate.pos2.transform.position;
            
            gate.line.SetPosition(0, gate.line.transform.InverseTransformPoint(gatePos1) + UnityEngine.Random.insideUnitSphere*0.01f);
            gate.line.SetPosition(1, gate.line.transform.InverseTransformPoint(gatePos2) + UnityEngine.Random.insideUnitSphere*0.01f);
            
            RaycastHit hit;
            Vector3 dir = Vector3.Normalize(gatePos2-gatePos1);
            
            if (Physics.Raycast(gatePos1, dir, out hit))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    GameManager.instance.ResetLevel();
                }
            }
        }

    }
    public void fn_ToggleLaserOn()
    {
        disabled = !disabled;
    }
}

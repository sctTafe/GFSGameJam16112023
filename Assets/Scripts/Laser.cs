using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public bool disabled = false;
    public LineRenderer lr;
    public GameObject pos1;
    public GameObject pos2;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (disabled)
        {
            lr.gameObject.SetActive(false);
            pos1.SetActive(false);
            pos2.SetActive(false);
            return;
        }
        lr.gameObject.SetActive(true);
        pos1.SetActive(true);
        pos2.SetActive(true);

        lr.SetPosition(0, lr.transform.InverseTransformPoint(pos1.transform.position) + Random.insideUnitSphere*0.01f);
        lr.SetPosition(1, lr.transform.InverseTransformPoint(pos2.transform.position) + Random.insideUnitSphere*0.01f);

        RaycastHit hit;
        Vector3 dir = Vector3.Normalize(pos2.transform.position - pos1.transform.position);
        if (Physics.Raycast(pos1.transform.position, dir, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                Debug.Log(hit.transform.name);
            }
        }
    }
    public void fn_ToggleStomperOn()
    {
        disabled = !disabled;
    }
}

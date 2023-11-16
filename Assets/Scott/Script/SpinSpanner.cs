using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSpanner : MonoBehaviour
{
    public float _rotationSpeedMultiplier = 100;
    Transform _spannerTrans;
    // Start is called before the first frame update
    void Start()
    {
        _spannerTrans = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _spannerTrans.Rotate(Vector3.right * Time.deltaTime * _rotationSpeedMultiplier);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpannerScript : MonoBehaviour
{
    public UnityEvent _OnDestruction;
    public UnityEvent _OnSpannerCollision;

    public float _destoryDelay = 0.5f;

    public void fn_CallDelayedDestruction()
    {
        StartCoroutine(DestoryAfterDelay(_destoryDelay));
    }
    IEnumerator DestoryAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        _OnDestruction.Invoke();
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _OnSpannerCollision?.Invoke();
        StartCoroutine(DestoryAfterDelay(_destoryDelay));
    }

}

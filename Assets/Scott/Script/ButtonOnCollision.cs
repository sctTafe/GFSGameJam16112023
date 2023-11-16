using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ButtonOn : MonoBehaviour
{
    public UnityEvent _OnCollision;
    public string _spannerTag = "Spanner";
    private bool _isWaiting = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == _spannerTag)
        {
            if (!_isWaiting)
            {
                Debug.Log("Spanner Collision Hit");
                _OnCollision?.Invoke();

                _isWaiting = true;
                StartCoroutine(Wait());
            }
           
        }
    }

    IEnumerator Wait()
    {

        yield return new WaitForSeconds(0.5f);
        _isWaiting = false;
    }

}

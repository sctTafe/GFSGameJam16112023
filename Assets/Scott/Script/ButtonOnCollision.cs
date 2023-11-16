using UnityEngine;
using UnityEngine.Events;

public class ButtonOn : MonoBehaviour
{
    public UnityEvent _OnCollision;
    public string _spannerTag;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == _spannerTag)
        {
            _OnCollision?.Invoke();
        }
    }
}

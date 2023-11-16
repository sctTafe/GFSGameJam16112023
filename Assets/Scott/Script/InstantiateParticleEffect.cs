using UnityEngine;

public class InstantiateParticleEffect : MonoBehaviour
{
    public GameObject _particaleffectPrefab;

    public void fn_Instanciate()
    {
        Instantiate(_particaleffectPrefab,this.transform.position, Quaternion.identity);
    }
}

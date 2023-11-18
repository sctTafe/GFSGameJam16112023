using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    public Transform _spawnPoint;
    public float _playerRespawnYHeight = 0f;


    private Transform _playerTransform;
    private Rigidbody _rigidBody;
    private Vector3 _resetPosition;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _playerTransform = transform;

        if (_spawnPoint == null)
        {
            _resetPosition = Vector3.zero;
        }
        else
        {
            _resetPosition = _spawnPoint.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerTransform.position.y <= _playerRespawnYHeight)
        {
            ResetPlayerToStart();
        }
    }

    private void ResetPlayerToStart()
    {
        _playerTransform.position = _resetPosition;
        _playerTransform.rotation = Quaternion.identity;
        ZeroRBMomentum();   
    }

    void ZeroRBMomentum()
    {
        _rigidBody.velocity = Vector3.zero;
    }

    public void fn_DeathByEnvrionment()
    {
        ResetPlayerToStart();
    }
}

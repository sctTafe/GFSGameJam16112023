using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    public Transform _spawnPoint;
    public float _playerRespawnYHeight = 0f;


    private Transform _playerTransform;
    private PlayerController _playerController;
    private Vector3 _resetPosition;

    // Start is called before the first frame update
    void Start()
    {
        _resetPosition = _spawnPoint.position;
        _playerController = GetComponent<PlayerController>();
        _playerTransform = this.transform;
        if (_playerTransform == null) {
            _resetPosition = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerTransform.position.y <= _playerRespawnYHeight) 
        {
            _playerTransform.position = _resetPosition;
        }
    }

    void ZeroRBMomentum()
    {
        _playerController.fn_ZeroRigidBodyMomentum();
    }
}

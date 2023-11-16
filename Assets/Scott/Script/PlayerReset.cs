using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    public Transform _spawnPoint;
    public float _playerRespawnYHeight = 0f;
    private Transform _playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerTransform.position.y <= _playerRespawnYHeight) 
        {
            _playerTransform.position = _spawnPoint.position;
        }
    }
}

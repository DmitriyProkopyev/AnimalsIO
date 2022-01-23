using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private PlayerInfo _playerInfo;
    [SerializeField] private float _distance;
    [SerializeField] private float _speed;

    private Vector3 _startDifference;

    private void Start() => _startDifference = transform.position - _playerInfo.Keeper.AveragePosition;
    
    private void OnDisable() => _playerInfo.Keeper.Added -= MoveAway;

    public void Initialize(PlayerInfo info)
    {
        _playerInfo = info;
        _playerInfo.Keeper.Added += MoveAway;
    }
    
    private void LateUpdate() => transform.position = _playerInfo.Keeper.AveragePosition + _startDifference;

    private void MoveAway(Unit _)
    {
        if (_playerInfo.Keeper.Replacing == false)
            StartCoroutine(MoveAwayJob());
    }

    private IEnumerator MoveAwayJob()
    {
        var step = _distance / _speed;
        
        for (float i = 0; i < _distance; i += step)
        {
            _startDifference -= transform.forward * step;
            yield return null;
        }
    }
}

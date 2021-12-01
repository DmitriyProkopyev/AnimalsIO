using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraMover : MonoBehaviour
{
    [SerializeField] private PlayerInfo _playerInfo;

    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    private Rigidbody _rigidbody;
    private Vector3 _startDifference;

    private void Start()
    {
        _startDifference = transform.position - _playerInfo.Keeper.AveragePosition;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable() => _playerInfo.Spawner.Spawned += OnUnitSpawned;

    private void OnDisable() => _playerInfo.Spawner.Spawned -= OnUnitSpawned;

    private void LateUpdate()
    {
        var velocity = _playerInfo.Keeper.AveragePosition + _startDifference - transform.position;
        _rigidbody.velocity = PowerOfTwo(velocity) * _speed;
    }

    private void OnUnitSpawned(Unit unit) => StartCoroutine(MoveBackwards());

    private IEnumerator MoveBackwards()
    {
        var modifier = transform.forward * _distance;
        for (int i = 0; i < 1 / _speed; i++)
        {
            _startDifference -= modifier * _speed;
            yield return null;
        }
    }

    private static Vector3 PowerOfTwo(Vector3 vector)
    {
        float x = vector.x * vector.x * Mathf.Sign(vector.x);
        float y = vector.y * vector.y * Mathf.Sign(vector.y);
        float z = vector.z * vector.z * Mathf.Sign(vector.z);
        return new Vector3(x, y, z);
    }
}

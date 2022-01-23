using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UnitsKeeper))]
public class Mover : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float _centripetalForce;
    [Min(0)] [SerializeField] private float _speed;

    private UnitsKeeper _keeper;
    private IInput _input;

    private bool _isMoving;
    private bool _wasMoving;

    private Vector3 _velocity;

    private void Awake()
    {
        _isMoving = false;
        _wasMoving = false;
        _keeper = GetComponent<UnitsKeeper>();
    }

    public void Initialize(IInput input) => _input = input;

    public void ApplyImpulse() => StartCoroutine(ApplyImpulseJob());

    private void FixedUpdate()
    {
        _wasMoving = _isMoving;
        _isMoving = !_input.Direction.Equals(Vector3.zero);
        _velocity = _isMoving ? _input.Direction * _speed : Vector3.zero;

        foreach (var unit in _keeper.Units)
            Move(unit);
    }

    private void Move(Unit unit)
    {
        var centripetalDirection = _keeper.AveragePosition - unit.transform.position;
        var centripetalAcceleration = centripetalDirection * _centripetalForce;
        unit.SetVelocity(_velocity + centripetalAcceleration);

        if (_wasMoving != _isMoving)
        {
            if (_isMoving)
                unit.Run();
            else
                unit.Stay();
        }
    }

    private IEnumerator ApplyImpulseJob()
    {
        float centripetal = _centripetalForce;
        _centripetalForce = -_centripetalForce * 3;
        yield return new WaitForSeconds(0.5f);
        _centripetalForce = 0;
        while (_centripetalForce < centripetal * 3)
        {
            _centripetalForce = Mathf.MoveTowards(_centripetalForce, centripetal, 0.03f);
            yield return null;
        }
    }
}

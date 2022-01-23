using UnityEngine;

[RequireComponent(typeof(Radar))]
[RequireComponent(typeof(UnitsKeeper))]
public class EnemyAI : MonoBehaviour, IInput
{
    [SerializeField] private float _maxDistractionInAngles;
    [SerializeField] private float _thinkingSpeed;

    private Vector3 _mainTarget;
    private Vector3 _tempTarget;
    private Vector3 _currentTarget;
    private Vector3 _targetDirection;

    private Radar _radar;
    private UnitsKeeper _keeper;

    public Vector3 Direction { get; private set; }

    private void OnValidate() => _maxDistractionInAngles = Mathf.Repeat(_maxDistractionInAngles, 180);

    private void Start()
    {
        _radar = GetComponent<Radar>();
        _keeper = GetComponent<UnitsKeeper>();
        _mainTarget = Map.RandomPoint;
        _currentTarget = _mainTarget;
    }

    private void FixedUpdate()
    {
        UpdateTargets();

        _targetDirection = (_currentTarget - _keeper.AveragePosition).normalized;
        Direction = Vector3.MoveTowards(Direction, _targetDirection, _thinkingSpeed);
    }

    private void UpdateTargets()
    {
        if (_tempTarget.Equals(Vector3.zero))
            _tempTarget = FindTempTarget();

        if (_mainTarget.Equals(Vector3.zero))
            _mainTarget = Map.RandomPoint;

        if (IsTargetReached(_currentTarget))
            _currentTarget = DefineNewTarget();
    }

    private bool IsTargetReached(Vector3 target) => (target - _keeper.AveragePosition).sqrMagnitude < 1f;

    private Vector3 DefineNewTarget()
    {
        if (_currentTarget == _mainTarget)
            _mainTarget = Map.RandomPoint;

        if (_currentTarget == _tempTarget)
            _tempTarget = FindTempTarget();

        return _tempTarget == Vector3.zero ? _mainTarget : _tempTarget;
    }

    private Vector3 FindTempTarget()
    {
        var target = _radar.Scan(_keeper.AveragePosition);
        if (CalculateAngle(_keeper.AveragePosition, _mainTarget) <= _maxDistractionInAngles)
            return target;
        return Vector3.zero;
    }

    private float CalculateAngle(Vector3 first, Vector3 second)
    {
        var position = _keeper.AveragePosition;
        var firstVector = first - position;
        var secondVector = second - position;
        return Vector3.Angle(firstVector, secondVector);
    }
}

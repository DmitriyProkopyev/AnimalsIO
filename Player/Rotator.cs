using UnityEngine;

[RequireComponent(typeof(UnitsKeeper))]
public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speedOfRotation;

    private UnitsKeeper _keeper;
    private IInput _input;

    private void OnValidate() => _speedOfRotation = Mathf.Clamp(_speedOfRotation, 0, float.MaxValue);

    private void Awake() => _keeper = GetComponent<UnitsKeeper>();

    public void Initialize(IInput input) => _input = input;

    private void Update()
    {
        foreach (var unit in _keeper.Units)
            Rotate(unit.transform);
    }

    private void Rotate(Transform unit)
    {
        var rotation = Vector3.RotateTowards(unit.forward, _input.Direction, _speedOfRotation * Time.deltaTime, 1).normalized;
        unit.LookAt(unit.position + rotation);
    }
}

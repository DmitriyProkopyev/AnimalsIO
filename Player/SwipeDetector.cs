using UnityEngine;

public class SwipeDetector : MonoBehaviour, IInput
{
    private Vector2 _direction;
    private Vector2 _startPosition;

    public Vector3 Direction => new Vector3(_direction.x, 0, _direction.y).normalized;

    private void Start()
    {
        _startPosition = Vector2.zero;
        _direction = Vector2.zero;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _startPosition = Input.mousePosition;

        if (Input.GetMouseButton(0))
            _direction = (Vector2)Input.mousePosition - _startPosition;

        if (Input.GetMouseButtonUp(0))
            _direction = Vector2.zero;
    }
}

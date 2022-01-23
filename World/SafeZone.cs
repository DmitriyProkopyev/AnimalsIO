using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class SafeZone : MonoBehaviour, IInteractable
{
    [SerializeField] private float _maxRadius;
    [SerializeField] private float _minRadius;
    [SerializeField] private float _decreasingSpeed;
    [SerializeField] private UnityEvent _ready;
    
    [SerializeField] private RectTransform _mask;

    private float _radius;

    private void OnValidate()
    {
        if (_minRadius < 1)
            _minRadius = 1;
        if (_maxRadius <= _minRadius)
            _maxRadius = _minRadius + 1;
        _radius = _maxRadius;
        _mask.transform.localScale = new Vector2(_radius, _radius);
    }

    private void Update()
    {
        if (_radius > _minRadius)
        {
            _radius -= _decreasingSpeed * Time.deltaTime;
            _mask.transform.localScale = new Vector2(_radius, _radius);
        }
        else
            _ready.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
            unit.Interact(this);
    }
    
    public void Interact(IInteractable interactor) { }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(UnitsKeeper))]
public class Radar : MonoBehaviour
{
    [SerializeField] private float _visibilityRadius;
    [SerializeField] private LayerMask _layerMask;

    public Vector3 Scan(Vector3 position)
    {
        var colliders = Physics.OverlapSphere(position, _visibilityRadius, _layerMask, QueryTriggerInteraction.Collide);

        if (colliders.Length == 0)
            return Map.RandomPoint;

        return colliders[Random.Range(0, colliders.Length)].transform.position;
    }
}

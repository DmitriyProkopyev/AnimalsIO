using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInfo))]
public class UnitsSpawner : MonoBehaviour
{
    private PlayerInfo _info;
    
    public event UnityAction<Unit> Spawned;

    private void OnEnable()
    {
        _info = GetComponent<PlayerInfo>();
        _info.CollisionsHandler.UnitShouldBeSpawned += SpawnNew;
    }
    
    private void OnDisable()
    {
        _info.CollisionsHandler.UnitShouldBeSpawned -= SpawnNew;
    }

    private void SpawnNew()
    {
        var unit = Spawn(_info.Keeper.AveragePosition);
        Spawned?.Invoke(unit);
    }

    private Unit Spawn(Vector3 position)
    {
        var unit = Instantiate(_info.Hierarchy.CurrentPrefab, position, Quaternion.identity);
        unit.Initialize(_info);
        unit.transform.parent = transform;
        return unit;
    }

    public IEnumerable<Unit> SpawnGeneration(IEnumerable<Vector3> positions)
    {
        var units = new List<Unit>();
        foreach (var position in positions)
            units.Add(Spawn(position));

        return units;
    }
}

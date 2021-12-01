using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInfo))]
public class UnitsKeeper : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;

    private PlayerInfo _info;
    private bool _replacing = false;

    public IReadOnlyCollection<Unit> Units => _units;
    
    public Vector3 AveragePosition { get; private set; }

    public event UnityAction<Unit> Added;
    public event UnityAction<Unit> Removed;

    private void Awake()
    {
        if (_units.Count == 0)
            throw new ArgumentException("You need to have at least one unit");

        _info = GetComponent<PlayerInfo>();
        AveragePosition = CalculateAveragePosition();
    }

    private void OnEnable()
    {
        _info.Spawner.Spawned += Add;
        _info.CollisionsHandler.FoodCollected += OnFoodCollected;
        _info.CollisionsHandler.UnitsBeingEaten += OnUnitsBeingEaten;
    }

    private void OnDisable()
    {
        _info.Spawner.Spawned -= Add;
        _info.CollisionsHandler.FoodCollected -= OnFoodCollected;
        _info.CollisionsHandler.UnitsBeingEaten -= OnUnitsBeingEaten;
    }

    private void Update() => AveragePosition = CalculateAveragePosition();

    private void OnFoodCollected()
    {
        if (_info.Level == _info.Hierarchy.LevelLimit)
            ReplaceAllUnits();
    }

    private Vector3 CalculateAveragePosition()
    {
        var sum = Vector3.zero;
        foreach (var unit in _units)
            sum += unit.transform.position;
        
        if (_units.Count > 0)
            return sum / _units.Count;
        return AveragePosition;
    }

    private void ReplaceAllUnits()
    {
        _replacing = true;

        _info.Hierarchy.Next();
        var positions = _units.Select(unit => unit.transform.position);
        var newUnits = _info.Spawner.SpawnGeneration(positions.Append(AveragePosition).ToArray()).ToArray();

        foreach (var unit in new List<Unit>(_units))
            Remove(unit);

        foreach (var unit in newUnits)
            Add(unit);

        _replacing = false;
    }

    private void Add(Unit unit)
    {
        _units.Add(unit);
        Added?.Invoke(unit);
        unit.Run();
    }

    private void Remove(Unit unit)
    {
        _units.Remove(unit);
        Destroy(unit.gameObject);
        Removed?.Invoke(unit);

        if (_replacing == false && _units.Count == 0)
            Destroy(gameObject);
    }

    private void OnUnitsBeingEaten() => Remove(_units.First()); 
}

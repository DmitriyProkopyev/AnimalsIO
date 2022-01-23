using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Units Hierarchy", fileName = "Hierarchy", order = 51)]
public class UnitsHierarchy : ScriptableObject
{
    [SerializeField] private List<UnitCell> _cells;
    [SerializeField] private int _maxUnits;

    public UnitCell this[int index] => _cells[index];
    public int Count => _cells.Count;
    public int MaxUnits => _maxUnits;

    [Serializable]
    public class UnitCell
    {
        [SerializeField] private Unit _prefab;
        [SerializeField] private uint _maxCount;

        public Unit Prefab => _prefab;
        public int LevelLimit => (int)_maxCount;
    }
}


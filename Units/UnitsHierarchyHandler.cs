using UnityEngine;

public class UnitsHierarchyHandler : MonoBehaviour
{
    [SerializeField] private UnitsHierarchy _hierarchy;

    private int _lastIndex;
    
    public int MaxUnits { get; private set; }
    public Unit CurrentPrefab { get; private set; }
    public int LevelLimit { get; private set; }

    private void Start()
    {
        _lastIndex = -1;
        MaxUnits = _hierarchy.MaxUnits;
        Next();
    }

    public void Next()
    {
        if (_lastIndex < _hierarchy.Count - 1)
            _lastIndex++;

        CurrentPrefab = _hierarchy[_lastIndex].Prefab;
        LevelLimit = _hierarchy[_lastIndex].LevelLimit;
    }
}

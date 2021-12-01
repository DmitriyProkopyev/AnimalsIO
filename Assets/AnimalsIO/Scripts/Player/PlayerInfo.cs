using UnityEngine;

[RequireComponent(typeof(UnitsKeeper))]
[RequireComponent(typeof(CollisionsHandler))]
[RequireComponent(typeof(UnitsHierarchyHandler))]
[RequireComponent(typeof(UnitsSpawner))]
public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private AnimalType _type;
    [SerializeField] private UnitsKeeper _keeper;
    [SerializeField] private CollisionsHandler _collisionHandler;
    [SerializeField] private UnitsHierarchyHandler _hierarchy;
    [SerializeField] private UnitsSpawner _spawner;

    public int Level => _score.Value;
    public AnimalType Type => _type;

    public UnitsKeeper Keeper => _keeper;
    public CollisionsHandler CollisionsHandler => _collisionHandler;
    public UnitsHierarchyHandler Hierarchy => _hierarchy;
    public UnitsSpawner Spawner => _spawner;
}

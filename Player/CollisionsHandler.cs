using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInfo))]
public class CollisionsHandler : MonoBehaviour
{
    private PlayerInfo _info;

    private Interaction _current = Interaction.None;
    private bool _inSafeZone = true;

    public event UnityAction FoodCollected;
    public event UnityAction UnitShouldBeSpawned;
    public event UnityAction UnitsBeingEaten;

    private void Awake()
    {
        _info = GetComponent<PlayerInfo>();
        StartCoroutine(ApplyCollisions());
    }

    private void OnEnable()
    {
        var keeper = _info.Keeper;

        keeper.Added += OnUnitAdded;
        keeper.Removed += OnUnitRemoved;
        foreach (var unit in keeper.Units)
            unit.CollisionDetected += OnCollisionDetected;
    }
    
    private void OnDisable()
    {
        var keeper = _info.Keeper;

        keeper.Added -= OnUnitAdded;
        keeper.Removed -= OnUnitRemoved;
        foreach (var unit in keeper.Units)
            unit.CollisionDetected -= OnCollisionDetected;
    }

    private void OnCollisionDetected(IInteractable interactable, Unit source)
    {
        var keeper = _info.Keeper;

        if (interactable is Food food && _info.Type == food.Type)
        {
            Destroy(food.gameObject);
            FoodCollected?.Invoke();

            if (keeper.Units.Count <= _info.Hierarchy.MaxUnits)
                UnitShouldBeSpawned?.Invoke();

            return;
        }

        if (interactable is Unit unit)
        {
            if (unit.transform.parent == transform)
                return;

            if (_info.Level < unit.Level)
                _current = Interaction.Subordinate;

            else if (_info.Level > unit.Level)
                _current = Interaction.Dominant;
        }

        if (interactable is SafeZone)
            _inSafeZone = true;
    }

    private IEnumerator ApplyCollisions()
    {
        var wait = new WaitForSeconds(0.3f);

        while (true)
        {
            switch (_current)
            {
                case Interaction.Dominant:
                    FoodCollected?.Invoke();
                    if (_info.Keeper.Units.Count <= _info.Hierarchy.MaxUnits)
                        UnitShouldBeSpawned?.Invoke();
                    break;
                case Interaction.Subordinate:
                    UnitsBeingEaten?.Invoke();
                    break;
            }

            if (_inSafeZone == false)
                UnitsBeingEaten?.Invoke();
            
            _current = Interaction.None;
            _inSafeZone = false;
            yield return wait;
        }
    }

    private void OnUnitAdded(Unit unit) => unit.CollisionDetected += OnCollisionDetected;

    private void OnUnitRemoved(Unit unit) => unit.CollisionDetected -= OnCollisionDetected;

    private enum Interaction
    {
        Dominant,
        Subordinate,
        None
    }
}

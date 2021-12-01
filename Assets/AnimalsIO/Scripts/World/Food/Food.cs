using UnityEngine;

public class Food : MonoBehaviour, IInteractable
{
    [SerializeField] private AnimalType _type;

    public AnimalType Type => _type;

    public void Interact(IInteractable interactor) => Destroy(gameObject);

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Unit unit))
            unit.Interact(this);
    }
}

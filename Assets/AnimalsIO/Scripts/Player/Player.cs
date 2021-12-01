using UnityEngine;
using InvalidOperationException = System.InvalidOperationException;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(PlayerInfo))]
public class Player : MonoBehaviour
{
    private IInput _input;

    private void Awake()
    {
        if (TryGetComponent(out _input) == false)
            throw new InvalidOperationException("Attach any IInput component to " + gameObject.name);

        GetComponent<Mover>().Initialize(_input);
        GetComponent<Rotator>().Initialize(_input);
    }
}

public enum AnimalType
{
    Herbivor,
    Predator
}

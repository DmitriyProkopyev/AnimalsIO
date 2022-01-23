using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(UnitAnimator))]
public class Unit : MonoBehaviour, IInteractable
{
    private UnitAnimator _animator;
    private Rigidbody _rigidbody;

    private PlayerInfo _playerInfo;

    public int Level => _playerInfo.Level;

    public event UnityAction<IInteractable, Unit> CollisionDetected;

    public void Initialize(PlayerInfo info) => _playerInfo = info;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<UnitAnimator>();
        _playerInfo = GetComponentInParent<PlayerInfo>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit unit))
            Interact(unit);
    }

    public void Interact(IInteractable interactable) => CollisionDetected?.Invoke(interactable, this);

    public void SetVelocity(Vector3 velocity) => _rigidbody.velocity = velocity;

    public void Run() => _animator.Run();

    public void Stay() => _animator.Stay();

    public void Win() => _animator.Win();
}

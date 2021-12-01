using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator[] _animators;
    
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    public void Run()
    {
        foreach (var animator in _animators)
            animator.SetBool(IsMoving, true);
    }

    public void Stay()
    {
        foreach (var animator in _animators)
            animator.SetBool(IsMoving, false);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private uint _delay;
    [SerializeField] private UnityEvent _event;

    private void Awake() => StartCoroutine(Execute());

    private IEnumerator Execute()
    {
        yield return new WaitForSeconds(_delay);
        _event.Invoke();
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] private int _startIndex;
    [SerializeField] private int _delay;
    [SerializeField] private Text _text;
    [SerializeField] private string _finalText;

    [SerializeField] private UnityEvent _ended;
    
    private void OnValidate()
    {
        if (_startIndex < 0)
            _startIndex = 0;
        if (_delay < 0)
            _delay = 0;
    }

    private void OnEnable() => StartCoroutine(Make());

    private IEnumerator Make()
    {
        var wait = new WaitForSeconds(_delay);

        for (int i = _startIndex; i > 0; i--)
        {
            _text.text = i.ToString();
            yield return wait;
        }
        
        _text.text = _finalText;
        yield return wait;
        _ended.Invoke();
        gameObject.SetActive(false);
    }
}

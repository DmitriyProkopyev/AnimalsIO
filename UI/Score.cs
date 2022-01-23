using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private PlayerInfo _info;
    
    public int Value { get; private set; }

    private void OnEnable()
    {
        Value = Mathf.Clamp(_info.Keeper.Units.Count, 0, int.MaxValue);
        _text.text = Value.ToString();

        _info.CollisionsHandler.FoodCollected += Increase;
        _info.CollisionsHandler.UnitsBeingEaten += Decrease;
    }

    private void OnDisable()
    {
        _info.CollisionsHandler.FoodCollected -= Increase;
        _info.CollisionsHandler.UnitsBeingEaten -= Decrease;
    }

    private void LateUpdate()
    {
        var position = _info.Keeper.AveragePosition;
        position.y = transform.position.y;
        transform.position = position;
    }

    private void Increase()
    {
        Value++;
        _text.text = Value.ToString();
    }

    private void Decrease()
    {
        int count = _info.Keeper.Units.Count;
        count = count < 1 ? 1 : count;
        Value -= Value / count;
        _text.text = Value.ToString();
    }
}

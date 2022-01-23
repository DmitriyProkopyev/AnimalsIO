using UnityEngine;

public class Game : MonoBehaviour
{
    public void Win()
    {
        foreach (var unit in FindObjectsOfType<Unit>())
            unit.Win(); 
    }
}

using UnityEngine;
using InvalidOperationException = System.InvalidOperationException;

public class Map : MonoBehaviour
{
    [SerializeField] private Vector2 _leftLowerBorder;
    [SerializeField] private Vector2 _rightUpperBorder;

    private static Vector2[] _borders;
    private static bool _isInitialized = false;

    private void Awake()
    {
        if (_isInitialized)
            throw new InvalidOperationException("There should be only one instance of Map on the scene");

        _borders = new Vector2[2] { _leftLowerBorder, _rightUpperBorder };
        _isInitialized = true;
    }

    public static Vector3 RandomPoint => new Vector3(Random.Range(_borders[0].x, _borders[1].x), 0,
        Random.Range(_borders[0].y, _borders[1].y));
}

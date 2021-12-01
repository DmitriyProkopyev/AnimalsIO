using System.Collections;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    [SerializeField] private float _waitingTime;
    [SerializeField] private Food[] _prefabs;
    [SerializeField] private int _startAmount;

    private void Start()
    {
        for (int i = 0; i < _startAmount; i++)
            Spawn();
        StartCoroutine(SpawnFood());
    }

    private IEnumerator SpawnFood()
    {
        var wait = new WaitForSeconds(_waitingTime);
        
        while (true)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        var prefab = _prefabs[Random.Range(0, _prefabs.Length)];
        var food = Instantiate(prefab, Map.RandomPoint, Quaternion.Euler(0, Random.Range(0, 180), 0));
        food.transform.position += new Vector3(0, prefab.transform.position.y, 0);
    }
}

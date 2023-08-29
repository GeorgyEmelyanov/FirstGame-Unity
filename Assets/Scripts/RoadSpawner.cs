using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{

    [SerializeField] List<GameObject> _roads;
    [SerializeField] float _roadLength = 400;
    GameObject _road;

    private void Start()
    {
        _road = Instantiate(_roads[Random.Range(0, _roads.Count)], transform.position, Quaternion.identity);
    }

    public void Spawn()
    {
        Vector3 position = new Vector3(0, 0, _road.transform.position.z + _roadLength);
        _road = Instantiate(_roads[Random.Range(0, _roads.Count)], position, Quaternion.identity);
    }
}

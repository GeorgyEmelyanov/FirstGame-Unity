using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> _zombies;
    GameObject _zombie;
    public Player _player;

    private void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }

    public void Spawn()
    {
        int lvl = 10 + (int)(_player._difficultylvl / 100);
        for (int i = 0; i < lvl; i++) 
        {
            float x, z;
            x = Random.Range(-20, 20);
            z = Random.Range(300, 550);
            Vector3 pos = new Vector3(x, 0.5f, z);
            _zombie = Instantiate(_zombies[Random.Range(0, _zombies.Count)], pos, Quaternion.identity);
        }
    }

}

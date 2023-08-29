using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour, IPooledObject
{
    float _speed;
    Vector3 endposition;

    private Player _player;

    public ObjectPooler.ObjectInfo.ObjectType Type => type;
    [SerializeField]
    private ObjectPooler.ObjectInfo.ObjectType type;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        endposition = new Vector3(Random.Range(-4.5f, 4.5f), 0.5f, 0f);
        transform.eulerAngles = new Vector3(0, 180, 0);
    }
    private void FixedUpdate()
    {
        Move();
        DestroyZombie();
    }

    private void Move()
    {
        _speed = Time.fixedDeltaTime * (_player._speed + 5f);
        if (transform.position.z > 4f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endposition, _speed);
        }
        else
        {
            transform.Translate(-transform.forward * _speed);
        }    
    }

    private void DestroyZombie()
    {
        if (transform.position.z < 0)
            Destroy(gameObject);
    }
}

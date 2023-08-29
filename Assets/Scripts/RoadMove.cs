using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMove : MonoBehaviour, IPooledObject
{
    float _speed;
    Player _player;

    public ObjectPooler.ObjectInfo.ObjectType Type => type;
    [SerializeField]
    private ObjectPooler.ObjectInfo.ObjectType type;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }
    private void FixedUpdate()
    {
        DestroyRoad();
        Move();
    }

    private void Move()
    {
        _speed = _player._speed;
        transform.Translate(-transform.forward * _speed * Time.fixedDeltaTime);
    }

    private void DestroyRoad()
    {
        if(transform.position.z < -500f)
            Destroy(gameObject);
    }
}

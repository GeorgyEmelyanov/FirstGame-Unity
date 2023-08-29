using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateSpawner : MonoBehaviour
{   
    [SerializeField] public GameObject _carGate;
    [SerializeField] public GameObject _fuelGate;

    [SerializeField] List<FuelData> _fuelTypes;
    [SerializeField] List<CarData> _carTypes;

    Player player;
   

    public CarData car;
    public FuelData fuel;
    Vector3 _pos1, _pos2;
    float _speed;
    void Start()
    {
        _pos1 = new Vector3(2.5f, 0.5f, 400);
        _pos2 = new Vector3(-2.5f, 0.5f, 400);

        _fuelGate = Instantiate(_fuelGate, _pos1, Quaternion.identity);
        _carGate = Instantiate(_carGate, _pos2, Quaternion.identity);

        player = FindObjectOfType<Player>();

        ReSpawn();
    }

    private void FixedUpdate()
    {
        GatesMove();
    }

    public void ReSpawn()
    {
        car = _carTypes[Random.Range(0, _carTypes.Count)];
        fuel = _fuelTypes[Random.Range(0, _fuelTypes.Count)];
    }

    public void GatesMove()
    {
        _speed = player._speed * Time.fixedDeltaTime;
        _fuelGate.transform.Translate(-transform.forward * _speed);
        _carGate.transform.Translate(-transform.forward * _speed);
        if ((_fuelGate.transform.position.z < -10f) && (_carGate.transform.position.z < -10f))
        {
            EnableGates();
            Replace();
        }
    }

    public void Replace()
    {
        if (Random.Range(0, 100) > 50)
        {
            _fuelGate.transform.position = _pos1;
            _carGate.transform.position = _pos2;
        }
        else
        {
            _fuelGate.transform.position = _pos2;
            _carGate.transform.position = _pos1;
        }
    }

    public void EnableGates()
    {
        _fuelGate.GetComponent<Collider>().enabled = true;
        _carGate.GetComponent<Collider>().enabled = true;
    }
}

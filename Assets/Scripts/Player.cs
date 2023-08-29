using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Спавнеры/контроллеры
    [SerializeField] RoadSpawner _roadSpawner;
    [SerializeField] ZombieSpawner _zombieSpawner;
    [SerializeField] GateSpawner gateSpawner;

    // Общие хар-ки игрока
    [SerializeField] public float _speed;
    [SerializeField] float _fuel;
    [SerializeField] int _money;
    [SerializeField] public int _difficultylvl;
    float _eulerY;
    bool canDrive = true;

    // Текущая и стартовая тачки
    [SerializeField] CarData _startCar;
    CarData _car;

    private void Awake()
    {
        // Инициализируем экзэмпляр машины
        _car = ScriptableObject.CreateInstance<CarData>();
    }

    private void Start()
    {
        _difficultylvl = 10;
        _fuel = 40;
        SetStartCar();
    }

    public void FixedUpdate()
    {
        FuelCounter();
        if (canDrive)
            Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        switch (tag)
        {
            case "Enemy":
                EnemyTrig(other);
                break;
            case "RoadTrigger":
                RoadTrig(other);
                break;
            case "DifficultyTrigger":
                DifficultyTrig(other);
                break;
            case "GateChangeCar":
                ChangeCar();
                break;
            case "GateCollectFuel":
                CollectFuel();
                break;
            default:
                break;

        }
    }

    public void IncreaseDifficulty()
    {
        if (_difficultylvl < 10000)
            _difficultylvl = (int)(_difficultylvl * 1.15);
        _zombieSpawner.Spawn();
    }

    public void ChangeCar()
    {
        DisableGates();
        
        if (_money - gateSpawner.car.Cost > 0)
        {
            Debug.Log("u change car");
            _money -= gateSpawner.car.Cost;
            _car = gateSpawner.car;
            GameObject newCar = (GameObject)Instantiate(_car.Type);

            if (transform.childCount > 0)
                Destroy(transform.GetChild(0).gameObject);

            Vector3 pos = newCar.transform.position;
            Quaternion angle = newCar.transform.rotation;

            newCar.transform.parent = gameObject.transform;
            newCar.transform.localPosition = pos;
            newCar.transform.localRotation = angle;

            newCar.transform.SetSiblingIndex(0);
        }
        else
            Debug.Log("Not enough money to change car");

        gateSpawner.ReSpawn();
    }

    public void Move()
    {
        float localspeed;
        float fdt = Time.fixedDeltaTime;

        if (_speed<60)
            localspeed = _speed * fdt * 3;
        else
            localspeed = 60 * fdt * 3;

        if ((Input.GetAxisRaw("Horizontal") == 1) && (transform.position.x < 3.25f))
        {
            transform.position += new Vector3(0.03f * localspeed, 0, 0);
            _eulerY += 1f * localspeed;
            _eulerY = Mathf.Clamp(_eulerY, -20, 20);
            transform.eulerAngles = new Vector3(0, _eulerY, 0);
        }
        else if ((Input.GetAxisRaw("Horizontal") == -1) && (transform.position.x > -3.25f)) 
        {
            transform.position += new Vector3(-0.03f*localspeed, 0, 0);
            _eulerY -= 1f*localspeed;
            _eulerY = Mathf.Clamp(_eulerY, -20, 20);
            transform.eulerAngles = new Vector3(0, _eulerY, 0);
        }
        else
        {
            if (Mathf.Abs(_eulerY) < 2.5)
                _eulerY = 0;
            else if (_eulerY < 0)
                _eulerY += 1f*localspeed;
            else if (_eulerY > 0)
                _eulerY -= 1*localspeed;
            
            transform.eulerAngles = new Vector3(0, _eulerY, 0);
        }
    }

    public void FuelCounter()
    {
        float fdt = Time.fixedDeltaTime;
        if (_fuel > 0)
        {
            if (_speed < _car.MaxSpeed)
            {
                _speed += _car.IncreaseSpeed * fdt;
                _fuel -= _car.FuelFlow * fdt;
            }
            else
            {
                _fuel -= _car.FuelFlow * fdt / 1.5f;
            }
        }
        else
        {
            _speed -= _car.DecreaseSpeed * fdt;
            if ( _speed <=1 )
            {
                _speed = 0;
                canDrive = false;
            }
        }

        if (_speed - _car.DecreaseSpeed > _car.MaxSpeed)
            _speed -= _car.DecreaseSpeed * fdt;
    }

    public void EnemyTrig(Collider enemy)
    {
        if (canDrive)
        {
            Destroy(enemy.gameObject);
            _speed -= _car.ZombieInfluence;
            _money += Random.Range(10, 15);
        }
    }

    public void RoadTrig(Collider roadTrigger)
    {
        _roadSpawner.Spawn();
        Destroy(roadTrigger.gameObject);
    }

    public void DifficultyTrig(Collider difTrigger)
    {
        IncreaseDifficulty();
        Destroy(difTrigger.gameObject);
    }

    public void SetStartCar()
    {
        _car = _startCar;
    }

    public void CollectFuel()
    {
        DisableGates();

        if (_money - gateSpawner.fuel.Cost > 0)
        {
            Debug.Log("u take fuel");
            if (_fuel + gateSpawner.fuel.Quantity < _car.MaxFuel)
                _fuel += gateSpawner.fuel.Quantity;
            else
                _fuel = _car.MaxFuel;
            _money -= gateSpawner.fuel.Cost;
        }
        else
            Debug.Log("Not enough money to collect fuel");

        gateSpawner.ReSpawn();
    }

    private void DisableGates()
    {
        if (gateSpawner._carGate != null)
            gateSpawner._carGate.GetComponent<Collider>().enabled = false;
        if (gateSpawner._fuelGate != null)
            gateSpawner._fuelGate.GetComponent<Collider>().enabled = false;
    }
}

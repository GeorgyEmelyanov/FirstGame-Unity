using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New CarData", menuName = "Car Data", order = 51)]
public class CarData : ScriptableObject
{
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _increaseSpeed;
    [SerializeField]
    private float _decreaseSpeed;
    [SerializeField]
    private float _fuelFlow;
    [SerializeField]
    private float _fuelMax;
    [SerializeField]
    private float _zombieInfluence;
    [SerializeField]
    private GameObject _type;
    [SerializeField]
    private Image _img;
    [SerializeField]
    private int _cost;
    
    public float MaxSpeed
    {
        get
        {
            return _maxSpeed;
        }
    }

    public float IncreaseSpeed
    {
        get
        {
            return _increaseSpeed;
        }
    }

    public float DecreaseSpeed
    {
        get
        {
            return _decreaseSpeed;
        }
    }

    public float FuelFlow
    {
        get
        {
            return _fuelFlow;
        }
    }

    public float ZombieInfluence
    {
        get
        {
            return _zombieInfluence;
        }
    }

    public GameObject Type
    {
        get
        {
            return _type;
        }
    }

    public Image Image
    {
        get
        {
            return _img;
        }
    }

    public float MaxFuel
    {
        get
        {
            return _fuelMax;
        }
    }

    public int Cost
    {
        get
        {
            return _cost;
        }
    }
}
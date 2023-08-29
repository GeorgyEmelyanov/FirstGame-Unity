using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New FuelData", menuName = "Fuel Data", order = 52)] 

public class FuelData: ScriptableObject
{
    [SerializeField]
    private float _quantity;
    [SerializeField]
    private Image _img;
    [SerializeField]
    private int _cost;

    public float Quantity
    {
        get
        {
            return _quantity;
        }
    }
    
    public Image Image
    {
        get
        {
            return _img;
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

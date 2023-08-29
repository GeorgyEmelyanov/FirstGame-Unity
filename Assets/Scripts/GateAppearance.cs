using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GateAppearance : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _text;

    [SerializeField] Image _img;

    GateSpawner gateSpawner;

    private void Start()
    {
        gateSpawner = FindObjectOfType<GateSpawner>();
        SetAppearance();
    }

    private void FixedUpdate()
    {
        if (transform.position.z < -5)
            SetAppearance();
    }

    private void SetAppearance()
    {
        if (gameObject.name.Contains("CarGate"))
        {
            _text.text = "Price: " + gateSpawner.car.Cost.ToString();
            _img = gateSpawner.car.Image;
        }
        else if (gameObject.name.Contains("FuelGate"))
        {
            _text.text = "Price: " + gateSpawner.fuel.Cost.ToString();
            _img = gateSpawner.fuel.Image;
        }
    }

}

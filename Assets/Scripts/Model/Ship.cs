using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public float speed;
    public float rotationSpeed;

    public MetalCargo Cargo { get; private set; }
    public Hitpoints Hitpoints { get; private set; }

    private void Start() {
        Cargo = GetComponent<MetalCargo>();
        Hitpoints = GetComponent<Hitpoints>();

        ShipsManager.AddShip(this);
    }
}

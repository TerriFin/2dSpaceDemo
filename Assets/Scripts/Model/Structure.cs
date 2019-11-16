using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour {
    public bool sellsMetal;
    public bool buysMetal;
    public float buildingTime;
    public int buildingMoneyCost;
    public int buildingMetalCost;

    public MetalCargo Cargo { get; private set; }
    public Hitpoints Hitpoints { get; private set; }

    private void Start() {
        Cargo = GetComponent<MetalCargo>();
        Hitpoints = GetComponent<Hitpoints>();

        BuildingsManager.AddBuilding(this);
    }
}

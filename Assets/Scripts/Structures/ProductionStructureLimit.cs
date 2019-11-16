using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionStructureLimit : MonoBehaviour {
    public float buildTime;
    public int metalCost;
    public int moneyCost;
    public GameObject producedShip;
    public int howManyProduced;

    public bool online;

    public bool IsBuilding { get; private set; }
    public float BuildTimer { get; private set; }

    private MetalCargo cargo;
    private int producedShips; 

    private void Start() {
        IsBuilding = false;
        BuildTimer = 0;

        cargo = GetComponent<MetalCargo>();
        producedShips = 0;
    }

    private void Update() {
        if (online && !IsBuilding && cargo.CurrentMetal >= metalCost && producedShips < howManyProduced && FactionsManager.factions[gameObject.tag].money >= moneyCost) {
            BuildTimer = buildTime;
            cargo.RemoveMetal(metalCost);
            FactionsManager.factions[gameObject.tag].money -= moneyCost;
            IsBuilding = true;
        }

        if (online && IsBuilding) {
            BuildTimer -= Time.deltaTime;
            if (BuildTimer <= 0) {
                GameObject createdShip = Instantiate(producedShip);
                createdShip.tag = gameObject.tag;
                createdShip.transform.position = transform.position;

                LimitedProductionStructureTag limitedTag = createdShip.AddComponent<LimitedProductionStructureTag>();
                limitedTag.factory = this;
                producedShips++;

                IsBuilding = false;
            }
        }
    }

    public void shipDestroyed() {
        producedShips--;
    }
}
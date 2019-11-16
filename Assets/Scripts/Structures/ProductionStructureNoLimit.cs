using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionStructureNoLimit : MonoBehaviour, RightClick {
    public float buildTime;
    public int metalCost;
    public int moneyCost;
    public GameObject producedShip;
    public GameObject rallypointMark;

    public bool online;

    public bool IsBuilding { get; private set; }
    public float BuildTimer { get; private set; }
    public Vector2 RallyPos { get; private set; }

    private MetalCargo cargo;

    private void Start() {
        IsBuilding = false;
        BuildTimer = 0;
        RallyPos = transform.position;

        cargo = GetComponent<MetalCargo>();
    }

    private void Update() {
        if (online && !IsBuilding && cargo.CurrentMetal >= metalCost && FactionsManager.factions[gameObject.tag].money >= moneyCost) {
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
                IsBuilding = false;

                if (!(RallyPos.x == transform.position.x && RallyPos.y == transform.position.y)) {
                    MilitaryAi createdShipMilitaryAi = createdShip.GetComponent<MilitaryAi>();
                    createdShipMilitaryAi.automate = false;
                    createdShipMilitaryAi.HandleRightClick(RallyPos, tag);
                }
            }
        }
    }

    public void HandleRightClick(Vector2 clickedPos, string faction) {
        if (tag.Equals(faction)) {
            RallyPos = clickedPos;
            rallypointMark.transform.position = clickedPos;
        }
    }

    public void ResetRallyPoint() {
        RallyPos = transform.position;
        rallypointMark.transform.position = transform.position;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandStructure : MonoBehaviour {
    public GameObject bigCargo;
    public GameObject mediumCargo;
    public GameObject smallCargo;
    public GameObject buildingShip;

    public float bigCargoBuildTime;
    public int bigCargoMetalCost;
    public float mediumCargoBuildTime;
    public int mediumCargoMetalCost;
    public float smallCargoBuildTime;
    public int smallCargoMetalCost;

    public bool online;

    public bool IsBuilding { get; private set; }
    public bool IsBuildingBig { get; private set; }
    public bool IsBuildingMedium { get; private set; }
    public bool IsBuildingSmall { get; private set; }
    public float BuildTimer { get; private set; }

    private MetalCargo cargo;

    private void Start() {
        IsBuilding = false;
        IsBuildingBig = false;
        IsBuildingMedium = false;
        IsBuildingSmall = false;
        BuildTimer = 0;

        cargo = GetComponent<MetalCargo>();
    }

    private void Update() {
        if (online && !IsBuilding) {
            if (FactionsManager.factions[tag].CurrentFactionMediumCargoes < FactionsManager.factions[tag].maxFactionMediumCargoes && cargo.CurrentMetal >= mediumCargoMetalCost) {
                FactionsManager.factions[tag].CurrentFactionMediumCargoes++;
                IsBuilding = true;
                IsBuildingMedium = true;
                cargo.RemoveMetal(mediumCargoMetalCost);
                BuildTimer = mediumCargoBuildTime;
            } else if (FactionsManager.factions[tag].CurrentFactionSmallCargoes < FactionsManager.factions[tag].maxFactionSmallCargoes && cargo.CurrentMetal >= smallCargoMetalCost) {
                FactionsManager.factions[tag].CurrentFactionSmallCargoes++;
                IsBuilding = true;
                IsBuildingSmall = true;
                cargo.RemoveMetal(smallCargoMetalCost);
                BuildTimer = smallCargoBuildTime;
            } else if (FactionsManager.factions[tag].CurrentFactionBigCargoes < FactionsManager.factions[tag].maxFactionBigCargoes && cargo.CurrentMetal >= bigCargoMetalCost) {
                FactionsManager.factions[tag].CurrentFactionBigCargoes++;
                IsBuilding = true;
                IsBuildingBig = true;
                cargo.RemoveMetal(bigCargoMetalCost);
                BuildTimer = bigCargoBuildTime;
            }
        }

        if (online && IsBuilding) {
            BuildTimer -= Time.deltaTime;
            if (BuildTimer <= 0) {
                if (IsBuildingMedium) {
                    GameObject createdShip = Instantiate(mediumCargo);
                    createdShip.tag = tag;
                    createdShip.transform.position = transform.position;

                    IsBuilding = false;
                    IsBuildingMedium = false;
                } else if (IsBuildingSmall) {
                    GameObject createdShip = Instantiate(smallCargo);
                    createdShip.tag = tag;
                    createdShip.transform.position = transform.position;

                    IsBuilding = false;
                    IsBuildingSmall = false;
                } else if (IsBuildingBig) {
                    GameObject createdShip = Instantiate(bigCargo);
                    createdShip.tag = tag;
                    createdShip.transform.position = transform.position;

                    IsBuilding = false;
                    IsBuildingBig = false;
                }
            }
        }
    }

    public void CreateBuildingShip(GameObject structureToBuild, Vector2 structureLocation, float buildingTime) {
        GameObject createdShip = Instantiate(buildingShip);
        createdShip.transform.position = transform.position;
        createdShip.tag = tag;

        BuildingShip attachedBuildingShip = createdShip.GetComponent<BuildingShip>();
        attachedBuildingShip.structureToBuild = structureToBuild;
        attachedBuildingShip.buildingTime = buildingTime;

        createdShip.GetComponent<AiAttributes>().currentManualTarget = structureLocation;
    }
}

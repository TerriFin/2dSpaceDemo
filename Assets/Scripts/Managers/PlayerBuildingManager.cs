using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingManager : MonoBehaviour {
    public static bool isPlayerBuilding;
    public static GameObject Building { get; private set; }

    private void Update() {
        if (isPlayerBuilding && Building != null) {
            print("PLACING BUILDING");
        }
    }

    public static void SetNewBuilding(GameObject selectedBuilding) {
        isPlayerBuilding = true;
        Building = selectedBuilding;
    }

    public static void CancelBuildingPlacement() {
        isPlayerBuilding = false;
    }

    public static void PlaceBuilding(Vector2 pos) {
        isPlayerBuilding = false;

        CommandStructure selectedCommandStructure = SelectionManager.selected[0].GetComponent<CommandStructure>();
        selectedCommandStructure.CreateBuildingShip(Building, pos, Building.GetComponent<Structure>().buildingTime);
        Building = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour {
    public GameObject attachedStructure;
    public Text givenMetalCostText;
    public Text givenMoneyCostText;

    private Button attachedButton;
    private Structure structureComponent;

    private void Start() {
        attachedButton = GetComponent<Button>();
        structureComponent = attachedStructure.GetComponent<Structure>();

        givenMetalCostText.text = "" + structureComponent.buildingMetalCost;
        givenMoneyCostText.text = "" + structureComponent.buildingMoneyCost;

        attachedButton.onClick.AddListener(ClickButton);
    }

    private void ClickButton() {
        PlayerBuildingManager.CancelBuildingPlacement();
        PlayerBuildingManager.SetNewBuilding(attachedStructure);
    }
}

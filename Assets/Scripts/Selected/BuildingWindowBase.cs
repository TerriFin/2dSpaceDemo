using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingWindowBase : MonoBehaviour {
    public Button selectedReverseButton;
    public Text selectedCargoText;

    private MetalCargo attachedMetalCargo;

    private void Start() {
        selectedReverseButton.onClick.AddListener(ReverseButtonClick);

        attachedMetalCargo = SelectionManager.selected[0].GetComponent<MetalCargo>();
    }

    private void LateUpdate() {
        selectedCargoText.text = "" + attachedMetalCargo.CurrentMetal;
    }

    private void ReverseButtonClick() {
        SelectionManager.UpdateDescription();
        PlayerBuildingManager.CancelBuildingPlacement();
    }
}

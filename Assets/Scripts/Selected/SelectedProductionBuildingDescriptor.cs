using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedProductionBuildingDescriptor : MonoBehaviour, DescriptorUpdater {
    public Image selectedSpriteImage;
    public Text selectedAmountText;
    public Text selectedNameText;
    public Text selectedHitpointsText;
    public Text selectedMetalCargoText;
    public Text selectedMetalPriceText;
    public Text selectedBasePriceText;
    public Button selectedMinusButton;
    public Button selectedPlusButton;
    public Button selectedResetRallyButton;

    private Selectable attachedSelectable;

    private Hitpoints attachedHitpoints;
    private MetalCargo attachedMetalCargo;
    private BuyingStructure attachedBuyingStructure;

    private void LateUpdate() {
        if (attachedHitpoints != null) {
            selectedHitpointsText.text = attachedHitpoints.CurrentHp + "/" + attachedHitpoints.maxHp;
        }

        if (attachedMetalCargo != null) {
            selectedMetalCargoText.text = attachedMetalCargo.CurrentMetal + "/" + attachedMetalCargo.maxMetal;
        }

        if (attachedBuyingStructure != null) {
            selectedMetalPriceText.text = "" + attachedBuyingStructure.MetalPrice;
            selectedBasePriceText.text = "" + attachedBuyingStructure.baseMetalPrice;
        }
    }

    public void SetSelectable(Selectable selected) {
        attachedSelectable = selected;
    }

    public void GetRequiredComponentsFromSelectable() {
        selectedSpriteImage.sprite = attachedSelectable.gameObject.GetComponent<SpriteRenderer>().sprite;
        selectedAmountText.text = "" + SelectionManager.selected.Count;
        selectedNameText.text = attachedSelectable.selectableName;

        attachedHitpoints = attachedSelectable.gameObject.GetComponent<Hitpoints>();
        attachedMetalCargo = attachedSelectable.gameObject.GetComponent<MetalCargo>();
        attachedBuyingStructure = attachedSelectable.gameObject.GetComponent<BuyingStructure>();
    }

    public void CheckAndSetFactionChanges(string faction) {
        if (!FactionsManager.playerFaction.factionTag.Equals(faction)) {
            selectedMinusButton.enabled = false;
            selectedMinusButton.image.enabled = false;

            selectedPlusButton.enabled = false;
            selectedPlusButton.image.enabled = false;

            selectedResetRallyButton.enabled = false;
            selectedResetRallyButton.image.enabled = false;
        } else {
            selectedMinusButton.onClick.AddListener(DecreaseAllSelectedBuyingStructurePrices);
            selectedPlusButton.onClick.AddListener(IncreaseAllSelectedBuyingStructurePrices);

            selectedResetRallyButton.onClick.AddListener(ResetAllRallyPoints);
        }
    }

    private void ResetAllRallyPoints() {
        List<Selectable> currentlySelected = SelectionManager.selected;

        foreach (Selectable current in currentlySelected) {
            current.GetComponent<ProductionStructureNoLimit>().ResetRallyPoint();
        }
    }

    private void DecreaseAllSelectedBuyingStructurePrices() {
        List<Selectable> currentlySelected = SelectionManager.selected;

        foreach (Selectable current in currentlySelected) {
            current.GetComponent<BuyingStructure>().DecreaseBaseMetalPrice();
        }
    }

    private void IncreaseAllSelectedBuyingStructurePrices() {
        List<Selectable> currentlySelected = SelectionManager.selected;

        foreach (Selectable current in currentlySelected) {
            current.GetComponent<BuyingStructure>().IncreaseBaseMetalPrice();
        }
    }
}

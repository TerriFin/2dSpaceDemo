using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedSellingStructureUpdater : MonoBehaviour, DescriptorUpdater {
    public Image selectedSpriteImage;
    public Text selectedAmountText;
    public Text selectedNameText;
    public Text selectedHitpointsText;
    public Text selectedMetalCargoText;
    public Text selectedMetalPriceText;
    public Text selectedBasePriceText;
    public Button selectedMinusButton;
    public Button selectedPlusButton;

    private Selectable attachedSelectable;

    private Hitpoints attachedHitpoints;
    private MetalCargo attachedMetalCargo;
    private SellingStructure attachedSellingStructure;

    private void LateUpdate() {
        if (attachedHitpoints != null) {
            selectedHitpointsText.text = attachedHitpoints.CurrentHp + "/" + attachedHitpoints.maxHp;
        }

        if (attachedMetalCargo != null) {
            selectedMetalCargoText.text = attachedMetalCargo.CurrentMetal + "/" + attachedMetalCargo.maxMetal;
        }

        if (attachedSellingStructure != null) {
            selectedMetalPriceText.text = "" + attachedSellingStructure.MetalPrice;
            selectedBasePriceText.text = "" + attachedSellingStructure.baseMetalPrice;
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
        attachedSellingStructure = attachedSelectable.gameObject.GetComponent<SellingStructure>();
    }

    public void CheckAndSetFactionChanges(string faction) {
        if (!FactionsManager.playerFaction.factionTag.Equals(faction)) {
            selectedMinusButton.enabled = false;
            selectedMinusButton.image.enabled = false;

            selectedPlusButton.enabled = false;
            selectedPlusButton.image.enabled = false;
        } else {
            selectedMinusButton.onClick.AddListener(DecreaseAllSelectedSellingStructurePrices);
            selectedPlusButton.onClick.AddListener(IncreaseAllSelectedSellingStructurePrices);
        }
    }

    private void DecreaseAllSelectedSellingStructurePrices() {
        List<Selectable> currentlySelected = SelectionManager.selected;

        foreach (Selectable current in currentlySelected) {
            current.GetComponent<SellingStructure>().DecreaseBaseMetalPrice();
        }
    }

    private void IncreaseAllSelectedSellingStructurePrices() {
        List<Selectable> currentlySelected = SelectionManager.selected;

        foreach (Selectable current in currentlySelected) {
            current.GetComponent<SellingStructure>().IncreaseBaseMetalPrice();
        }
    }
}
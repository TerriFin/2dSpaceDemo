using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedShipDescriptorUpdater : MonoBehaviour, DescriptorUpdater {
    public Image selectedSpriteImage;
    public Text selectedAmountText;
    public Text selectedNameText;
    public Text selectedHitpointsText;
    public Text selectedMetalCargoText;

    private Selectable attachedSelectable;

    private Hitpoints attachedHitpoints;
    private MetalCargo attachedMetalCargo;

    private void LateUpdate() {
        if (attachedHitpoints != null) {
            selectedHitpointsText.text = attachedHitpoints.CurrentHp + "/" + attachedHitpoints.maxHp;
        }

        if (attachedMetalCargo != null) {
            selectedMetalCargoText.text = attachedMetalCargo.CurrentMetal + "/" + attachedMetalCargo.maxMetal;
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
    }

    public void CheckAndSetFactionChanges(string faction) {
        
    }
}

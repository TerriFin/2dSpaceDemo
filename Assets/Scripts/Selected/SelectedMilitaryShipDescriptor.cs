using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedMilitaryShipDescriptor : MonoBehaviour, DescriptorUpdater {
    public Image selectedSpriteImage;
    public Text selectedAmountText;
    public Text selectedNameText;
    public Text selectedHitpointsText;
    public Toggle automationToggle;
    public Toggle attackToggle;

    private Selectable attachedSelectable;

    private Hitpoints attachedHitpoints;

    private void LateUpdate() {
        if (attachedHitpoints != null) {
            selectedHitpointsText.text = attachedHitpoints.CurrentHp + "/" + attachedHitpoints.maxHp;
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
    }

    public void CheckAndSetFactionChanges(string faction) {
        MilitaryAi attachedMilitaryAi = attachedSelectable.GetComponent<MilitaryAi>();

        automationToggle.isOn = attachedMilitaryAi.automate;
        attackToggle.isOn = attachedMilitaryAi.attackOnSight;

        if (!FactionsManager.playerFaction.factionTag.Equals(faction)) {
            automationToggle.enabled = false;
            automationToggle.image.enabled = false;

            attackToggle.enabled = false;
            attackToggle.image.enabled = false;
        } else {
            automationToggle.onValueChanged.AddListener(AutomateSelectedMilitaryShips);
            attackToggle.onValueChanged.AddListener(AutomateAttackSelectedMilitaryShips);
        }
    }

    private void AutomateSelectedMilitaryShips(bool value) {
        List<Selectable> currentSelected = SelectionManager.selected;

        foreach (Selectable current in currentSelected) {
            current.GetComponent<MilitaryAi>().automate = value;
        }
    }

    private void AutomateAttackSelectedMilitaryShips(bool value) {
        List<Selectable> currentSelected = SelectionManager.selected;

        foreach (Selectable current in currentSelected) {
            current.GetComponent<MilitaryAi>().attackOnSight = value;
        }
    }
}

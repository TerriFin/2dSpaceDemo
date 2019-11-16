using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedAsteroidUpdater : MonoBehaviour, DescriptorUpdater {
    public Image selectedSpriteImage;
    public Text selectedAmountText;
    public Text selectedNameText;
    public Text selectedHitpointsText;

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

    }
}

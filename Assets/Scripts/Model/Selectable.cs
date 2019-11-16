using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {

    public string selectableName;

    public SpriteRenderer selectedCrossRenderer;
    public HealthBar healthBar;
    public SpriteRenderer rallypointRenderer;

    public bool selected;
    public GameObject descriptorBox;

    public RightClick RightClickFunctionality { get; private set; }

    private void Start() {
        selectedCrossRenderer.enabled = false;
        if (rallypointRenderer != null) {
            rallypointRenderer.enabled = false;
        }
        selected = false;

        RightClickFunctionality = GetComponent<RightClick>();
    }

    private void OnDestroy() {
        SelectionManager.DeselectSelectable(this);
    }

    public void FlipSelected() {
        selected = !selected;

        if (selected) {
            selectedCrossRenderer.enabled = true;
            if (healthBar != null) {
                healthBar.Show();
            }

            if (rallypointRenderer != null) {
                rallypointRenderer.enabled = true;
            }
        } else {
            selectedCrossRenderer.enabled = false;
            if (healthBar != null) {
                healthBar.DontShow();
            }

            if (rallypointRenderer != null) {
                rallypointRenderer.enabled = false;
            }
        }
    }

    public GameObject GetDescriptorBox() {
        GameObject leftPaneDescriptor = Instantiate(descriptorBox);

        DescriptorUpdater updater = leftPaneDescriptor.GetComponent<DescriptorUpdater>();
        updater.SetSelectable(this);
        updater.GetRequiredComponentsFromSelectable();
        updater.CheckAndSetFactionChanges(tag);

        return leftPaneDescriptor;
    }
}

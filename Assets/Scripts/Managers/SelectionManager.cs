using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour {
    public GameObject leftPane;

    public static List<Selectable> selected;

    private static GameObject staticLeftPane;
    private static GameObject currentDescriptor;

    private void Start() {
        staticLeftPane = leftPane;
        selected = new List<Selectable>();
    }

    public static void UpdateDescription() {
        // Check if null so that when game ends it will not try to access leftHandPane
        if (staticLeftPane != null) {
            if (selected.Count == 0) {
                Destroy(currentDescriptor);
                currentDescriptor = null;
            } else {
                Destroy(currentDescriptor);
                currentDescriptor = selected[0].GetDescriptorBox();
                currentDescriptor.transform.SetParent(staticLeftPane.transform, false);
            }
        }
    }

    public static void SetDescription(GameObject givenDescription) {
        if (staticLeftPane != null) {
            Destroy(currentDescriptor);
            currentDescriptor = givenDescription;
            currentDescriptor.transform.SetParent(staticLeftPane.transform, false);
        }
    }

    // This one is not static for the button
    public void ButtonDeselectSelected() {
        foreach (Selectable currentSelected in selected) {
            currentSelected.FlipSelected();
        }

        selected = new List<Selectable>();
        UpdateDescription();
    }

    // This one is static so that it can be accessed from static methods, also does not update description box since this is only called from HandleClickOnCollider, and it updates
    public static void DeselectSelected() {
        foreach (Selectable currentSelected in selected) {
            currentSelected.FlipSelected();
        }

        selected = new List<Selectable>();
    }

    // This one is so that when a thing is destroyed, this can be called
    public static void DeselectSelectable(Selectable currentSelectable) {
        if (selected.Count > 0) {
            if (selected[0].gameObject == currentSelectable.gameObject) {
                if (selected[0].GetComponent<CommandStructure>() != null) {
                    PlayerBuildingManager.CancelBuildingPlacement();
                }
                selected.Remove(currentSelectable);
                UpdateDescription();
            } else {
                selected.Remove(currentSelectable);
            }
        }
    }

    public static void HandleRightClick(Vector2 clickedPos, string faction) {
        if (selected.Count > 0) {
            if (PlayerBuildingManager.isPlayerBuilding) {
                PlayerBuildingManager.PlaceBuilding(clickedPos);
            } else {
                if (selected[0].RightClickFunctionality != null) {
                    foreach (Selectable current in selected) {
                        current.RightClickFunctionality.HandleRightClick(clickedPos, faction);
                    }
                }
            }
        }
    }

    public static void HandleClickOnCollider(Collider2D clickedCollider) {
        Selectable currentSelected = clickedCollider.GetComponent<Selectable>();

        // If the selected collider has selectable component
        if (currentSelected != null) {
            // Check if the selected thing belongs to player faction
            if (FactionsManager.playerFaction.factionTag.Equals(currentSelected.tag)) {
                // Check if anything else is selected, if so, check if the new selected is of the player faction, and if so, that it is of the same type of selectable
                if (selected.Count != 0 && selected[0].tag.Equals(FactionsManager.playerFaction.factionTag) && selected[0].selectableName.Equals(currentSelected.selectableName)) {
                    // If previous conditions are filled, we then check if the selected thing is already selected, if so, we select all of the same type on screen
                    if (!selected.Contains(currentSelected)) {
                        // Add currentSelected to selected list as it is of same type, player faction and is not yet present in selected list
                        selected.Add(currentSelected);
                        currentSelected.FlipSelected();
                    } else {
                        // Select all of the same type on screen
                        SelectAllSameSelectableInScreen(currentSelected);
                    }
                } else {
                    // This is run if the current selected is first on selected list, if it is not of the player faction or different type than other selected things
                    SelectOnlyGiven(currentSelected);
                }
            } else {
                // If it does not belong to player faction, only it can be selected, so we call selectOnlyGiven -method,
                // Which deselects all previously selected things, and selects the given selectable -component
                SelectOnlyGiven(currentSelected);
            }
        }

        UpdateDescription();
    }

    private static void SelectOnlyGiven(Selectable currentSelected) {
        DeselectSelected();
        selected.Add(currentSelected);
        currentSelected.FlipSelected();
    }

    private static void SelectAllSameSelectableInScreen(Selectable currentSelectable) {
        DeselectSelected();
        Collider2D[] allVisibleColliders = Physics2D.OverlapAreaAll(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight)));

        foreach (Collider2D collider in allVisibleColliders) {
            Selectable colliderSelectable = collider.GetComponent<Selectable>();
            if (colliderSelectable != null && colliderSelectable.selectableName.Equals(currentSelectable.selectableName) && !selected.Contains(colliderSelectable)) {
                selected.Add(colliderSelectable);
                colliderSelectable.FlipSelected();
            }
        }
    }
}

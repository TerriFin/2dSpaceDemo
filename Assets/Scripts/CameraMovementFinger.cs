using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementFinger : MonoBehaviour {
    public float movementSpeed;

    private void Update() {
        if (Input.touchCount == 1) {
            if (Input.GetTouch(0).position.x > Camera.main.pixelWidth * 0.3f) {
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    Vector2 tappedPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                    Collider2D selectedCollider = Physics2D.OverlapPoint(tappedPoint);
                    if (selectedCollider != null) {
                        SelectionManager.HandleClickOnCollider(selectedCollider);
                    } else {
                        SelectionManager.HandleRightClick(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), FactionsManager.playerFaction.factionTag);
                    }
                }
            }
            
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * movementSpeed * Time.deltaTime, -touchDeltaPosition.y * movementSpeed * Time.deltaTime, 0);
        }

        if (Input.touchCount >= 3) {
            if (RelationshipManager.AreFactionsInWar("Faction1", "Faction2")) {
                RelationshipManager.EndWar("Faction1", "Faction2");
                RelationshipManager.EndWar("Faction2", "Faction1");

                RelationshipManager.EndWar("Faction1", "Faction3");
                RelationshipManager.EndWar("Faction3", "Faction1");

                RelationshipManager.EndWar("Faction3", "Faction2");
                RelationshipManager.EndWar("Faction2", "Faction3");
            } else {
                RelationshipManager.StartWar("Faction1", "Faction2");
                RelationshipManager.StartWar("Faction2", "Faction1");

                RelationshipManager.StartWar("Faction1", "Faction3");
                RelationshipManager.StartWar("Faction3", "Faction1");

                RelationshipManager.StartWar("Faction3", "Faction2");
                RelationshipManager.StartWar("Faction2", "Faction3");
            }
        }
    }
}

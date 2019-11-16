using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {
    
    public float checkTime;

    private float checkTimer;
    private Sensors attachedSensors;

    private void Start() {
        checkTimer = Time.time + checkTime;
        attachedSensors = GetComponent<Sensors>();
    }

    private void Update() {
        if (Time.time >= checkTimer) {
            checkTimer += checkTime;
            string dominantFactionTag = giveDominantFactionTag();
            if (dominantFactionTag != null) {
                tag = dominantFactionTag;
            }
        }
    }

    private string giveDominantFactionTag() {
        string returnValue = null;
        if (tag.Equals("Untagged")) {
            IDictionary<string, int> factionMilitaryShipsPresent = new Dictionary<string, int>();
            foreach (string key in FactionsManager.factions.Keys) {
                factionMilitaryShipsPresent[key] = 0;
            }

            foreach (Collider2D collider in attachedSensors.NearbyColliders) {
                if (collider != null) {
                    if (collider.gameObject.layer == 9) {
                        factionMilitaryShipsPresent[collider.tag]++;
                    }
                }
            }

            int mostMilitaryShips = 0;
            foreach (string key in factionMilitaryShipsPresent.Keys) {
                if (factionMilitaryShipsPresent[key] > mostMilitaryShips) {
                    returnValue = key;
                    mostMilitaryShips = factionMilitaryShipsPresent[key];
                }
            }

        } else {
            foreach (Collider2D collider in attachedSensors.NearbyColliders) {
                if (collider != null) {
                    if (collider.gameObject.layer == 9) {
                        if (tag.Equals(collider.tag)) {
                            returnValue = null;
                            break;
                        } else if (RelationshipManager.AreFactionsInWar(tag, collider.tag)) {
                            if (returnValue == null || returnValue.Equals(collider.tag)) {
                                returnValue = collider.tag;
                            } else {
                                returnValue = null;
                                break;
                            }
                        }
                    }
                }
            }
        }

        return returnValue;
    }
}



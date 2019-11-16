using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour {

    private void Awake() {
        FactionsManager.SetFactions();
        RelationshipManager.InitializeRelations();
        ShipsManager.SetFactions();
        BuildingsManager.SetFactions();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            print("STARTING WAR BETWEEN ALL FACTIONS!");
            RelationshipManager.StartWar("Faction1", "Faction2");
            RelationshipManager.StartWar("Faction2", "Faction1");

            RelationshipManager.StartWar("Faction1", "Faction3");
            RelationshipManager.StartWar("Faction3", "Faction1");

            RelationshipManager.StartWar("Faction3", "Faction2");
            RelationshipManager.StartWar("Faction2", "Faction3");
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            print("ENDING WAR BETWEEN ALL FACTIONS!");
            RelationshipManager.EndWar("Faction1", "Faction2");
            RelationshipManager.EndWar("Faction2", "Faction1");

            RelationshipManager.EndWar("Faction1", "Faction3");
            RelationshipManager.EndWar("Faction3", "Faction1");

            RelationshipManager.EndWar("Faction3", "Faction2");
            RelationshipManager.EndWar("Faction2", "Faction3");
        }
    }
}

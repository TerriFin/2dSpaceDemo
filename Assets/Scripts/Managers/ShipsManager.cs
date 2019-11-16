using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsManager : MonoBehaviour {
    public static IDictionary<string, List<Ship>> factionMilitaryShips;
    public static IDictionary<string, List<Ship>> factionCivilianShips;

    public static void SetFactions() {
        factionMilitaryShips = new Dictionary<string, List<Ship>>();
        factionCivilianShips = new Dictionary<string, List<Ship>>();

        foreach (string faction in FactionsManager.factions.Keys) {
            factionMilitaryShips[faction] = new List<Ship>();
            factionCivilianShips[faction] = new List<Ship>();
        }
    }

    public static void AddShip(Ship ship) {
        if (FactionsManager.factions.Keys.Contains(ship.tag)) {
            if (ship.gameObject.layer == 9) {
                factionMilitaryShips[ship.tag].Add(ship);
            } else {
                factionCivilianShips[ship.tag].Add(ship);
            }
        }
    }

    public static void RemoveShip(Ship ship) {
        if (FactionsManager.factions.Keys.Contains(ship.tag)) {
            if (ship.gameObject.layer == 9) {
                factionMilitaryShips[ship.tag].Remove(ship);
            } else {
                factionCivilianShips[ship.tag].Remove(ship);
            }
        }
    }
}

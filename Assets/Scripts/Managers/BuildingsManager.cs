using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour {

    // Dictionary that stores all faction buildings by their faction tag
    public static IDictionary<string, List<Structure>> factionBuildings;

    // Two lists that store all production and manufacturing buildings.
    public static List<SellingStructure> sellingStructures;
    public static List<BuyingStructure> buyingStructures;

    public static void SetFactions() {
        factionBuildings = new Dictionary<string, List<Structure>>();

        sellingStructures = new List<SellingStructure>();
        buyingStructures = new List<BuyingStructure>();

        // Make a seperate key and list for each faction that is initialized
        foreach (string faction in FactionsManager.factions.Keys) {
            factionBuildings[faction] = new List<Structure>();
        }
    }

    public static void AddBuilding(Structure structure) {
        if (FactionsManager.factions.Keys.Contains(structure.tag)) {
            if (structure.sellsMetal) {
                sellingStructures.Add(structure.GetComponent<SellingStructure>());
            } else if (structure.buysMetal) {
                buyingStructures.Add(structure.GetComponent<BuyingStructure>());
            }

            factionBuildings[structure.tag].Add(structure);
        }
    }

    public static void RemoveBuilding(Structure structure) {
        if (FactionsManager.factions.Keys.Contains(structure.tag)) {
            if (structure.sellsMetal) {
                sellingStructures.Remove(structure.GetComponent<SellingStructure>());
            } else if (structure.buysMetal) {
                buyingStructures.Remove(structure.GetComponent<BuyingStructure>());
            }

            factionBuildings[structure.tag].Remove(structure);
        }
    }
}

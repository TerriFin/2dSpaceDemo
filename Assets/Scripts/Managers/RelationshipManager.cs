using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationshipManager : MonoBehaviour {

    private static IDictionary<string, RelationShip> factionRelationships;

    public static void InitializeRelations() {
        print("RELATIONS INITIALIZED");
        factionRelationships = new Dictionary<string, RelationShip>();

        foreach (string faction in FactionsManager.factions.Keys) {
            factionRelationships[faction] = new RelationShip();
            
        }
    }

    public static void StartWar(string firstFactionTag, string secondFactionTag) {
        if (!factionRelationships[firstFactionTag].startedWars.Contains(secondFactionTag)) {
            factionRelationships[firstFactionTag].startedWars.Add(secondFactionTag);
            factionRelationships[secondFactionTag].targetOfWars.Add(firstFactionTag);

            factionRelationships[firstFactionTag].wars.Add(secondFactionTag);
            factionRelationships[secondFactionTag].wars.Add(firstFactionTag);

            StartBlockade(firstFactionTag, secondFactionTag);
        }
    }

    public static void EndWar(string firstFactionTag, string secondFactionTag) {
        factionRelationships[firstFactionTag].startedWars.Remove(secondFactionTag);
        factionRelationships[secondFactionTag].targetOfWars.Remove(firstFactionTag);

        if (!AreFactionsInWar(firstFactionTag, secondFactionTag)) {
            factionRelationships[firstFactionTag].wars.Remove(secondFactionTag);
            factionRelationships[secondFactionTag].wars.Remove(firstFactionTag);
            // If this code is ran, it means that the war ender was the last to declare peace, but because the other guy was in war, they could not stop blockading.
            EndBlockade(secondFactionTag, firstFactionTag);
        }

        EndBlockade(firstFactionTag, secondFactionTag);
    }

    public static void StartBlockade(string firstFactionTag, string secondFactionTag) {
        if (!factionRelationships[firstFactionTag].startedBlockades.Contains(secondFactionTag)) {
            factionRelationships[firstFactionTag].startedBlockades.Add(secondFactionTag);
            factionRelationships[secondFactionTag].targetOfBlockades.Add(firstFactionTag);

            factionRelationships[firstFactionTag].blockades.Add(secondFactionTag);
            factionRelationships[secondFactionTag].blockades.Add(firstFactionTag);
        }
    }

    public static void EndBlockade(string firstFactionTag, string secondFactionTag) {
        if (!AreFactionsInWar(firstFactionTag, secondFactionTag)) {
            factionRelationships[firstFactionTag].startedBlockades.Remove(secondFactionTag);
            factionRelationships[secondFactionTag].targetOfBlockades.Remove(firstFactionTag);

            if (!IsBlockading(firstFactionTag, secondFactionTag)) {
                factionRelationships[firstFactionTag].blockades.Remove(secondFactionTag);
                factionRelationships[secondFactionTag].blockades.Remove(firstFactionTag);
            }
        }
    }

    public static HashSet<string> GetFactionsFactionIsAtWarWith(string factionTag) {
        return factionRelationships[factionTag].wars;
    }

    public static bool AreFactionsInWar(string firstFactionTag, string secondFactionTag) {
        try {
            return factionRelationships[firstFactionTag].startedWars.Contains(secondFactionTag) || factionRelationships[firstFactionTag].targetOfWars.Contains(secondFactionTag);
        } catch (Exception) {
            return false;
        }
    }

    public static bool IsBlockading(string firstFactionTag, string secondFactionTag) {
        try {
            return factionRelationships[firstFactionTag].startedBlockades.Contains(secondFactionTag) || factionRelationships[firstFactionTag].targetOfBlockades.Contains(secondFactionTag);
        } catch (Exception) {
            return false;
        }
    }

    public static bool IsFactionInWar(string factionTag) {
        return factionRelationships[factionTag].startedWars.Count > 0 || factionRelationships[factionTag].targetOfWars.Count > 0;
    }
}

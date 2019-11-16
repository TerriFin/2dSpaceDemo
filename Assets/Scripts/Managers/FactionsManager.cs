using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionsManager : MonoBehaviour {

    public static IDictionary<string, Faction> factions;
    public static Faction playerFaction;

    public static void SetFactions() {
        factions = new Dictionary<string, Faction>();
        playerFaction = null;

        Faction[] factionsData = GameObject.FindGameObjectWithTag("FactionsManager").GetComponents<Faction>();

        for (int i = 0; i < factionsData.Length; i++) {
            print("CREATED FACTION " + factionsData[i].factionTag);
            factions[factionsData[i].factionTag] = factionsData[i];

            if (factionsData[i].player) {
                playerFaction = factionsData[i];
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoIdentifier : MonoBehaviour {
    public bool bigCargo;
    public bool mediumCargo;
    public bool smallCargo;

    private void OnDestroy() {
        if (bigCargo) {
            FactionsManager.factions[tag].CurrentFactionBigCargoes--;
        } else if (mediumCargo) {
            FactionsManager.factions[tag].CurrentFactionMediumCargoes--;
        } else if (smallCargo) {
            FactionsManager.factions[tag].CurrentFactionSmallCargoes--;
        }
    }
}

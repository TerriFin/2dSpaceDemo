using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedProductionStructureTag : MonoBehaviour {
    public ProductionStructureLimit factory;

    private void OnDestroy() {
        if (factory != null) {
            factory.shipDestroyed();
        }
    }
}

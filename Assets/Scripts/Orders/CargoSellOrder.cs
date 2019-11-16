using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoSellOrder : MonoBehaviour, Order {

    private AiAttributes aiAttributes;

    BuyingStructure sellingTo;

    private void Start() {
        aiAttributes = GetComponent<AiAttributes>();

        sellingTo = getBestDeal();
    }

    public bool UpdateOrder() {
        if (sellingTo != null) {
            if (MoveOrder.move(sellingTo.transform.position, aiAttributes.AttachedShip.speed, aiAttributes.AttachedShip.rotationSpeed, 0.2f, 1f, aiAttributes.AttachedRigidBody, gameObject)) {
                sellingTo.BuyFromShip(aiAttributes.AttachedShip, aiAttributes.AttachedShip.Cargo.CurrentMetal);
                return true;
            } else {
                return false;
            }
        } else {
            return true;
        }
    }

    public void DestroyOrder() {
        Destroy(this);
    }

    private BuyingStructure getBestDeal() {
        List<BuyingStructure> allBuildings = BuildingsManager.buyingStructures;
        int currentBestPrice = int.MaxValue;
        BuyingStructure best = null;

        foreach (BuyingStructure current in allBuildings) {
            if (current != null) {
                // DistanceToBuilding is how much distance to current building affects outcome
                float distanceToBuilding = Vector2.Distance(aiAttributes.AttachedShip.transform.position, current.transform.position) * aiAttributes.distanceToBuildingModifier;
                // Calculate currentPrice by getting metal price in the building and modifying it by distanceToBuilding
                int currentPrice = Mathf.FloorToInt(current.MetalPrice - distanceToBuilding);

                // Add preference towards own faction
                if (aiAttributes.AttachedShip.tag.Equals(current.tag)) {
                    currentPrice = Mathf.FloorToInt(currentPrice * (1 + aiAttributes.ownFactionTradeBiasModifier));
                }

                if ((best == null || currentBestPrice < currentPrice) &&
                    !RelationshipManager.IsBlockading(current.tag, aiAttributes.AttachedShip.tag) &&
                    (FactionsManager.factions[current.tag].money >= current.MetalPrice * aiAttributes.AttachedShip.Cargo.CurrentMetal || current.tag.Equals(aiAttributes.AttachedShip.tag)) &&
                    current.Cargo.GetCurrentFreeCargo() >= aiAttributes.AttachedShip.Cargo.CurrentMetal) {

                    if (best == null || Random.value > aiAttributes.randomness) {
                        currentBestPrice = currentPrice;
                        best = current;
                    }
                }
            }
        }

        return best;
    }
}

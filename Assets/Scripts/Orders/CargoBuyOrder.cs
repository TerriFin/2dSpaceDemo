using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoBuyOrder : MonoBehaviour, Order {

    private AiAttributes aiAttributes;

    private SellingStructure buyingFrom;

    private void Start() {
        aiAttributes = GetComponent<AiAttributes>();

        buyingFrom = getBestDeal();
    }

    public bool UpdateOrder() {
        if (buyingFrom != null) {
            if (MoveOrder.move(buyingFrom.transform.position, aiAttributes.AttachedShip.speed, aiAttributes.AttachedShip.rotationSpeed, 0.2f, 1f, aiAttributes.AttachedRigidBody, gameObject)) {
                buyingFrom.SellToShip(aiAttributes.AttachedShip, aiAttributes.AttachedShip.Cargo.GetCurrentFreeCargo());
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

    private SellingStructure getBestDeal() {
        List<SellingStructure> allBuildings = BuildingsManager.sellingStructures;
        int currentBestPrice = int.MaxValue;
        SellingStructure best = null;

        foreach (SellingStructure current in allBuildings) {
            if (current != null) {
                // DistanceToBuilding is how much distance to current building affects outcome
                float distanceToBuilding = Vector2.Distance(aiAttributes.AttachedShip.transform.position, current.transform.position) * aiAttributes.distanceToBuildingModifier;
                // Calculate currentPrice by getting metal price in the building and modifying it by distanceToBuilding
                int currentPrice = Mathf.FloorToInt(current.MetalPrice + distanceToBuilding);

                // Add preference towards own faction
                if (!aiAttributes.AttachedShip.tag.Equals(current.tag)) {
                    currentPrice = Mathf.FloorToInt(currentPrice * (1 + aiAttributes.ownFactionTradeBiasModifier));
                }

                if ((best == null || currentBestPrice > currentPrice) &&
                    !RelationshipManager.IsBlockading(current.tag, aiAttributes.AttachedShip.tag) &&
                    current.Cargo.CurrentMetal >= aiAttributes.AttachedShip.Cargo.GetCurrentFreeCargo() / 2 &&
                    (FactionsManager.factions[aiAttributes.AttachedShip.tag].money >= aiAttributes.AttachedShip.Cargo.GetCurrentFreeCargo() * current.MetalPrice || current.tag.Equals(aiAttributes.AttachedShip.tag))) {

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
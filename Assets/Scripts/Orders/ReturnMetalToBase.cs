using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnMetalToBase : MonoBehaviour, Order {

    AiAttributes aiAttributes;
    LimitedProductionStructureTag attachedFactory;
    MetalCargo attachedFactoryCargo;

    private void Start() {
        aiAttributes = GetComponent<AiAttributes>();
        attachedFactory = GetComponent<LimitedProductionStructureTag>();
        attachedFactoryCargo = attachedFactory.factory.GetComponent<MetalCargo>();
    }

    public void DestroyOrder() {
        Destroy(this);
    }

    public bool UpdateOrder() {
        if (attachedFactory.factory != null) {
            if (MoveOrder.move(attachedFactory.factory.transform.position, aiAttributes.AttachedShip.speed, aiAttributes.AttachedShip.rotationSpeed, 0.2f, 1f, aiAttributes.AttachedRigidBody, gameObject)) {
                if (aiAttributes.AttachedShip.Cargo.SellMetal(attachedFactoryCargo, aiAttributes.AttachedShip.Cargo.CurrentMetal)) {
                    // Return true if the transaction is succesfull
                    return true;
                }
            } else {
                // Return false if attached factory is destroyed
                return false;
            }
        }

        return true;
    }
}

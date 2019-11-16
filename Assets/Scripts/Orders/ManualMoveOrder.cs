using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualMoveOrder : MonoBehaviour, Order {
    private AiAttributes aiAttributes;

    public bool UpdateOrder() {
        if (aiAttributes == null) {
            aiAttributes = GetComponent<AiAttributes>();
        }

        return MoveOrder.move(aiAttributes.currentManualTarget, aiAttributes.AttachedShip.speed, aiAttributes.AttachedShip.rotationSpeed, 0.2f, 0.5f, aiAttributes.AttachedRigidBody, gameObject);
    }

    public void DestroyOrder() {
        Destroy(this);
    }
}

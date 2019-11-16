using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOrder : MonoBehaviour, Order {

    private AiAttributes aiAttributes;

    private Collider2D currentTarget;

    private Vector2 targetPos;

    private void Start() {
        aiAttributes = GetComponent<AiAttributes>();

        currentTarget = getClosestEnemy();

        if (currentTarget != null) {
            // Get the direction the ships needs to go (away from target)
            targetPos = transform.position - currentTarget.transform.position;
            
            // Normalize it so that x and y put together are 1 but still preserving the direction
            targetPos.Normalize();
            
            // Multiply with preferred combat distance so that the ship tries to stay there
            targetPos *= aiAttributes.preferredCombatDistance;

            // Check if the ship is too far away from preferred distance, flip the value if so
            if (Vector2.Distance(transform.position, currentTarget.transform.position) >= aiAttributes.preferredCombatDistance) {
                targetPos = -targetPos;
            }

            // Add own position so that the ship moves relative to own position
            targetPos += (Vector2) transform.position;
        }
    }

    public bool UpdateOrder() {
        if (currentTarget != null) {
            if (MoveOrder.move(targetPos, aiAttributes.AttachedShip.speed, aiAttributes.AttachedShip.rotationSpeed, 0.2f, 0.5f, aiAttributes.AttachedRigidBody, gameObject) ||
                Vector2.Distance(transform.position, currentTarget.transform.position) > aiAttributes.AttachedSensors.checkRadius) {
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

    private Collider2D getClosestEnemy() {
        Collider2D currentClosest = null;
        float currentClosestEnemy = float.MaxValue;
        foreach (Collider2D collider in aiAttributes.AttachedSensors.NearbyColliders) {
            if (collider != null) {
                if (RelationshipManager.AreFactionsInWar(gameObject.tag, collider.tag)) {
                    float currentColliderDistance = Vector2.Distance(gameObject.transform.position, collider.transform.position);
                    if (currentClosest == null || currentClosestEnemy > currentColliderDistance) {
                        currentClosest = collider;
                        currentClosestEnemy = currentColliderDistance;
                    }
                }
            }
        }

        return currentClosest;
    }
}

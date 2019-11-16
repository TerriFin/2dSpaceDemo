using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeOrder : MonoBehaviour, Order {

    private AiAttributes aiAttributes;

    private Vector2 targetPos;
    private Collider2D closestEnemy;

    private void Start() {
        aiAttributes = GetComponent<AiAttributes>();

        closestEnemy = getClosestEnemy();
        if (closestEnemy != null) {
            targetPos = transform.position - closestEnemy.transform.position;
            targetPos.Normalize();
            targetPos *= aiAttributes.fleeDistance;
            targetPos += (Vector2) transform.position;
        }
    }

    public bool UpdateOrder() {
        if (closestEnemy != null) {
            return MoveOrder.move(targetPos, aiAttributes.AttachedShip.speed, aiAttributes.AttachedShip.rotationSpeed, 0.2f, 0.5f, aiAttributes.AttachedRigidBody, gameObject);
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
                if (RelationshipManager.AreFactionsInWar(gameObject.tag, collider.tag) && collider.gameObject.layer == 9) {
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

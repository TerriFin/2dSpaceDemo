using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolShipsOrder : MonoBehaviour, Order {

    private AiAttributes aiAttributes;

    private Ship patrolTarget;

    private float waitingTimer;

    private void Start() {
        aiAttributes = GetComponent<AiAttributes>();

        patrolTarget = getPatrolLocation();
        if (patrolTarget != null) {
            waitingTimer = Random.Range(aiAttributes.minWaitingTimeAtTarget, aiAttributes.maxWaitingTimeAtTarget);
        }
    }

    public bool UpdateOrder() {
        if (patrolTarget != null) {
            if (MoveOrder.move(patrolTarget.transform.position, aiAttributes.AttachedShip.speed, aiAttributes.AttachedShip.rotationSpeed, 0.2f, 1.8f, aiAttributes.AttachedRigidBody, gameObject)) {
                waitingTimer -= Time.deltaTime;
                if (waitingTimer <= 0) {
                    return true;
                }
            }
        } else {
            return true;
        }

        return false;
    }

    public void DestroyOrder() {
        Destroy(this);
    }

    private Ship getPatrolLocation() {
        if (ShipsManager.factionCivilianShips[aiAttributes.AttachedShip.tag].Count == 0) {
            return null;
        }

        return ShipsManager.factionCivilianShips[aiAttributes.AttachedShip.tag][Random.Range(0, ShipsManager.factionCivilianShips[aiAttributes.AttachedShip.tag].Count)];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiderHuntOrder : MonoBehaviour, Order {
    private AiAttributes aiAttributes;

    private Ship currentTarget;

    private Vector2 targetPos;

    private float waitingTimer;

    private void Start() {
        aiAttributes = GetComponent<AiAttributes>();

        currentTarget = getRandomEnemyCivilian();

        if (currentTarget != null) {
            targetPos = currentTarget.transform.position;
            waitingTimer = Random.Range(aiAttributes.minWaitingTimeAtTarget, aiAttributes.maxWaitingTimeAtTarget);
        }
    }

    public bool UpdateOrder() {
        if (currentTarget != null) {
            if (MoveOrder.move(targetPos, aiAttributes.AttachedShip.speed, aiAttributes.AttachedShip.rotationSpeed, 0.2f, 3f, aiAttributes.AttachedRigidBody, gameObject)) {
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

    private Ship getRandomEnemyCivilian() {
        HashSet<string> factionsAtWarWith = RelationshipManager.GetFactionsFactionIsAtWarWith(tag);

        string factionTag = null;
        foreach (string currentFactionTag in factionsAtWarWith) {
            if (factionTag == null || Random.value > (1f / factionsAtWarWith.Count)) {
                factionTag = currentFactionTag;
            }
        }

        if (ShipsManager.factionCivilianShips[factionTag].Count == 0) {
            return null;
            
        }

        return ShipsManager.factionCivilianShips[factionTag][Random.Range(0, ShipsManager.factionCivilianShips[factionTag].Count)];
    }
}

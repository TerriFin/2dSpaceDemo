using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiderAi : MonoBehaviour {
    public float stateUpdateTime;

    private float nextStateUpdateTime;

    private AiAttributes aiAttributes;

    public Order CurrentOrder { get; private set; }

    private void Start() {
        nextStateUpdateTime = Time.time + stateUpdateTime;

        aiAttributes = GetComponent<AiAttributes>();

        CurrentOrder = null;
    }

    private void Update() {
        if (CurrentOrder != null) {
            if (CurrentOrder.UpdateOrder()) {
                CurrentOrder.DestroyOrder();
                CurrentOrder = null;
            }
        }

        if (Time.time >= nextStateUpdateTime) {
            nextStateUpdateTime += stateUpdateTime;
            if (aiAttributes.AttachedSensors.EnemyMilitaryNearby) {
                if (CurrentOrder != null) {
                    CurrentOrder.DestroyOrder();
                }
                CurrentOrder = gameObject.AddComponent<FleeOrder>();
            } else if (aiAttributes.AttachedSensors.EnemyNearby) {
                if (CurrentOrder != null) {
                    CurrentOrder.DestroyOrder();
                }
                CurrentOrder = gameObject.AddComponent<AttackOrder>();
            } else if (CurrentOrder == null && RelationshipManager.IsFactionInWar(tag)) {
                CurrentOrder = gameObject.AddComponent<RaiderHuntOrder>();
            } else if (CurrentOrder == null && !RelationshipManager.IsFactionInWar(tag)) {
                CurrentOrder = gameObject.AddComponent<PatrolShipsOrder>();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAi : MonoBehaviour {

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
            if (aiAttributes.AttachedSensors.EnemyNearby) {
                if (CurrentOrder != null) {
                    CurrentOrder.DestroyOrder();
                }
                CurrentOrder = gameObject.AddComponent<AttackOrder>();
            } else if (CurrentOrder == null) {
                if (Random.value <= aiAttributes.patrolPreferenceForBuildings) {
                    CurrentOrder = gameObject.AddComponent<PatrolBuildingsOrder>();
                } else {
                    CurrentOrder = gameObject.AddComponent<PatrolShipsOrder>();
                }
            }
        }
    }
}

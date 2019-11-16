using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryAi : MonoBehaviour, RightClick {
    public bool automate;
    public bool attackOnSight;

    public float stateUpdateTime;

    private float nextStateUpdateTime;

    private AiAttributes aiAttributes;

    public Order CurrentOrder { get; private set; }

    private void Start() {
        nextStateUpdateTime = Time.time + stateUpdateTime;

        aiAttributes = GetComponent<AiAttributes>();
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

            if (automate) {
                if (CurrentOrder == null && RelationshipManager.IsFactionInWar(tag)) {
                    CurrentOrder = gameObject.AddComponent<RaiderHuntOrder>();
                } else if (CurrentOrder == null && !RelationshipManager.IsFactionInWar(tag)) {
                    CurrentOrder = gameObject.AddComponent<PatrolBuildingsOrder>();
                }
            }

            if (attackOnSight) {
                if (aiAttributes.AttachedSensors.EnemyNearby && (CurrentOrder == null || CurrentOrder.GetType() != typeof(ManualMoveOrder))) {
                    if (CurrentOrder != null) {
                        CurrentOrder.DestroyOrder();
                    }
                    CurrentOrder = gameObject.AddComponent<AttackOrder>();
                }
            }
        }
    }

    public void HandleRightClick(Vector2 clickedPos, string faction) {
        if (tag.Equals(faction)) {
            if (CurrentOrder != null) {
                CurrentOrder.DestroyOrder();
            }

            if (aiAttributes == null) {
                aiAttributes = GetComponent<AiAttributes>();
            }

            aiAttributes.currentManualTarget = clickedPos;
            CurrentOrder = gameObject.AddComponent<ManualMoveOrder>();
        }
    }
}

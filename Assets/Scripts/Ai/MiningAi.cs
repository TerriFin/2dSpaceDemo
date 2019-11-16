using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningAi : MonoBehaviour {
    public float stateUpdateTime;
    private float nextStateUpdateTime;

    private AiAttributes aiAttributes;

    public Order CurrentOrder { get; private set; }

    private LimitedProductionStructureTag attachedFactory;

    private void Start() {
        nextStateUpdateTime = Time.time + stateUpdateTime;

        aiAttributes = GetComponent<AiAttributes>();

        CurrentOrder = null;

        attachedFactory = GetComponent<LimitedProductionStructureTag>();
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
            } else if (CurrentOrder == null) {
                if (aiAttributes.AttachedShip.Cargo.CurrentMetal >= aiAttributes.AttachedShip.Cargo.GetCurrentFreeCargo() / 2) {
                    if (attachedFactory != null && attachedFactory.factory != null) {
                        CurrentOrder = gameObject.AddComponent<ReturnMetalToBase>();
                    } else {
                        CurrentOrder = gameObject.AddComponent<CargoSellOrder>();
                    }
                } else {
                    if (aiAttributes.AttachedSensors.AsteroidNearby) {
                        CurrentOrder = gameObject.AddComponent<MineAsteroidOrder>();
                    } else {
                        CurrentOrder = gameObject.AddComponent<ReturnMetalToBase>();
                    }
                }
            }
        }
    }
}

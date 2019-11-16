using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoAi : MonoBehaviour {

    public float stateUpdateTime;
    public int repeatTradeAfterXCycles;

    private float nextStateUpdateTime;

    private AiAttributes aiAttributes;
    private int repeatTimer;

    public Order CurrentOrder { get; private set; }

    private void Start() {
        nextStateUpdateTime = Time.time + stateUpdateTime;

        aiAttributes = GetComponent<AiAttributes>();
        repeatTimer = 0;

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
            repeatTimer++;
            if (aiAttributes.AttachedSensors.EnemyMilitaryNearby) {
                if (CurrentOrder != null) {
                    CurrentOrder.DestroyOrder();
                }
                CurrentOrder = gameObject.AddComponent<FleeOrder>();
            } else if (CurrentOrder == null || repeatTimer >= repeatTradeAfterXCycles) {
                repeatTimer = 0;
                if (aiAttributes.AttachedShip.Cargo.CurrentMetal >= aiAttributes.AttachedShip.Cargo.GetCurrentFreeCargo() / 2) {
                    if (CurrentOrder != null) {
                        CurrentOrder.DestroyOrder();
                    }
                    
                    CurrentOrder = gameObject.AddComponent<CargoSellOrder>();
                } else {
                    if (CurrentOrder != null) {
                        CurrentOrder.DestroyOrder();
                    }

                    CurrentOrder = gameObject.AddComponent<CargoBuyOrder>();
                }
            } 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitpoints : MonoBehaviour {
    public int maxHp;
    public float afterCombatHealTime;
    public float timeBetweenHeals;
    public int healAmount;

    public int CurrentHp { get; private set; }
    public HealthBar AttachedHealthBar { get; private set; }

    private float afterCombatHealTimer;
    private float timeBetweenHealsTimer;

    private void Start() {
        CurrentHp = maxHp;
        AttachedHealthBar = GetComponentInChildren<HealthBar>();

        afterCombatHealTimer = 0;
        timeBetweenHealsTimer = 0;
    }

    private void Update() {
        if (CurrentHp <= 0) {
            print("OBJECT: " + gameObject.name + " WAS DESTROYED!");
            Ship attachedShip = GetComponent<Ship>();
            Structure attachedStructure = GetComponent<Structure>();

            if (attachedShip != null) {
                ShipsManager.RemoveShip(attachedShip);
            } else if (attachedStructure != null) {
                BuildingsManager.RemoveBuilding(attachedStructure);
            }

            Destroy(gameObject);
        }

        if (CurrentHp < maxHp) {
            if (Time.time >= afterCombatHealTimer) {
                if (Time.time >= timeBetweenHealsTimer) {
                    timeBetweenHealsTimer = Time.time + timeBetweenHeals;
                    CurrentHp += healAmount;

                    if (CurrentHp > maxHp) {
                        CurrentHp = maxHp;
                    }
                }
            }
        }
    }


    public void TakeDamage(int amount, string factionTag) {
        CurrentHp -= amount;

        afterCombatHealTimer = Time.time + afterCombatHealTime;

        if (CurrentHp <= 0) {
            MetalCargo attachedCargo = GetComponent<MetalCargo>();
            if (attachedCargo != null) {
                FactionsManager.factions[factionTag].money += (attachedCargo.CurrentMetal * 220);
            }
        }
    }

    public void TakeDamage(int amount) {
        CurrentHp -= amount;

        afterCombatHealTimer = Time.time + afterCombatHealTime;
    }
}

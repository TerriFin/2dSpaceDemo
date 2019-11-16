using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGenerator : MonoBehaviour {
    public int moneyAmount;
    public float moneyTime;

    public float MoneyTimer { get; private set; }

    private void Start() {
        MoneyTimer = Time.time + moneyTime;
    }

    private void Update() {
        if (!tag.Equals("Untagged")) {
            if (Time.time >= MoneyTimer) {
                MoneyTimer += moneyTime;
                FactionsManager.factions[tag].money += moneyAmount;
            }
        }
    }
}

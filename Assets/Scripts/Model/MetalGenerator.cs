using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalGenerator : MonoBehaviour {
    public int minTimeToRestock;
    public int maxTimeToRestock;
    public int minRestockAmount;
    public int maxRestockAmount;

    public MetalCargo Cargo { get; private set; }
    public SellingStructure SellingStructure { get; private set; }

    private float nextRestockTime;

    private void Start() {
        nextRestockTime = Time.time + Random.Range(minTimeToRestock, maxTimeToRestock);

        Cargo = GetComponent<MetalCargo>();
        SellingStructure = GetComponent<SellingStructure>();
    }

    private void Update() {
        if (Time.time > nextRestockTime) {
            nextRestockTime += Random.Range(minTimeToRestock, maxTimeToRestock);
            Cargo.AddMetal(Random.Range(minRestockAmount, maxRestockAmount));
            if (SellingStructure != null) {
                SellingStructure.CalculateNewMetalPrice();
            }
        }
    }
}

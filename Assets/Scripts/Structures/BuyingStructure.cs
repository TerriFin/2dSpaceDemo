using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingStructure : MonoBehaviour {

    public int baseMetalPrice;
    public int maxMetalPrice;
    public int minAmountForPriceChange;
    public float priceChangePercentage;

    public int timeBetweenPriceUpdates;

    private float nextPriceUpdateTime;
    private int howManyTimesPriceChangedManually;
    private int allowedManualPriceChanges;

    public MetalCargo Cargo { get; private set; }
    public int MetalPrice { get; private set; }

    private void Start() {
        nextPriceUpdateTime = Time.time + timeBetweenPriceUpdates;
        howManyTimesPriceChangedManually = 0;
        allowedManualPriceChanges = 6;

        Cargo = GetComponent<MetalCargo>();
        MetalPrice = baseMetalPrice;
        CalculateNewMetalPrice();
    }

    private void Update() {
        if (Time.time > nextPriceUpdateTime) {
            nextPriceUpdateTime += timeBetweenPriceUpdates;
            CalculateNewMetalPrice();
        }
    }

    private void CalculateNewMetalPrice() {
        if (Cargo.CurrentMetal < minAmountForPriceChange) {
            if (MetalPrice < maxMetalPrice) {
                MetalPrice += Mathf.FloorToInt(MetalPrice * priceChangePercentage);
            }
        } else {
            MetalPrice = baseMetalPrice;
        }
    }

    public bool BuyFromShip(Ship cargoShip, int amountSelling) {
        int costOfPurchase = amountSelling * MetalPrice;
        if (FactionsManager.factions[gameObject.tag].money >= costOfPurchase || cargoShip.tag.Equals(gameObject.tag)) {
            if (Cargo.BuyMetal(cargoShip.Cargo, amountSelling)) {
                FactionsManager.factions[gameObject.tag].money -= costOfPurchase;
                FactionsManager.factions[cargoShip.tag].money += costOfPurchase;
                if (Cargo.CurrentMetal >= minAmountForPriceChange) {
                    MetalPrice = baseMetalPrice;
                }
                return true;
            }
        }

        return false;
    }

    public void IncreaseBaseMetalPrice() {
        if (howManyTimesPriceChangedManually < allowedManualPriceChanges) {
            howManyTimesPriceChangedManually++;
            baseMetalPrice += 10;
            maxMetalPrice += 10;

            if (baseMetalPrice > MetalPrice) {
                MetalPrice = baseMetalPrice;
            }
        }
    }

    public void DecreaseBaseMetalPrice() {
        if (howManyTimesPriceChangedManually > -allowedManualPriceChanges) {
            howManyTimesPriceChangedManually--;
            baseMetalPrice -= 10;
            maxMetalPrice += 10;

            MetalPrice = baseMetalPrice;
        }
    }
}

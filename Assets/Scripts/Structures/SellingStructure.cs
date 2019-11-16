using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingStructure : MonoBehaviour {

    public int baseMetalPrice;
    public int minAmountBeforePriceChange;
    public int maxAmountForPriceChange;
    public float amountToPriceModifier;

    private int howManyTimesPriceChangedManually;
    private int allowedManualPriceChanges;

    public MetalCargo Cargo { get; private set; }
    public int MetalPrice { get; private set; }

    private void Start() {
        howManyTimesPriceChangedManually = 0;
        allowedManualPriceChanges = 6;

        Cargo = GetComponent<MetalCargo>();
        CalculateNewMetalPrice();
    }

    public void CalculateNewMetalPrice() {
        int basePrice = Cargo.CurrentMetal * baseMetalPrice;
        if (Cargo.CurrentMetal > minAmountBeforePriceChange) {
            if (Cargo.CurrentMetal > maxAmountForPriceChange) {
                float newPercentage = 1 - ((maxAmountForPriceChange - minAmountBeforePriceChange) * amountToPriceModifier);
                MetalPrice = Mathf.FloorToInt(baseMetalPrice * newPercentage);
            } else {
                float newPercentage = 1 - ((Cargo.CurrentMetal - minAmountBeforePriceChange) * amountToPriceModifier);
                MetalPrice = Mathf.FloorToInt(baseMetalPrice * newPercentage);
            }
        } else {
            MetalPrice = baseMetalPrice;
        }

        if (MetalPrice < 1) {
            MetalPrice = 1;
        }
    }

    public bool SellToShip(Ship cargoShip, int amountBuying) {
        int costOfPurchase = amountBuying * MetalPrice;
        if (amountBuying <= Cargo.CurrentMetal) {
            if (FactionsManager.factions[cargoShip.tag].money >= costOfPurchase || cargoShip.tag.Equals(gameObject.tag)) {
                if (Cargo.SellMetal(cargoShip.Cargo, amountBuying)) {
                    FactionsManager.factions[cargoShip.tag].money -= costOfPurchase;
                    FactionsManager.factions[gameObject.tag].money += costOfPurchase;
                    CalculateNewMetalPrice();
                    return true;
                }
            }
        } else {
            amountBuying = Cargo.CurrentMetal;
            costOfPurchase = Cargo.CurrentMetal * MetalPrice;
            if (FactionsManager.factions[cargoShip.tag].money >= costOfPurchase || cargoShip.tag.Equals(gameObject.tag)) {
                if (Cargo.SellMetal(cargoShip.Cargo, amountBuying)) {
                    FactionsManager.factions[cargoShip.tag].money -= costOfPurchase;
                    FactionsManager.factions[gameObject.tag].money += costOfPurchase;
                    CalculateNewMetalPrice();
                    return true;
                }
            }
        }

        return false;
    }

    public void IncreaseBaseMetalPrice() {
        if (howManyTimesPriceChangedManually < allowedManualPriceChanges) {
            howManyTimesPriceChangedManually++;
            baseMetalPrice += 10;
            CalculateNewMetalPrice();
        }
    }

    public void DecreaseBaseMetalPrice() {
        if (howManyTimesPriceChangedManually > -allowedManualPriceChanges) {
            howManyTimesPriceChangedManually--;
            baseMetalPrice -= 10;
            CalculateNewMetalPrice();
        }
    }
}
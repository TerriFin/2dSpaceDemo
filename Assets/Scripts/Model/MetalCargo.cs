using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCargo : MonoBehaviour {
    public int startingMetal;
    public int maxMetal;

    public int CurrentMetal { get; private set; }

    private void Start() {
        CurrentMetal = startingMetal;
    }

    public int GetCurrentFreeCargo() {
        return maxMetal - CurrentMetal;
    }

    public bool AddMetal(int amount) {
        if (!(CurrentMetal + amount > maxMetal)) {
            CurrentMetal += amount;
            return true;
        }

        return false;
    }

    public bool RemoveMetal(int amount) {
        if (!(CurrentMetal - amount < 0)) {
            CurrentMetal -= amount;
            return true;
        }

        return false;
    }

    public bool SellMetal(MetalCargo to, int amount) {
        if (Vector2.Distance(transform.position, to.transform.position) <= 2f) {
            if (!RelationshipManager.IsBlockading(gameObject.tag, to.tag)) {
                if (CurrentMetal >= amount) {
                    if (to.GetCurrentFreeCargo() >= amount) {
                        CurrentMetal -= amount;
                        to.AddMetal(amount);
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public bool BuyMetal(MetalCargo from, int amount) {
        if (Vector2.Distance(transform.position, from.transform.position) <= 2f) {
            if (!RelationshipManager.IsBlockading(gameObject.tag, from.tag)) {
                if (GetCurrentFreeCargo() >= amount) {
                    CurrentMetal += amount;
                    from.RemoveMetal(amount);
                    return true;
                }
            }
        }

        return false;
    }
}

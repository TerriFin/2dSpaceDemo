using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    public AsteroidField field;

    public bool smallAsteroid;
    public bool mediumAsteroid;
    public bool largeAsteroid;

    public int hpPerMetal;

    private Hitpoints attachedHitpoints;

    private void Start() {
        attachedHitpoints = GetComponent<Hitpoints>();
    }

    public int MineAsteroid(int amount) {
        attachedHitpoints.TakeDamage(amount, null);
        return 1 + (amount / hpPerMetal);
    }

    private void OnDestroy() {
        if (field != null) {
            field.HandleAsteroidDestruction(this);
        }
    }
}

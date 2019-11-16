using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensors : MonoBehaviour {
    public float checkRadius;
    public float checkTime;

    private float enemyCheckTimer;

    public Collider2D[] NearbyColliders { get; private set; }
    public bool EnemyMilitaryNearby { get; private set; }
    public bool EnemyNearby { get; private set; }
    public bool AsteroidNearby { get; private set; }

    private void Start() {
        enemyCheckTimer = Time.time + checkTime;

        NearbyColliders = new Collider2D[0];
    }

    private void Update() {
        if (Time.time >= enemyCheckTimer) {
            enemyCheckTimer += checkTime;
            GetAndSetEnemiesInArea();
        }
    }

    private void GetAndSetEnemiesInArea() {
        NearbyColliders = Physics2D.OverlapCircleAll(transform.position, checkRadius, Utilities.bulletIgnoreLayerMask);
        bool foundOneMilitaryEnemy = false;
        bool foundOneEnemy = false;
        bool foundOneAsteroid = false;

        if (!gameObject.tag.Equals("Untagged")) {
            foreach (Collider2D collider in NearbyColliders) {
                if (!collider.tag.Equals("Asteroid")) {
                    if (RelationshipManager.AreFactionsInWar(gameObject.tag, collider.tag)) {
                        foundOneEnemy = true;

                        if (collider.gameObject.layer == 9) {
                            foundOneMilitaryEnemy = true;
                            break;
                        }
                    }
                } else {
                    foundOneAsteroid = true;
                }
            }
        }

        EnemyNearby = foundOneEnemy;
        EnemyMilitaryNearby = foundOneMilitaryEnemy;
        AsteroidNearby = foundOneAsteroid;
    }
}

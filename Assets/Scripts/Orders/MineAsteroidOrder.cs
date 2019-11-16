using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineAsteroidOrder : MonoBehaviour, Order {

    private AiAttributes aiAttributes;

    private Asteroid currentTargetAsteroid;

    private float miningTimer;

    private void Start() {
        aiAttributes = GetComponent<AiAttributes>();

        currentTargetAsteroid = GetRandomAsteroid();

        miningTimer = Time.time + aiAttributes.miningTime;
    }

    public void DestroyOrder() {
        Destroy(this);
    }

    public bool UpdateOrder() {
        if (currentTargetAsteroid != null) {
            if (MoveOrder.move(currentTargetAsteroid.transform.position, aiAttributes.AttachedShip.speed, aiAttributes.AttachedShip.rotationSpeed, 0.2f, 2f, aiAttributes.AttachedRigidBody, gameObject)) {
                if (Time.time >= miningTimer) {
                    miningTimer = Time.time + aiAttributes.miningTime;
                    // Mine more metal, if mined metal does not fit into cargo, return true
                    if (!aiAttributes.AttachedShip.Cargo.AddMetal(currentTargetAsteroid.MineAsteroid(aiAttributes.miningDamage))) {
                        return true;
                    }

                    // Check if there is enough metal, return true if so
                    if (aiAttributes.AttachedShip.Cargo.CurrentMetal >= aiAttributes.AttachedShip.Cargo.maxMetal * 0.9f) {
                        return true;
                    }
                }
            }
        } else {
            // Return true if current asteroid is destroyed
            return true;
        }

        return false;
    }

    private Asteroid GetRandomAsteroid() {
        Collider2D[] colliders = aiAttributes.AttachedSensors.NearbyColliders;
        Asteroid foundAsteroid = null;

        if (aiAttributes.AttachedSensors.AsteroidNearby) {
            foreach (Collider2D current in colliders) {
                if (current != null) {
                    Asteroid fromCurrent = current.GetComponent<Asteroid>();
                    if (fromCurrent != null) {
                        if (foundAsteroid == null || Random.value >= aiAttributes.randomness) {
                            foundAsteroid = fromCurrent;
                        }
                    }
                }
            }
        }

        return foundAsteroid;
    }
}

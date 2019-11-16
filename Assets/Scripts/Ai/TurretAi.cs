using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAi : MonoBehaviour {
    public float targetUpdateTime;

    public Collider2D Target { get; private set; }

    private AiAttributes aiAttributes;
    private float targetUpdateTimer;
    private Turret attachedTurret;

    private void Start() {
        Target = null;

        aiAttributes = GetComponentInParent<AiAttributes>();
        targetUpdateTimer = Time.time + targetUpdateTime;
        attachedTurret = GetComponent<Turret>();
    }

    private void Update() {
        if (Time.time >= targetUpdateTimer) {
            targetUpdateTimer += targetUpdateTime;

            float closestEnemy = float.MaxValue;
            bool foundOneEnemy = false;

            if (aiAttributes.AttachedSensors.EnemyNearby) {
                foreach (Collider2D collider in aiAttributes.AttachedSensors.NearbyColliders) {
                    if (collider != null) {
                        if (RelationshipManager.AreFactionsInWar(collider.tag, gameObject.tag)) {
                            float distanceToCollider = Vector2.Distance(transform.position, collider.transform.position);
                            if (Target == null || closestEnemy > distanceToCollider) {
                                Target = collider;
                                closestEnemy = distanceToCollider;
                                foundOneEnemy = true;
                            }
                        }
                    }
                }
            }

            if (!foundOneEnemy) {
                Target = null;
            }
        }

        // If we have a target rotate the turret to face target, and shoot
        if (Target != null) {
            float angleToTarget = Utilities.angleToTarget(gameObject, Target.transform.position);
            if (Mathf.Abs(angleToTarget) < 3) {
                attachedTurret.FireTurret();
            } else {
                rotateTurret(angleToTarget);
            }
        // If we do not have a target, face the turret forwards
        } else {
            if (aiAttributes.AttachedShip != null) {
                float angleForwards = Utilities.angleToTarget(gameObject, aiAttributes.AttachedShip.transform.position + aiAttributes.AttachedShip.transform.up);
                if (Mathf.Abs(angleForwards) > 1) {
                    rotateTurret(angleForwards);
                }
            }
        }
    }

    private void rotateTurret(float angleToTarget) {
        float turretRotation = attachedTurret.turretRotationSpeed * Time.deltaTime * Random.Range(0.5f, 1.5f);
        if (angleToTarget > 0) {
            turretRotation = -turretRotation;
        }

        transform.Rotate(new Vector3(0, 0, turretRotation));
    }
}

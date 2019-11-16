using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public GameObject turretBarrel;
    public GameObject projectilePrefab;
    public float turretRotationSpeed;
    public float turretFiringSpeed;
    public float turretInaccuracy;

    private float turretFiringTimer;

    private void Start() {
        gameObject.tag = transform.parent.tag;
        turretFiringTimer = Time.time + turretFiringSpeed;
    }

    public void FireTurret() {
        if (Time.time >= turretFiringTimer) {
            turretFiringTimer = Time.time + turretFiringSpeed;
            Quaternion newProjectileRotation = transform.rotation * Quaternion.Euler(0, 0, transform.rotation.z + Random.Range(-turretInaccuracy, turretInaccuracy));
            GameObject projectile = Instantiate(projectilePrefab, turretBarrel.transform.position, newProjectileRotation);
            projectile.tag = tag;
        }
    }
}

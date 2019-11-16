using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public int damage;
    public float speed;
    public float decayTime;

    private float decayTimer;

    private void Start() {
        decayTimer = Time.time + decayTime;
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
    }

    private void Update() {
        if (Time.time >= decayTimer) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Check that the other is not of the same faction, and not marked as bullet in tags
        if (!other.tag.Equals(tag) && other.gameObject.layer != 8) {
            Hitpoints otherHitpoints = other.GetComponent<Hitpoints>();
            if (otherHitpoints != null) {
                otherHitpoints.TakeDamage(damage, tag);
            }

            Destroy(gameObject);
        }
    }
}

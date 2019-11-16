using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    public GameObject hitpointsBar;

    private Hitpoints attachedHitpoints;
    private float startingHeight;
    private Quaternion startingRotation;
    private SpriteRenderer redBarSpriteRenderer;
    private SpriteRenderer greenBarSpriteRenderer;

    private bool startedFromOutside = false;

    private void Start() {
        if (!startedFromOutside) {
            attachedHitpoints = GetComponentInParent<Hitpoints>();
            startingHeight = transform.position.y - transform.parent.transform.position.y;
            startingRotation = transform.rotation;
            redBarSpriteRenderer = GetComponent<SpriteRenderer>();
            greenBarSpriteRenderer = hitpointsBar.GetComponentInChildren<SpriteRenderer>();
            DontShow();
        }
    }

    private void Update() {
        // Calculate hp percentage and set bars local scale
        float currentHpPercentageFromMax = (float) attachedHitpoints.CurrentHp / (float) attachedHitpoints.maxHp;
        hitpointsBar.transform.localScale = new Vector2(currentHpPercentageFromMax, 1f);
    }

    private void LateUpdate() {
        // Set the position to be what it was at beginning so it does not rotate/move with parent
        transform.position = new Vector2(transform.parent.transform.position.x, transform.parent.transform.position.y + startingHeight);
        transform.rotation = startingRotation;
    }

    public void Show() {
        redBarSpriteRenderer.enabled = true;
        greenBarSpriteRenderer.enabled = true;
    }

    public void DontShow() {
        redBarSpriteRenderer.enabled = false;
        greenBarSpriteRenderer.enabled = false;
    }

    public void ManuallyStart() {
        startedFromOutside = true;

        attachedHitpoints = GetComponentInParent<Hitpoints>();
        startingHeight = transform.position.y - transform.parent.transform.position.y;
        startingRotation = transform.rotation;
        redBarSpriteRenderer = GetComponent<SpriteRenderer>();
        greenBarSpriteRenderer = hitpointsBar.GetComponentInChildren<SpriteRenderer>();
        DontShow();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureBuildBar : MonoBehaviour {
    public GameObject progressBar;

    private BuildingShip buildingShip;

    private float startingWidth;
    private float startingHeight;
    private Quaternion startingRotation;

    private SpriteRenderer bar;
    private SpriteRenderer barBackground;

    private float startingTime;

    private void Start() {
        buildingShip = GetComponentInParent<BuildingShip>();

        startingWidth = transform.position.x - transform.parent.transform.position.x;
        startingHeight = transform.position.y - transform.parent.transform.position.y;
        startingRotation = transform.rotation;

        bar = GetComponent<SpriteRenderer>();
        barBackground = GetComponentInChildren<SpriteRenderer>();

        HideBar();
    }

    private void Update() {
        if (bar.enabled) {
            float currentTimeLeft = (Time.time - startingTime) / buildingShip.buildingTime;
            progressBar.transform.localScale = new Vector2(currentTimeLeft, 1f);
        }
    }

    private void LateUpdate() {
        // Set the position to be what it was at beginning so it does not rotate/move with parent
        transform.position = new Vector2(transform.parent.transform.position.x + startingWidth, transform.parent.transform.position.y + startingHeight);
        transform.rotation = startingRotation;
    }

    public void HideBar() {
        bar.enabled = false;
        barBackground.enabled = false;
    } 

    public void ShowBar() {
        bar.enabled = true;
        barBackground.enabled = true;

        startingTime = Time.time;
    }
}

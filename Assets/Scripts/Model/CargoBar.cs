using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoBar : MonoBehaviour {
    public GameObject cargoBar;

    private MetalCargo attachedCargo;
    private float startingWidth;
    private float startingHeight;
    private Quaternion startingRotation;

    private void Start() {
        attachedCargo = GetComponentInParent<MetalCargo>();
        startingWidth = transform.position.x - transform.parent.transform.position.x;
        startingHeight = transform.position.y - transform.parent.transform.position.y;
        startingRotation = transform.rotation;
    }

    private void Update() {
        // Calculate hp percentage and set bars local scale
        float currentFreeCargoPercentage = (float)attachedCargo.CurrentMetal / (float)attachedCargo.maxMetal;
        cargoBar.transform.localScale = new Vector2(1f, currentFreeCargoPercentage);
    }

    private void LateUpdate() {
        // Set the position to be what it was at beginning so it does not rotate/move with parent
        transform.position = new Vector2(transform.parent.transform.position.x + startingWidth, transform.parent.transform.position.y + startingHeight);
        transform.rotation = startingRotation;
    }
}

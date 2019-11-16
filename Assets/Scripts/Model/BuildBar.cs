using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBar : MonoBehaviour {
    public GameObject ProgressBar;

    private CommandStructure commandStructure;
    private ProductionStructureLimit productionStructureLimit;
    private ProductionStructureNoLimit productionStructureNoLimit;

    private float startingWidth;
    private float startingHeight;
    private Quaternion startingRotation;

    private void Start() {
        commandStructure = GetComponentInParent<CommandStructure>();
        productionStructureNoLimit = GetComponentInParent<ProductionStructureNoLimit>();
        productionStructureLimit = GetComponentInParent<ProductionStructureLimit>();

        startingWidth = transform.position.x - transform.parent.transform.position.x;
        startingHeight = transform.position.y - transform.parent.transform.position.y;
        startingRotation = transform.rotation;
    }

    private void Update() {
        if (commandStructure != null) {
            if (commandStructure.IsBuildingBig) {
                float currentFreeCargoPercentage = (float)commandStructure.BuildTimer / (float)commandStructure.bigCargoBuildTime;
                ProgressBar.transform.localScale = new Vector2(currentFreeCargoPercentage, 1f);
            } else if (commandStructure.IsBuildingMedium) {
                float currentFreeCargoPercentage = (float)commandStructure.BuildTimer / (float)commandStructure.mediumCargoBuildTime;
                ProgressBar.transform.localScale = new Vector2(currentFreeCargoPercentage, 1f);
            } else if (commandStructure.IsBuildingSmall) {
                float currentFreeCargoPercentage = (float)commandStructure.BuildTimer / (float)commandStructure.smallCargoBuildTime;
                ProgressBar.transform.localScale = new Vector2(currentFreeCargoPercentage, 1f);
            }
        } else if (productionStructureLimit != null) {
            float currentFreeCargoPercentage = (float)productionStructureLimit.BuildTimer / (float)productionStructureLimit.buildTime;
            ProgressBar.transform.localScale = new Vector2(currentFreeCargoPercentage, 1f);
        } else if (productionStructureNoLimit != null) {
            float currentFreeCargoPercentage = (float)productionStructureNoLimit.BuildTimer / (float)productionStructureNoLimit.buildTime;
            ProgressBar.transform.localScale = new Vector2(currentFreeCargoPercentage, 1f);
        }
    }

    private void LateUpdate() {
        // Set the position to be what it was at beginning so it does not rotate/move with parent
        transform.position = new Vector2(transform.parent.transform.position.x + startingWidth, transform.parent.transform.position.y + startingHeight);
        transform.rotation = startingRotation;
    }
}

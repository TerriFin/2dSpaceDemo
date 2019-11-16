using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingShip : MonoBehaviour {
    public Sprite buildingSprite;
    public StructureBuildBar structureBuildBar;
    public GameObject structureToBuild;
    public float buildingTime;

    public float BuildingTimer { get; private set; }

    private AiAttributes aiAttributes;
    private bool deployed = false;

    private void Start() {
        aiAttributes = GetComponent<AiAttributes>();
    }

    private void Update() {
        if (deployed) {
            if (Time.time >= BuildingTimer) {
                GameObject createdStructure = Instantiate(structureToBuild);
                createdStructure.transform.position = transform.position;
                createdStructure.tag = tag;

                BuildingsManager.AddBuilding(createdStructure.GetComponent<Structure>());

                Destroy(gameObject);
            }
        } else {
            float distanceToTarget = Vector2.Distance(transform.position, aiAttributes.currentManualTarget);
            if (distanceToTarget < 2f) {
                BuildAi attachedBuildAi = GetComponent<BuildAi>();
                attachedBuildAi.CurrentOrder.DestroyOrder();
                Destroy(attachedBuildAi);

                BuildingTimer = Time.time + buildingTime;

                GetComponent<SpriteRenderer>().sprite = buildingSprite;
                structureBuildBar.ShowBar();

                aiAttributes.AttachedRigidBody.velocity = Vector2.zero;

                deployed = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public float movementSpeed;

    public float maxVertical;
    public float maxHorizontal;
    public float minVertical;
    public float minHorizontal;

    private Camera attachedCamera;
    private float startingSize;

    private void Start() {
        attachedCamera = GetComponent<Camera>();
        startingSize = attachedCamera.orthographicSize;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (Input.mousePosition.x > Camera.main.pixelWidth * 0.3f) {
                Collider2D selectedCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (selectedCollider != null) {
                    SelectionManager.HandleClickOnCollider(selectedCollider);
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            if (Input.mousePosition.x > Camera.main.pixelWidth * 0.3f) {
                SelectionManager.HandleRightClick(Camera.main.ScreenToWorldPoint(Input.mousePosition), FactionsManager.playerFaction.factionTag);
            }
        }

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float newXPos = gameObject.transform.position.x + (horizontal * movementSpeed * Time.deltaTime);
        float newYPos = gameObject.transform.position.y + (vertical * movementSpeed * Time.deltaTime);

        if (newXPos > maxHorizontal) {
            newXPos = maxHorizontal;
        } else if (newXPos < minHorizontal) {
            newXPos = minHorizontal;
        }

        if (newYPos > maxVertical) {
            newYPos = maxVertical;
        } else if (newYPos < minVertical) {
            newYPos = minVertical;
        }

        gameObject.transform.position = new Vector3(newXPos, newYPos, gameObject.transform.position.z);
    }

    public void SliderZoom(float value) {
        attachedCamera.orthographicSize = startingSize - value;
    }
}

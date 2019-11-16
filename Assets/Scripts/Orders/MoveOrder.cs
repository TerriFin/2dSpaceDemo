using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOrder : MonoBehaviour {
    public static bool move(Vector2 targetPos, float speed, float rotationSpeed, float rotationForwardSpeed, float finishRange, Rigidbody2D rigidBody, GameObject gameobject) {
        // Check if ship is not at the targetPos
        if (Vector2.Distance(new Vector2(gameobject.transform.position.x, gameobject.transform.position.y), targetPos) > finishRange) {
            // How much the ship is allowed to "miss" the target
            float allowedAngleOff = 5;
            // Calculate the angle the ship is in relation to target
            float angleToTarget = Utilities.angleToTarget(gameobject, targetPos);

            // Check if the ship is pointed towards target and continue accordingly
            if (Mathf.Abs(angleToTarget) < allowedAngleOff) {
                rigidBody.AddRelativeForce(new Vector2(0, speed * Time.deltaTime));
            } else {
                // calculate unique rotation for each cycle for cosmetic effect
                float rotation = rotationSpeed * Time.deltaTime * Random.Range(0.5f, 1.5f);
                // Check if the ship needs to turn either right or left and continue accordingly
                if (angleToTarget > 0) {
                    // Flip the rotation if more than zero
                    rotation = -rotation;
                }

                // Apply the rotation
                rigidBody.MoveRotation(rigidBody.rotation + rotation);

                // Make the ship move slowly while turning for cosmetic effect 
                rigidBody.AddRelativeForce(new Vector2(0, speed * Time.deltaTime * rotationForwardSpeed));
            }
        } else {
            // If the ship is at targetPos, return true
            return true;
        }

        // Reset rotation if it is over 360
        if (Mathf.Abs(rigidBody.rotation) > 360) {
            if (rigidBody.rotation > 0) {
                rigidBody.rotation -= 360;
            } else {
                rigidBody.rotation += 360;
            }
        }

        // If the ship is not at targetPos, return false and dont destroy this script so that it is run untill it is at target
        return false;
    }
}

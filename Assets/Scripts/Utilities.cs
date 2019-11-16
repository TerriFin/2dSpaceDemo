using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour{

    public static int bulletIgnoreLayerMask = ~(1 << 8);

    public static float angleToTarget(GameObject source, Vector2 targetPos) {
        // Move targetPos from global to local through gameobjects transform
        Vector2 relative = source.transform.InverseTransformPoint(targetPos);
        // Calculate angle with tan, and convert it to degrees
        return Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
    }
}

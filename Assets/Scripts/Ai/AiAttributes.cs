using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttributes : MonoBehaviour {
    public float preferredCombatDistance;
    public float fleeDistance;
    public float minWaitingTimeAtTarget;
    public float maxWaitingTimeAtTarget;
    public float patrolPreferenceForBuildings;
    public float distanceToBuildingModifier;
    public float ownFactionTradeBiasModifier;
    public float randomness;
    public float miningTime;
    public int miningDamage;
    public Vector2 currentManualTarget;

    public Ship AttachedShip { get; set; }
    public Sensors AttachedSensors { get; set; }
    public Hitpoints AttachedHitpoints { get; set; }
    public Rigidbody2D AttachedRigidBody { get; set; }

    private void Start() {
        AttachedShip = GetComponent<Ship>();
        AttachedSensors = GetComponent<Sensors>();
        AttachedHitpoints = GetComponent<Hitpoints>();
        AttachedRigidBody = GetComponent<Rigidbody2D>();
    }
}

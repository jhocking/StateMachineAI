using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicAI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Enemy : BaseWaypointAI
{
    [SerializeField] GameObject player;

    public float waitTime = 2;

    // Start is called before the first frame update
    protected override void Start()
    {
        currentState = new IdleState(this.gameObject, player, waitTime);

        availableStates = new Dictionary<Type, BaseState>() {
            { typeof(IdleState), currentState},
            { typeof(PatrolState), new PatrolState(this.gameObject, player, waypoints, targetThreshold)}
        };
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicAI;

public class Enemy : BaseWaypointAI
{
    // Start is called before the first frame update
    protected override void Start()
    {
        currentState = new IdleState();
        availableStates = new Dictionary<Type, BaseState>() {
            { typeof(IdleState), currentState},
            { typeof(PatrolState), new PatrolState(waypoints)}
        };
    }
}

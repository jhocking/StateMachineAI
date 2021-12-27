using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicAI;

public class PatrolState : BaseState
{
	private Transform[] waypoints;

	public PatrolState(Transform[] waypoints) {
		this.waypoints = waypoints;
	}

    public override Type Tick() {
		return null;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicAI;

public class PatrolState : BaseState
{
	public enum PatrolType {
		Loop,
		PingPong
	}

	private GameObject actor;
	private Transform[] waypoints;
	private PatrolType behavior;

	public PatrolState(GameObject actor, Transform[] waypoints, PatrolType behavior = PatrolType.PingPong) {
		this.actor = actor;
		this.waypoints = waypoints;
		this.behavior = behavior;
	}

    public override Type Tick() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			return typeof(IdleState);
		}
		return null;
	}
}

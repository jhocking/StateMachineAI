using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicAI;

public class PatrolState : BaseState {
	private readonly Enemy actor;

	private readonly float speed;
	private readonly float targetThreshold;

	public PatrolState(Enemy actor, float targetThreshold, float speed) {
		this.actor = actor;

		this.targetThreshold = targetThreshold;
		this.speed = speed;
	}

	public override void OnEnter() {
		var agent = actor.Agent;
		agent.isStopped = false;
		agent.speed = speed;
		agent.angularSpeed = 360; // this value is already set on the component
		agent.acceleration = 8; // this value is already set on the component

		var targetPos = actor.GetCurrentWaypoint().position;
		agent.SetDestination(targetPos);
	}

	public override Type Tick() {
		var agent = actor.Agent;
		var targetDist = Vector3.Distance(agent.transform.position, actor.GetCurrentWaypoint().position);
		if (targetDist < targetThreshold) {
			var targetPos = actor.IncrementAndGetWaypoint().position;
			agent.SetDestination(targetPos);
		}

		// don't pay attention to noises in low alert state, only vision
		if (actor.CanSeePlayer) {
			return typeof(WaryState);
		}
		return null;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicAI;

public class ChaseState : BaseState {

    private Enemy actor;

	private float speed;
	private float targetThreshold;

	public ChaseState(Enemy actor, float targetThreshold, float speed) {
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
	}

	public override Type Tick() {
		var agent = actor.Agent;
		if (UnityEngine.Random.value < .4) { // just so it isn't pathfinding EVERY frame
			agent.SetDestination(actor.LastPlayerPosition);
		}
		var targetDist = Vector3.Distance(agent.transform.position, agent.destination);
		if (targetDist < targetThreshold) {
			if (!actor.CanSeePlayer && !actor.IsDetectingPlayer) {
				return typeof(WaryState);
			}
		}

		return null;
	}
}

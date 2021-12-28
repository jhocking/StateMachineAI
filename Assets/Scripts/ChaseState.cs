using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BasicAI;

public class ChaseState : BaseState {

    private Enemy actor;
    private NavMeshAgent agent;

	private float speed;
	private float targetThreshold;

	public ChaseState(Enemy actor, float targetThreshold, float speed) {
		this.actor = actor;
		agent = actor.GetComponent<NavMeshAgent>();

		this.targetThreshold = targetThreshold;
		this.speed = speed;
	}

	public override void OnEnter() {
		agent.isStopped = false;
		agent.speed = speed;
		agent.angularSpeed = 360; // this value is already set on the component
		agent.acceleration = 8; // this value is already set on the component
	}

	public override Type Tick() {
		agent.SetDestination(actor.LastPlayerPosition);
		var targetDist = Vector3.Distance(agent.transform.position, agent.destination);
		if (targetDist < targetThreshold) {
			if (!actor.CanSeePlayer && !actor.IsDetectingPlayer) {
				return typeof(IdleState);
			}
		}

		return null;
	}
}

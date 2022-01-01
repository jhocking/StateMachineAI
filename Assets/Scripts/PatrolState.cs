using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BasicAI;

public class PatrolState : BaseState
{
	private Enemy actor;
	private NavMeshAgent agent;

	private float speed;
	private float targetThreshold;

	public PatrolState(Enemy actor, float targetThreshold, float speed) {
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

		var targetPos = actor.GetCurrentWaypoint().position;
		agent.SetDestination(targetPos);
	}

	public override Type Tick() {
		var targetDist = Vector3.Distance(agent.transform.position, actor.GetCurrentWaypoint().position);
		if (targetDist < targetThreshold) {
			var targetPos = actor.IncrementAndGetWaypoint().position;
			agent.SetDestination(targetPos);
		}

		if (actor.CanSeePlayer) {
			return typeof(WaryState);
		}
		return null;
	}
}

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

	private Transform[] waypoints;
	private float targetThreshold;
	private int targetWaypointIndex;

	public PatrolState(Enemy actor, Transform[] waypoints, float targetThreshold) {
		this.actor = actor;
		agent = actor.GetComponent<NavMeshAgent>();

		this.waypoints = waypoints;
		this.targetThreshold = targetThreshold;
	}

	public override void OnEnter() {
		agent.isStopped = false;
		agent.speed = 4;
		agent.angularSpeed = 360;
		agent.acceleration = 8;

		var targetPos = waypoints[targetWaypointIndex].position;
		agent.SetDestination(targetPos);
	}

	public override Type Tick() {
		var targetDist = Vector3.Distance(agent.transform.position, waypoints[targetWaypointIndex].position);
		if (targetDist < targetThreshold) {
			targetWaypointIndex++;
			if (targetWaypointIndex >= waypoints.Length) {
				targetWaypointIndex = 0;
			}

			var targetPos = waypoints[targetWaypointIndex].position;
			agent.SetDestination(targetPos);
		}

		if (actor.CanSeePlayer) {
			return typeof(IdleState);
		}
		return null;
	}
}

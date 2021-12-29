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

	private Transform[] waypoints;
	private int targetWaypointIndex;

	public PatrolState(Enemy actor, Transform[] waypoints, float targetThreshold, float speed) {
		this.actor = actor;
		agent = actor.GetComponent<NavMeshAgent>();

		this.waypoints = waypoints;
		this.targetThreshold = targetThreshold;
		this.speed = speed;
	}

	public override void OnEnter() {
		agent.isStopped = false;
		agent.speed = speed;
		agent.angularSpeed = 360; // this value is already set on the component
		agent.acceleration = 8; // this value is already set on the component

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
			return typeof(WaryState);
		}
		return null;
	}
}

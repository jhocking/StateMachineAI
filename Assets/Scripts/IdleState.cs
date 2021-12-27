using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BasicAI;

public class IdleState : BaseState {
	private Enemy actor;
	private NavMeshAgent agent;

	private DateTime startTime;
	private float waitTime;

	public IdleState(Enemy actor, float waitTime) {
		this.actor = actor;
		agent = actor.GetComponent<NavMeshAgent>();

		this.waitTime = waitTime;
	}

	public override void OnEnter() {
		startTime = DateTime.UtcNow;
		agent.isStopped = true;
	}

	public override Type Tick() {
		if (DateTime.UtcNow.Subtract(startTime).TotalSeconds > waitTime) {
			return typeof(PatrolState);
		}
		return null;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BasicAI;

public class IdleState : BaseState {
	private GameObject player;
	private NavMeshAgent agent;

	private DateTime startTime;
	private float waitTime;

	public IdleState(GameObject actor, GameObject player, float waitTime) {
		this.waitTime = waitTime;

		this.player = player;
		agent = actor.GetComponent<NavMeshAgent>();
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

	public override void OnExit() {
		Debug.Log("Left Idle");
	}
}

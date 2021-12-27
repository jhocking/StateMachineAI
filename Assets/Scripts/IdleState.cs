using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BasicAI;

public class IdleState : BaseState {
	private GameObject player;
	private NavMeshAgent agent;

	public IdleState(GameObject actor, GameObject player) {
		this.player = player;
		agent = actor.GetComponent<NavMeshAgent>();
	}

	public override void OnEnter() {
		agent.isStopped = true;
	}

	public override Type Tick() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			return typeof(PatrolState);
		}
		return null;
	}

	public override void OnExit() {
		Debug.Log("Left Idle");
	}
}

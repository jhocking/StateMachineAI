using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BasicAI;

public class WaryState : BaseState {
	private Enemy actor;
	private NavMeshAgent agent;

	private TextMesh symbol;

	private DateTime startTime;
	private float waitTime;

	public WaryState(Enemy actor, float waitTime, TextMesh symbol) {
		this.actor = actor;
		agent = actor.GetComponent<NavMeshAgent>();

		this.symbol = symbol;

		this.waitTime = waitTime;
	}

	public override void OnEnter() {
		startTime = DateTime.UtcNow;
		agent.isStopped = true;

		symbol.gameObject.SetActive(true);
		symbol.text = actor.CanSeePlayer ? "!" : "?";
	}

	public override Type Tick() {
		if (DateTime.UtcNow.Subtract(startTime).TotalSeconds > waitTime) {
			if (actor.CanSeePlayer || actor.IsDetectingPlayer) {
				return typeof(ChaseState);
			} else {
				return typeof(PatrolState);
			}
		}
		return null;
	}

	public override void OnExit() {
		symbol.gameObject.SetActive(false);
	}
}

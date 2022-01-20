using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BasicAI;

public class WaryState : BaseState {
	private Enemy actor;

	private TMP_Text symbol;

	private DateTime startTime;
	private float waitTime;

	public WaryState(Enemy actor, float waitTime, TMP_Text symbol) {
		this.actor = actor;
		this.symbol = symbol;

		this.waitTime = waitTime;
	}

	public override void OnEnter() {
		startTime = DateTime.UtcNow;
		actor.Agent.isStopped = true;

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

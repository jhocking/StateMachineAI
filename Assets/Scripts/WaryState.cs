using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BasicAI;

public class WaryState : BaseState {
	private readonly Enemy actor;
	private readonly TMP_Text symbol;

	private readonly float waitTime;

	private DateTime startTime;

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
		var elapsed = DateTime.UtcNow.Subtract(startTime).TotalSeconds;
		if (elapsed > waitTime) {
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

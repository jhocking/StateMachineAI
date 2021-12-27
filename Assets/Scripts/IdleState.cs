using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicAI;

public class IdleState : BaseState {
	public override void OnEnter() {
		Debug.Log("Entered idle");
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

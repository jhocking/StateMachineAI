using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
refer to zelda3 for how enemies behave: 
slowly patrol waypoint path until see player
brief pause for guard noticing player, then give chase
if the player goes around a corner just move to last place seen
once reach that target check if can see player now
if not then pause (maybe look around) before returning to patrol
navmesh back to path, with obstacle avoidance at all times

resources for first pass:
18:40 state machine https://www.youtube.com/watch?v=YdERlPfwUb0
https://www.raywenderlich.com/16977649-pathfinding-with-navmesh-getting-started
simply do waypoints like FlockEmitter in flock-demo

later improvements:
waypoint editor https://www.youtube.com/watch?v=MXCZ-n5VyJc
improved behavior https://www.youtube.com/watch?v=6BrZryMz-ac
sensor toolkit https://www.youtube.com/watch?v=37z6hTHuJRI
*/

namespace BasicAI {

	public abstract class BaseWaypointAI : MonoBehaviour {
		[SerializeField] protected Transform[] waypoints;

		protected Dictionary<Type, BaseState> availableStates;

		protected BaseState currentState;
		public string CurrentState => currentState.GetType().Name;

		// Start is called before the first frame update
		protected virtual void Start() {
			// TODO override this method; do NOT call base.Start()
			// then initialize the dictionary of states
			// (including creating a state with the waypoints)
			// and declare the current state
		}

		// Update is called once per frame
		protected virtual void Update() {
			if (currentState == null) {
				Debug.LogError($"Current state not initialized for {this.name}");
				return;
			}
			var stateType = currentState.Tick();

			if (stateType != null && stateType != currentState.GetType()) {
				availableStates.TryGetValue(stateType, out var newState);
				if (newState == null) {
					Debug.LogError($"No available state for {stateType.Name}");
				} else {
					currentState.OnExit();
					currentState = newState;
					currentState.OnEnter();
				}
			}
		}
	}

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicAI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Enemy : BaseWaypointAI
{
    [SerializeField] GameObject player;

    public float idleWaitTime = 2; // how long to pause in the idle state
    public float visionWaitTime = .5f; // how long between vision updates
    public float visionDistance = 50; // how far away the player is visible
    public float visionRadius = .25f; // width of the visibility spherecast
    public float patrolSpeed = 4;
    public float chaseSpeed = 8;

    [Range(0.1f, 0.9f)]
    public float facingDotThreshold = .35f; // lower is a wider field of vision

    public bool CanSeePlayer { get; private set; }
    public Vector3 SeenPlayerPosition { get; private set; }

    private Coroutine visionLoop;

    // Start is called before the first frame update
    protected override void Start()
    {
        currentState = new IdleState(this, idleWaitTime);

        availableStates = new Dictionary<Type, BaseState>() {
            { typeof(IdleState), currentState},
            { typeof(PatrolState), new PatrolState(this, waypoints, targetThreshold, patrolSpeed)},
            { typeof(ChaseState), new ChaseState(this, targetThreshold, chaseSpeed)}
        };

        visionLoop = StartCoroutine(VisionCoroutine());
    }

    private IEnumerator VisionCoroutine() {

        // offset so multiple enemies execute on different frames
        var offset = UnityEngine.Random.value;
        yield return new WaitForSeconds(offset);

        while (player != null) {
            CanSeePlayer = false;
            var playerOffset = player.transform.position - this.transform.position;

            // first check if the player is close enough
            if (playerOffset.magnitude < visionDistance) {

                // then use the dot product to see if the player is within the field of view
                var dot = Vector3.Dot(transform.forward, playerOffset.normalized);
                if (dot >= facingDotThreshold) {

                    // only then do a raycast for line of sight
                    if (Physics.SphereCast(transform.position, visionRadius, playerOffset, out var hit)) {
                        if (hit.transform.gameObject == player) {
                            CanSeePlayer = true;
                            SeenPlayerPosition = player.transform.position;
                        }
                    }

                    if (showRuntimeDebug) {
                        var tint = CanSeePlayer ? Color.blue : Color.yellow;
                        Debug.DrawRay(transform.position, playerOffset, tint, visionWaitTime);
                        // TODO instead of 'transform.forward * 1000' do 'transform.forward * hit.distance'
                    }
                }
            }

            // pause before checking again, to simulate reaction time
            yield return new WaitForSeconds(visionWaitTime);
		}
	}
}

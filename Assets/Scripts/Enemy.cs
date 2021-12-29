using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicAI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Enemy : BaseWaypointAI
{
    [SerializeField] GameObject player;

    public float waryWaitTime = 1; // how long to pause in the wary state
    public float visionWaitTime = .5f; // how long between vision updates
    public float visionRadius = .25f; // width of the visibility spherecast
    public float visionDistance = 50; // how far away the player is visible
    public float detectDistance = 15; // how far away to detect player regardless of visibility
    public float patrolSpeed = 4;
    public float chaseSpeed = 8;

    [Range(0.1f, 0.9f)]
    public float facingDotThreshold = .35f; // lower is a wider field of vision

    public bool CanSeePlayer { get; private set; }
    public bool IsDetectingPlayer { get; private set; } // sensing other than vision
    public Vector3 LastPlayerPosition { get; private set; }

    private Coroutine visionLoop;

    // Start is called before the first frame update
    protected override void Start()
    {
        currentState = new WaryState(this, waryWaitTime);

        availableStates = new Dictionary<Type, BaseState>() {
            { typeof(WaryState), currentState},
            { typeof(PatrolState), new PatrolState(this, waypoints, targetThreshold, patrolSpeed)},
            { typeof(ChaseState), new ChaseState(this, targetThreshold, chaseSpeed)}
        };

        visionLoop = StartCoroutine(VisionCoroutine());
    }

    private IEnumerator VisionCoroutine() {

        // offset so multiple enemies execute on different frames
        var offset = UnityEngine.Random.value * visionWaitTime;
        yield return new WaitForSeconds(offset);

        while (player != null) {
            CanSeePlayer = false;
            IsDetectingPlayer = false;
            var doWaitTime = currentState.GetType() == typeof(PatrolState);

            var playerOffset = player.transform.position - this.transform.position;

            // first check if the player is close enough to see
            var dist = playerOffset.magnitude;
            if (dist < visionDistance) {

                // then use the dot product to see if the player is within the field of view
                var dot = Vector3.Dot(transform.forward, playerOffset.normalized);
                var doRaycast = dot >= facingDotThreshold;

                // only then do a raycast for line of sight
                if (doRaycast) {
                    if (Physics.SphereCast(transform.position, visionRadius, playerOffset, out var hit)) {
                        if (hit.transform.gameObject == player) {
                            CanSeePlayer = true;
                            LastPlayerPosition = player.transform.position;
                        }
                    }

                    if (showRuntimeDebug) {
                        var tint = CanSeePlayer ? Color.blue : Color.yellow;
                        if (doWaitTime) {
                            Debug.DrawRay(transform.position, playerOffset, tint, visionWaitTime);
                        } else {
                            Debug.DrawRay(transform.position, playerOffset, tint);
                        }
                    }

                // update very close positions regardless of visibility
                } else if (dist < detectDistance) {
                    IsDetectingPlayer = true;
                    LastPlayerPosition = player.transform.position;
                }
            }

            // while patrolling: pause before checking again, to simulate reaction time
            if (doWaitTime) {
                yield return new WaitForSeconds(visionWaitTime);
            } else {
                yield return null;
            }
		}
	}
}

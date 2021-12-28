using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicAI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Enemy : BaseWaypointAI
{
    [SerializeField] GameObject player;

    public float idleWaitTime = 2;
    public float visionWaitTime = 1;

    [Range(0.1f, 0.9f)]
    public float facingDotThreshold = .3f; // lower is a wider field of vision

    public bool CanSeePlayer { get; private set; }
    public Vector3 SeenPlayerPosition { get; private set; }

    private Coroutine visionLoop;

    // Start is called before the first frame update
    protected override void Start()
    {
        currentState = new IdleState(this, idleWaitTime);

        availableStates = new Dictionary<Type, BaseState>() {
            { typeof(IdleState), currentState},
            { typeof(PatrolState), new PatrolState(this, waypoints, targetThreshold)}
        };

        visionLoop = StartCoroutine(VisionCoroutine());
    }

    private IEnumerator VisionCoroutine() {

        // offset so multiple enemies execute on different frames
        var offset = UnityEngine.Random.value;
        yield return new WaitForSeconds(offset);

        while (player != null) {
            CanSeePlayer = false;

            // first use the dot product to see if the player is within the field of view
            var playerDir = player.transform.position - this.transform.position;
            var dot = Vector3.Dot(transform.forward, playerDir);
            if (dot >= facingDotThreshold) {
                Debug.Log($"facing {DateTime.UtcNow}");

                // only then do a raycast for line of sight
                if (Physics.Raycast(transform.position, transform.forward, out var hit)) {
                    if (hit.transform.gameObject == player) {
                        CanSeePlayer = true;
                        SeenPlayerPosition = player.transform.position;
                    }
				}
            }

            if (showRuntimeDebug) {
                var tint = CanSeePlayer ? Color.green : Color.yellow;
                Debug.DrawRay(transform.position, transform.forward * 1000, tint);
            }

            // pause before checking again, to simulate reaction time
            yield return new WaitForSeconds(visionWaitTime);
		}
	}
}

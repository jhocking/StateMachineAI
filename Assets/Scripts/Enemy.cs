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
            // TODO check player line of sight

            // pause before checking again, to simulate reaction time
            yield return new WaitForSeconds(visionWaitTime);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface INPCState
{
    void Begin(NPC NPC) { }
    void End(NPC NPC) { }
    void Update(NPC NPC) { }
}

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public INPCState currentState;
    public Route route;
    int currentPoint = 0;
    public int routeOffset = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentPoint = routeOffset;
        agent.SetDestination(route.points[currentPoint].position);
        NPCStart();
    }

    protected virtual void NPCStart()
    {

    }

    void Update()
    {
        currentState?.Update(this);
    }

    internal protected void PathFind()
    {
        if (agent.remainingDistance < 0.3f)
        {

            currentPoint++;

            if (currentPoint >= route.points.Count) 
                currentPoint = 0;
        }
        agent.SetDestination(route.points[currentPoint].position);
    }

    protected virtual void EventHandler()
    {

    }

    internal protected void TransitionState(INPCState state)
    {
        currentState?.End(this);

        currentState = state;
        currentState.Begin(this);
    }
}

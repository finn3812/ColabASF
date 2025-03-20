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
    NavMeshAgent agent;
    INPCState currentState;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        NPCStart();
    }

    protected virtual void NPCStart()
    {

    }

    void Update()
    {
        currentState?.Update(this);
    }

    protected virtual void EventHandler()
    {

    }

    void TransitionState(INPCState state)
    {
        currentState?.End(this);

        currentState = state;
        currentState.Begin(this);
    }
}

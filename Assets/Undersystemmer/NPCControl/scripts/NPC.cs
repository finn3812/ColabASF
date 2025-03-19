using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INPCState
{
    void Begin(NPC NPC) { }
    void End(NPC NPC) { }
    void Update(NPC NPC) { }
}

public class NPC : MonoBehaviour
{
    INPCState currentState;

    void Start()
    {
        
    }

    void Update()
    {
        currentState?.Update(this);
    }

    void TransitionState(INPCState state)
    {
        currentState?.End(this);

        currentState = state;
        currentState.Begin(this);
    }
}

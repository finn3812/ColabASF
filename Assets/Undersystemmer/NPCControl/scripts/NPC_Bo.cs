using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Bo;

namespace Bo
{
    class Idle : INPCState
    {

    }

    class Roam : INPCState
    {

    }

    class Jagt : INPCState
    {

    }
}

public class NPC_Bo : NPC
{
    Idle Idle = new();
    Roam Roam = new();
    Jagt Jagt = new();

    protected override void NPCStart()
    {
        TransitionState(Idle);
    }

    protected override void EventHandler()
    {

    }
}

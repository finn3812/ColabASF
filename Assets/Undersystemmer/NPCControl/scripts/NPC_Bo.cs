using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Bo;

namespace Bo
{
    public class Idle : INPCState
    {

    }

    public class Roam : INPCState
    {

    }

    public class Jagt : INPCState
    {

    }
}

public class NPC_Bo : NPC
{
    protected internal Idle Idle = new();
    protected internal Roam Roam = new();
    protected internal Jagt Jagt = new();

    protected override void NPCStart()
    {
        TransitionState(Idle);
    }

    protected override void EventHandler()
    {

    }
}

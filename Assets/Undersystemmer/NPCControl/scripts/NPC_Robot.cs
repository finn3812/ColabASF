using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Robot;

namespace Robot
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

public class NPC_Robot : NPC
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Bo;
using UnityEngine.Events;

namespace Bo
{
    public class Idle : INPCState
    {

    }

    public class Roam : INPCState
    {
        public void Update(NPC NPC)
        {
            NPC.PathFind();
        }
    }

    public class Jagt : INPCState
    {
        public void Update(NPC NPC)
        {
            if (NPC is NPC_Bo npcBo)
                npcBo.Dikter.Invoke();
        }
    }
}

public class NPC_Bo : NPC
{
    protected internal Idle Idle = new();
    protected internal Roam Roam = new();
    protected internal Jagt Jagt = new();

    public UnityEvent Dikter = new UnityEvent();

    protected override void NPCStart()
    {
        TransitionState(Roam);
    }

    protected override void EventHandler()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Finn;

namespace Finn
{
    public class Idle : INPCState
    {
        public void Begin(NPC NPC)
        {
            Debug.Log("Virkern?");
            if (NPC is NPC_Finn npcFinn)
            {
                npcFinn.TransitionState(npcFinn.Roam);
            }
        }
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

    }

    public class RobotControl : INPCState
    {

    }
}

public class NPC_Finn : NPC
{
    protected internal Idle Idle = new();
    protected internal Roam Roam = new();
    protected internal Jagt Jagt = new();
    protected internal RobotControl RobotControl = new();

    protected override void NPCStart()
    {
        TransitionState(Idle);
    }

    //protected override void EventHandler()
    //{

    //}
}

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
        public void Update(NPC NPC)
        {
            if (NPC is NPC_Finn npcF)
            {
                if (npcF.dikteret == 1)
                {
                    npcF.GOTO(npcF.player.transform.position);
                    return;
                }
                npcF.Hunt();

                if (npcF.DistanceToPlayer() > 5f)
                    npcF.TransitionState(npcF.Idle);
            }
        }
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

    protected override void NPCUpdate()
    {
        base.NPCUpdate();
        if (DistanceToPlayer() < 5f)
            TransitionState(Jagt);
    }

    protected override void BoDikterer(int s)
    {
        base.BoDikterer(s);
        if (s == 1)
            TransitionState(Jagt);
        else
            TransitionState(Idle);
    }
}
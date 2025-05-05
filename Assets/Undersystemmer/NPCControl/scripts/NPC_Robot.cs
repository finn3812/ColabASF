using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Robot;

namespace Robot
{
    public class Idle : INPCState
    {
        public void Update(NPC NPC)
        {
            if (NPC is NPC_Robot npcF)
            {
                if (npcF.tid == Tid.Modul.Programmering)
                    npcF.TransitionState(npcF.Roam);
            }
        }
    }

    public class Roam : INPCState
    {
        public void Update(NPC NPC)
        {
            NPC.PathFind();

            if (NPC is NPC_Robot npcF)
            {
                if (npcF.tid != Tid.Modul.Programmering)
                    npcF.TransitionState(npcF.Idle);
            }
        }
    }

    public class Jagt : INPCState
    {
        public void Update(NPC NPC)
        {
            if (NPC is NPC_Robot npcF)
            {
                npcF.Hunt();

                if (npcF.DistanceToPlayer() > 8f)
                    npcF.TransitionState(npcF.Idle);
                if (npcF.tid != Tid.Modul.Programmering)
                    npcF.TransitionState(npcF.Idle);
            }
        }
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

    protected override void NPCUpdate()
    {
        base.NPCUpdate();
        if (DistanceToPlayer() < 8f && tid == Tid.Modul.Programmering)
            TransitionState(Jagt);
    }
}

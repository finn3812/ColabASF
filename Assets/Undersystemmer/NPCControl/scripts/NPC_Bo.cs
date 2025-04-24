using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Bo;
using UnityEngine.Events;
using TMPro.EditorUtilities;

namespace Bo
{
    public class Idle : INPCState
    {
        public void Update(NPC NPC)
        {
            if (NPC is NPC_Bo npcBo)
            {
                if (npcBo.tid == Tid.Modul.Idehistorie)
                    npcBo.TransitionState(npcBo.Roam);

                npcBo.GOTO(npcBo.laerervaerelse.transform.position);
            }
        }
    }

    public class Roam : INPCState
    {
        public void Update(NPC NPC)
        {
            NPC.PathFind();

            if (NPC is NPC_Bo npcBo)
            {
                if (npcBo.tid != Tid.Modul.Idehistorie)
                    npcBo.TransitionState(npcBo.Idle);
            }
        }
    }

    public class Jagt : INPCState
    {
        public void Update(NPC NPC)
        {
            if (NPC is NPC_Bo npcBo)
            {
                if (npcBo.DistanceToPlayer() > 5f)
                    npcBo.TransitionState(npcBo.Idle);

                npcBo.Dikter.Invoke();
            }
        }
    }
}

public class NPC_Bo : NPC
{
    protected internal Idle Idle = new();
    protected internal Roam Roam = new();
    protected internal Jagt Jagt = new();

    public UnityEvent Dikter = new UnityEvent();

    public GameObject laerervaerelse;

    protected override void NPCStart()
    {
        TransitionState(Idle);
    }

    protected override void NPCUpdate()
    {
        if (DistanceToPlayer() <= 5f)
        {
            TransitionState(Jagt);
        }
    }

    //protected override void EventHandler()
    //{

    //}
}

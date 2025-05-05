using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using HJ;
using Unity.VisualScripting;

namespace HJ
{
    public class Idle : INPCState
    {
        public void Begin(NPC NPC) {
            if (NPC is NPC_HJ npcHJ)
            {
                npcHJ.debug.Log("Virkeren?");
                npcHJ.TransitionState(npcHJ.Roam);
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

    public class Rundstykke : INPCState
    {

    }

    public class Jagt : INPCState
    {
        public void Update(NPC NPC)
        {
            if (NPC is NPC_HJ npcHJ)
            {
                if (npcHJ.dikteret == 1)
                {
                    npcHJ.GOTO(npcHJ.player.transform.position);
                    return;
                }
                npcHJ.Hunt();

                if (npcHJ.DistanceToPlayer() > 5f)
                    npcHJ.TransitionState(npcHJ.Idle);
            }
        }
    }

    public class Forsvind : INPCState
    {
        public void Begin(NPC NPC)
        {
            if (NPC is NPC_HJ npcHJ)
            {
                npcHJ.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }

        public void Update(NPC NPC)
        {
            if (NPC is NPC_HJ npcHJ)
            {
                if (npcHJ.tid != Tid.Modul.PU)
                {
                    npcHJ.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    npcHJ.TransitionState(npcHJ.Idle);
                }
            }
        }
    }
}

public class NPC_HJ : NPC
{
    protected internal Idle Idle = new();
    protected internal Roam Roam = new();
    protected internal Rundstykke Rundstykke = new();
    protected internal Jagt Jagt = new();
    protected internal Forsvind Forsvind = new();

    protected override void NPCStart()
    {
        TransitionState(Idle);
    }

    protected override void NPCUpdate()
    {
        base.NPCUpdate();
        if (DistanceToPlayer() < 5f)
            TransitionState(Jagt);
        if (tid == Tid.Modul.PU)
            TransitionState(Forsvind);
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

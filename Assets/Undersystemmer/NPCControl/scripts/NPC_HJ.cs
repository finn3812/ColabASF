using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

class Idle : INPCState
{
    
}

class Roam : INPCState
{

}

class Rundstykke : INPCState
{

}

class Jagt : INPCState
{

}

class Forsvind :INPCState
{

}

public class NPC_HJ : NPC
{
    Idle Idle = new();
    Roam Roam = new();
    Rundstykke Rundstykke = new();
    Jagt Jagt = new();

    protected override void NPCStart()
    {
        TransitionState(Idle);
    }

    protected override void EventHandler()
    {
        
    }
}

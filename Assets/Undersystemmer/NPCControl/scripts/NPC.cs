using NikUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public interface INPCState
{
    void Begin(NPC NPC) { }
    void End(NPC NPC) { }
    void Update(NPC NPC) { }
}

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public INPCState currentState;
    public Route route; 
    public int routeOffset = 0;
    public UnityEvent<int> Bo;
    public nDebug debug = new();
    public GameObject player = null;

    public int dikteret = 0; 

    int currentPoint = 0;
    public Tid.Modul tid; // Vi gør noget farligt og ikke giver den er værdi med det samme :P

    private void Awake()
    {
        if (this is NPC_Bo)
            return;
        Bo = GameObject.Find("Bo").GetComponent<NPC_Bo>().Dikter;
        Bo.AddListener(BoDikterer);
    }

    void Start()
    {
        player = PlayerController.instance.gameObject;

        Tid.instance.TimeHasChanged.AddListener(EventHandler);

        debug.debug = true;
        debug.name = gameObject.name;

        agent = GetComponent<NavMeshAgent>();
        currentPoint = routeOffset;

        NPCStart();
    }

    protected virtual void NPCStart()
    {

    }

    protected virtual void NPCUpdate()
    {

    }

    protected virtual void BoDikterer(int status)
    {
        dikteret = status;
        debug.Log("Bo har dikteret " + dikteret);
    }

    void Update()
    {
        currentState?.Update(this);
        NPCUpdate();
    }

    internal protected void PathFind()
    {
        if (agent.remainingDistance < 0.3f)
        {
            currentPoint++;

            if (currentPoint >= route.points.Count) 
                currentPoint = 0;
        }
        agent.SetDestination(route.points[currentPoint].position);
    }

    internal protected void GOTO(Vector3 pos)
    {
        agent.SetDestination(pos);
    }

    protected virtual void EventHandler(Tid.Modul modul)
    {
        tid = modul;
        debug.Log(tid.ToString());
    }

    internal protected void TransitionState(INPCState state)
    {
        currentState?.End(this);

        currentState = state;
        currentState.Begin(this);
    }

    internal protected float DistanceToPlayer()
    {
        return Vector3.Distance(gameObject.transform.position, player.transform.position);
    }

    internal protected void Hunt()
    {
        GOTO(player.transform.position);
    }
}

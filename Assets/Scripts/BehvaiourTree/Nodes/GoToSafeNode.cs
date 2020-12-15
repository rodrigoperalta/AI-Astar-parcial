using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GoToSafeNode : Node
{
    
    private NavMeshAgent agent;
    private Wolf wolf;

    public GoToSafeNode( NavMeshAgent agent, Wolf wolf)
    {       
        this.agent = agent;
        this.wolf = wolf;
    }

    public override NodeState Evaluate()
    {
        Transform safeSpot = wolf.GetBestSafeSpot();
        if (safeSpot == null)
            return NodeState.FAILURE;
        float distance = Vector3.Distance(safeSpot.position, agent.transform.position);
        if (distance>0.2f)
        {
            agent.isStopped = false;
            agent.SetDestination(safeSpot.position);
            return NodeState.RUNNING;
        }
        else
        {
            agent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }
}

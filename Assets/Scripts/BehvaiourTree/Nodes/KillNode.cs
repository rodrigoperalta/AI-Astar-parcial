using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KillNode : Node
{
    private NavMeshAgent agent;
    private Wolf wolf;
    private bool kill;

    public KillNode(NavMeshAgent agent, Wolf wolf)
    {
        this.agent = agent;
        this.wolf = wolf;
    }

    public override NodeState Evaluate()
    {
        if (kill)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Miner")
        {
            kill = true;            
        }
    }
}

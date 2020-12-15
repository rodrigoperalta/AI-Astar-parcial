using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChaseNode : Node
{
    private Transform target;
    private Transform origin;
    private NavMeshAgent agent;
    private Wolf wolf;

    public ChaseNode(Transform target, Transform origin, Wolf wolf)
    {
        this.target = target;
        this.origin = origin;
        this.wolf = wolf;
        
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(target.position, origin.position);
        if (distance>0.2f)
        {
            origin.position = Vector3.MoveTowards(origin.position, target.position, 0.003f);
            return NodeState.RUNNING;
        }
        else
        {            
            return NodeState.SUCCESS;
        }
    }
}

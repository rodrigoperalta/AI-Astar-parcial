using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChaseNode : Node
{
    private Transform origin;
    private int randomMiner;
    private List<GameObject> miners = new List<GameObject>();

    public ChaseNode(Transform origin)
    {       
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        miners = origin.GetComponent<Wolf>().GetMiners();
        
        if (miners.Count > 0)
        {
            if (origin.GetComponent<Wolf>().GetTarget() == null)
            {
                randomMiner = Random.Range(0, miners.Count);
                origin.GetComponent<Wolf>().SetTarget(miners[randomMiner].transform);
                return NodeState.SUCCESS;
            }
            else
            {
                float distance = Vector3.Distance(origin.GetComponent<Wolf>().GetTarget().position, origin.position);
                if (distance > 0.1f)
                {
                    origin.position = Vector3.MoveTowards(origin.position, origin.GetComponent<Wolf>().GetTarget().position, 0.03f);
                    return NodeState.RUNNING;
                }
                else                                   
                    return NodeState.SUCCESS;
            }
        }
        else
            return NodeState.FAILURE;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChaseNode : Node
{
    private Transform target;
    private Transform origin;        
    private List<GameObject> miners = new List<GameObject>();

    public ChaseNode(Transform origin)
    {       
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Miner"))
        {
            miners.Add(fooObj);
        }
        if (target == null)
        {
            target = miners[Random.Range(0, miners.Count)].transform;
        }       
        float distance = Vector3.Distance(target.position, origin.position);
        if (distance>0.2f)
        {
            origin.position = Vector3.MoveTowards(origin.position, target.position, 0.03f);
            return NodeState.RUNNING;
        }
        else
        {            
            return NodeState.SUCCESS;
        }
    }
}

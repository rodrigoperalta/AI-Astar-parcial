using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChaseNode : Node
{
    private Transform target;
    private Transform origin;
    private int randomMiner;
    private List<GameObject> miners = new List<GameObject>();

    public ChaseNode(Transform origin)
    {       
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Miner"))
        {
           // miners.Add(fooObj);
            if (miners.IndexOf(fooObj) < 0)            
                miners.Add(fooObj);            
        }
        Debug.Log("Chase count " + miners.Count);
        if (miners.Count>0)
        {
            if (target == null)
            {
                randomMiner = Random.Range(0, miners.Count);
                target = miners[randomMiner].transform;
            }
            float distance = Vector3.Distance(target.position, origin.position);
            if (distance > 0.2f)
            {
                origin.position = Vector3.MoveTowards(origin.position, target.position, 0.03f);
                return NodeState.RUNNING;
            }
            else
            {
                Debug.Log("Saco al " + randomMiner);
                miners.RemoveAt(randomMiner);
                target = null;                
                return NodeState.SUCCESS;
            }
        }
        else
        {
            Debug.Log("warning");
            return NodeState.RUNNING;
        }
       

    }
}

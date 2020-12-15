using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GoToSafeNode : Node
{  
    private Wolf wolf;
    private List<GameObject> safes;

    public GoToSafeNode( Wolf wolf, List<GameObject> safes)
    {   
        this.wolf = wolf;
        this.safes = safes;
    }

    public override NodeState Evaluate()
    {
        Transform safeSpot;
        float distanceA = Vector3.Distance(safes[0].transform.position, wolf.transform.position);
        float distanceB = Vector3.Distance(safes[1].transform.position, wolf.transform.position);
        if (distanceA < distanceB)
        {
            safeSpot = safes[0].transform;
        }
        else
        {
            safeSpot = safes[1].transform;
        }        
        if (safeSpot == null)
            return NodeState.FAILURE;
        float distance = Vector3.Distance(safeSpot.position, wolf.transform.position);
        if (distance>0.2f)
        {
            wolf.transform.position = Vector3.MoveTowards(wolf.transform.position, safeSpot.position, 0.03f);
            return NodeState.RUNNING;
        }
        else           
            return NodeState.SUCCESS;
    }
}

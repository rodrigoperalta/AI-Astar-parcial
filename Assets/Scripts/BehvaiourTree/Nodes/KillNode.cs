using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KillNode : Node
{
    private Wolf wolf;   

    public KillNode(Wolf wolf)
    {
        this.wolf = wolf;
    }

    public override NodeState Evaluate()
    {  
        if (wolf.GetTarget()!=null)
        {
            float distance = Vector3.Distance(wolf.GetTarget().position, wolf.transform.position);
            if (distance < 2.5f)
            {                
                wolf.RemoveFromMiner(wolf.GetTarget().gameObject);
                wolf.LoseStamina();
                wolf.GetTarget().GetComponent<Miner>().Die();
                wolf.SetTarget(null);

                return NodeState.SUCCESS;
            }
        }
        return NodeState.FAILURE;
    }
}

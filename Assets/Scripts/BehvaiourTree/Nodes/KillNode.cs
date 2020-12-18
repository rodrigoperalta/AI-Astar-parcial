using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KillNode : Node
{
    private NavMeshAgent agent;
    private List<GameObject> miners = new List<GameObject>();
    //private GameObject[] miners;
    private Wolf wolf;
    private bool kill;

    public KillNode(Wolf wolf)
    {
        this.wolf = wolf;
    }

    public override NodeState Evaluate()
    {  
        if (wolf.GetTarget()!=null)
        {
            float distance = Vector3.Distance(wolf.GetTarget().position, wolf.transform.position);
            if (distance < 0.2f)
            {
                Debug.Log("Mate");
                wolf.GetTarget().GetComponent<Miner>().Die();
                wolf.LoseStamina();
                wolf.SetTarget(null);

                return NodeState.SUCCESS;
            }
        }
        return NodeState.FAILURE;
    }
}

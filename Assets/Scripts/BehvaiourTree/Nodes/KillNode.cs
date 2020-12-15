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

    public KillNode( Wolf wolf)
    {        
        this.wolf = wolf;
    }

    public override NodeState Evaluate()
    {
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Miner"))        
            miners.Add(fooObj);
        
        for (int i = 0; i < miners.Count; i++)
        {
            float distance = Vector3.Distance(miners[i].transform.position, wolf.transform.position);
            if (distance < 1.0f)
            {
                Debug.Log("Mate");
                miners[i].GetComponent<Miner>().Die();
                return NodeState.SUCCESS;
            }
        }
        return NodeState.FAILURE;
    }   
}

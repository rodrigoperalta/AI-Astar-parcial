using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSafeNode : Node
{

    private List<GameObject> safes;
    private GameObject wolf;

    public IsSafeNode(List<GameObject> safes, GameObject origin)
    {
        this.safes = safes;
        this.wolf = origin;
    }

    public override NodeState Evaluate()
    {
        float distanceA = Vector3.Distance(safes[0].transform.position, wolf.transform.position);
        float distanceB = Vector3.Distance(safes[1].transform.position, wolf.transform.position);
        if (distanceA < 0.2f || distanceB < 0.2f)
        {
            wolf.GetComponent<Wolf>().GetStamina();
            return NodeState.SUCCESS;
        }
           
        return NodeState.FAILURE;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSafeNode : Node
{ 

    private Transform target;
    private Transform origin;
    private bool hitFire;

    public IsSafeNode(Transform target, Transform origin)
    {
        this.target = target;
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        if (hitFire)        
            return NodeState.SUCCESS;

        return NodeState.FAILURE;
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Fire")
        {
            hitFire = true;
        }
    }
}

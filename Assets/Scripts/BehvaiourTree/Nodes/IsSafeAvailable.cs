using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSafeAvailable : Node
{
    private List<GameObject> safes;
    private Wolf wolf;
    GameObject spotAvailable;

    public IsSafeAvailable(List<GameObject> safes, Wolf wolf)
    {
        this.safes = safes;
        this.wolf = wolf;
    }

    public override NodeState Evaluate()
    {
        for (int i = 0; i < safes.Count; i++)
        {
            if (safes[i]!=null)            
               spotAvailable = safes[i];           
        }          
        return spotAvailable != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}

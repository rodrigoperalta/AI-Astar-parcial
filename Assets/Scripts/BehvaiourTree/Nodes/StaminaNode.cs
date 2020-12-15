using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaNode : Node
{
    private Wolf wolf;
    private float threshold;

    public StaminaNode(Wolf wolf, float threshold)
    {
        this.wolf = wolf;
        this.threshold = threshold;
    }

    public override NodeState Evaluate()
    {
        return wolf.currentStamina <= threshold ? NodeState.SUCCESS : NodeState.FAILURE;
    }

   
}

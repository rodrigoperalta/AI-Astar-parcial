using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSafeAvailable : Node
{
    public Safe[] availableSafe;
    private Transform target;
    private Wolf wolf;

    public IsSafeAvailable(Safe[] availableSafe, Transform target, Wolf wolf)
    {
        this.availableSafe = availableSafe;
        this.target = target;
        this.wolf = wolf;
    }

    public override NodeState Evaluate()
    {
        Transform bestSpot = FindBestSafeSpot();
        wolf.SetBestSafeSpot(bestSpot);
        return bestSpot != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    private Transform FindBestSafeSpot()
    {
        float minDistance = 5;
        Transform bestSpot = null;
        for (int i = 0; i < availableSafe.Length; i++)
        {
            Transform bestSpotInSafe = FindBestSpotInSafe(availableSafe[i], ref minDistance);
            if (bestSpotInSafe != null)
            {
                bestSpot = bestSpotInSafe;
            }
        }
        return bestSpot;
    }

    private Transform FindBestSpotInSafe(Safe safe, ref float minDistance)
    {
        Transform[] availableSpots = safe.GetSafeSpot();
        Transform bestSpot = null;
        for (int i = 0; i < availableSafe.Length; i++)
        {
            Vector3 direction = target.position - availableSpots[i].position;

            float distance = Vector3.Distance(availableSpots[i].position, direction);
            if (distance < minDistance)
            {
                minDistance = distance;
                bestSpot = availableSpots[i];
            }

        }
        return bestSpot;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{
    [SerializeField] private Transform[] safeSpots;

    public Transform[] GetSafeSpot()
    {
        return safeSpots;
    }
}

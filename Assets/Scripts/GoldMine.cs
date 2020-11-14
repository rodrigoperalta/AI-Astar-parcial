using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : MonoBehaviour
{
    private int gold;
    void Start()
    {
        gold = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (gold <= 0)        
            Destroy(this);
        
    }
}

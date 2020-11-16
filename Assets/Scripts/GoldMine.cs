using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : MonoBehaviour
{
  public float gold;
    void Start()
    {
        gold = 100;
    }

    void Update()
    {
        if (gold <= 0)        
            Destroy(this.gameObject);
    }

    public void LoseGold(float g)
    {
        gold -= g*Time.deltaTime;
    }
}

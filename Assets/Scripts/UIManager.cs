using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text goldText;
    public Text populationText;

    void Update()
    {
        goldText.text = "Gold: " + GameManager.Instance.GetGold().ToString();
        populationText.text = "Population: " + GameManager.Instance.GetPopulation().ToString() + "/20";
    }
}

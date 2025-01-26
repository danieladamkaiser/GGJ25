using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AllSteps : MonoBehaviour
{
    class Step
    {
        public bool filled = false;
        
        public string str()
        {
            return filled ? "[x]" : "[ ]";
        }
    }

    private MarketManager market;
    void Start()
    {
        market = FindObjectOfType<MarketManager>();
        
    }

    void Update()
    {
        var allSteps = Mathf.RoundToInt(market.GetTotalDuration());
        var progress = Mathf.RoundToInt(market.currentProgress);

        string res = "";
        for (int i = 0; i <= allSteps; i++)
        {
            var step = new Step();
            step.filled = i < progress;
            res += step.str();
        }

        var txt = GetComponent<TMP_Text>();
        txt.text = res;
        txt.color = market.stageText.color;
        txt.outlineColor = Color.black;
        txt.outlineWidth = 0.25f;
    }
}

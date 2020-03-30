using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealTime : MonoBehaviour
{
    public static float time;
    public readonly static float roundTime = 30f;

    reroll a;
    Text round;
    Text limit;

    public static bool isBattle = false;

    void Start()
    {
        a = GameObject.Find("리롤").GetComponent<reroll>();
        time = roundTime;
        round = transform.GetChild(0).gameObject.GetComponent<Text>();
        limit = transform.GetChild(1).gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (time != 0 && isBattle == false)
        {
            time -= Time.deltaTime;

            float n =1 - (time / 10f);

            if (n > 0)
                limit.color = new Color(n, 0, 0);
            else
                limit.color = new Color(0, 0, 0);
            if (time <= 0)
            {
                time = 0;

            }
        }
        TimeText();
    }

    void TimeText()
    {
        
        round.text =  "ROUND " + playervariable.Round.ToString();
        limit.text = string.Format("{0:N0}",time);
        
    }

    public void SkipButton()
    {
        time = 0;
    }
}

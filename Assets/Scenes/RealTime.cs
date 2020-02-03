﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealTime : MonoBehaviour
{
    public float time;
    reroll a;
    Text round;
    Text limit;

    void Start()
    {
        a = GameObject.Find("리롤").GetComponent<reroll>();
        time = 15f;
        round = transform.GetChild(0).gameObject.GetComponent<Text>();
        limit = transform.GetChild(1).gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (time != 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 0;
            }
        }
        else
        {
            playervariable.Round++;
            playervariable.playerexp += 2;
            playervariable.playergold += 7;
            a.OnClick();
            time = 15f;
        }
        realtime();
    }

    void realtime()
    {
        round.text =  "ROUND " + playervariable.Round.ToString();
        limit.text = string.Format("{0:N0}",time);
    }
}
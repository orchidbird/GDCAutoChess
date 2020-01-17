using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exp : MonoBehaviour
{
    public int requiredexp = 2;
    Text info;

    void Start()
    {
        info = transform.GetChild(0).gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if(playervariable.playerlevel == 3)
        {
            requiredexp = 6;
        }
        if(playervariable.playerlevel == 4)
        {
            requiredexp = 10;
        }
        if (playervariable.playerlevel == 5)
        {
            requiredexp = 20;
        }
        if (playervariable.playerlevel == 6)
        {
            requiredexp = 32;
        }
        if (playervariable.playerlevel == 7)
        {
            requiredexp = 50;
        }
        if (playervariable.playerlevel == 8)
        {
            requiredexp = 66;
        }
        if(playervariable.playerexp >= requiredexp)
        {
            playervariable.playerlevel++;
            playervariable.playerexp = playervariable.playerexp - requiredexp;
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(playervariable.playerlevel < 9 && playervariable.playergold >= 4)
            {
                playervariable.playerexp += 4;
                playervariable.playergold -= 4;
            }
        }
        expinfo();
    }
    
    void expinfo()
    {
        if (playervariable.playerlevel < 9)
        {
            info.text = playervariable.playerexp + "/" + requiredexp;
        }
        else
        {
            info.text = "MAX";
        }
    }
}

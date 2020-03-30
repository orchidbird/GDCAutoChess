using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    readonly playervariable a;
    public GameObject[] battleCard1 = new GameObject[12];
    public GameObject[] battleCard2 = new GameObject[12];


    void Update()
    {
        if(RealTime.isBattle == true)
        {
            StartCoroutine("FightMechanic");
        }
    }

    private IEnumerator FightMechanic()
    {
        for (int i = 0; i < 12; i++)
        {
            if (a.board[i] != 0)
                battleCard1[i] = GameObject.Find("BoardPlayer1").transform.GetChild(i + 8).gameObject;
        }

        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reroll : MonoBehaviour
{
    public void click()
    {
        Shop a = GameObject.Find("상점버튼").GetComponent<Shop>();
        for (int i = 0; i < 4; i++)
        {
            if (a.shop[i])
            {
                a.unit[a.num[i]-1]++;
            }
            a.shop[i] = true;
            a.UnitObject[i].SetActive(true);
            a.num[i] = Random.Range(1, 5);
            a.UnitObject[i].GetComponent<Image>().sprite = Resources.Load("Unit" + a.num[i].ToString(), typeof(Sprite)) as Sprite;
            a.unit[a.num[i]-1]--;
        }
    }
}

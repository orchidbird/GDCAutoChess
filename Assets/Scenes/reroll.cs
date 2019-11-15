using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reroll : MonoBehaviour
{
    public void click()
    {
		playervariable a = GameObject.Find("field").GetComponent<playervariable>();
        for (int i = 0; i < 4; i++)
        {
            if (a.shop[i])
            {
                a.unit[a.num[i]-1]++;
            }
            a.shop[i] = true;
            a.UnitObject[i].SetActive(true);
            a.num[i] = Random.Range(1, 17);
            a.UnitObject[i].GetComponent<Image>().sprite = Resources.Load("카드배경_1", typeof(Sprite)) as Sprite;  //카드 이미지 보여주기
            a.UnitObject[i].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load(a.num[i].ToString(), typeof(Sprite)) as Sprite;
            a.UnitObject[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("카드템플릿_1", typeof(Sprite)) as Sprite;
            if (a.num[i] < 14)
				a.UnitObject[i].transform.GetChild(1).GetComponent<Text>().text = "1$";
			else
				a.UnitObject[i].transform.GetChild(1).GetComponent<Text>().text = "4$";
			a.unit[a.num[i]-1]--;
        }
    }
}

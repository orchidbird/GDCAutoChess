using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reroll : MonoBehaviour
{
    public void OnClick()
    {
		playervariable a = GameObject.Find("field").GetComponent<playervariable>();
        if(playervariable.playergold >= 2)
            playervariable.playergold -= 2;
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
            a.UnitObject[i].transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Resources.Load("카드템플릿_1", typeof(Sprite)) as Sprite;
            a.UnitObject[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                Resources.Load("Synergy/속성_" + a.heroMap[a.nameOfHero[a.num[i]-1].ToString()].heroType.ToString(), typeof(Sprite)) as Sprite;
            a.UnitObject[i].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite =
                Resources.Load("Synergy/클래스_" + a.heroMap[a.nameOfHero[a.num[i]-1].ToString()].heroClass.ToString(), typeof(Sprite)) as Sprite;
            a.UnitObject[i].transform.GetChild(0).GetChild(2).GetComponent<Text>().text = a.heroMap[a.nameOfHero[a.num[i] - 1]].name.ToString();

            a.UnitObject[i].transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().text = a.heroMap[a.nameOfHero[a.num[i] - 1]].health.ToString();
            a.UnitObject[i].transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>().text = a.heroMap[a.nameOfHero[a.num[i] - 1]].power.ToString();

            if (a.num[i] < 14)
				a.UnitObject[i].transform.GetChild(1).GetComponent<Text>().text = "1$";
			else
				a.UnitObject[i].transform.GetChild(1).GetComponent<Text>().text = "4$";
			a.unit[a.num[i]-1]--;
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnClick();
        }
    }
}

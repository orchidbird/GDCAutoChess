using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reroll : MonoBehaviour
{
    public void OnClick()
    {
        RerollButton();
        if (Status.turn == 1)
        {
            if (playervariable.playergold >= 2)
                playervariable.playergold -= 2;
        }
        else if (Status.turn == 0)
        {
            if (playervariable.player2gold >= 2)
                playervariable.player2gold -= 2;
        }
    }

    public static void RerollButton()
    {
		playervariable a = GameObject.Find("field").GetComponent<playervariable>();

        for (int i = 0; i < 4; i++)
        {
            int level = 0;
            if (a.shop[i])
            {
                a.unit[a.num[i] - 1]++;
            }
            a.shop[i] = true;
            a.UnitObject[i].SetActive(true);
            if (playervariable.Round % 2 == 1)
                level = playervariable.playerlevel;
            else
                level = playervariable.player2level;
            switch (level)
            {
                case 1:
                    a.num[i] = Random.Range(1, 5);
                    break;
                case 2:
                    if(Random.Range(1, 101) <= 75)
                        a.num[i] = Random.Range(1, 5);
                    else
                        a.num[i] = Random.Range(5, 10);
                    break;
                case 3:
                    if (Random.Range(1, 101) <= 60)
                        a.num[i] = Random.Range(1, 5);
                    else
                        a.num[i] = Random.Range(5, 10);
                    break;
                case 4:
                    if (Random.Range(1, 101) <= 50)
                        a.num[i] = Random.Range(1, 5);
                    else if (Random.Range(1, 101) > 50 && Random.Range(1, 101) <= 85)
                        a.num[i] = Random.Range(5, 10);
                    else
                        a.num[i] = Random.Range(10, 14);
                    break;
                case 5:
                    if (Random.Range(1, 101) <= 40)
                        a.num[i] = Random.Range(1, 5);
                    else if (Random.Range(1, 101) > 40 && Random.Range(1, 101) <= 70)
                        a.num[i] = Random.Range(5, 10);
                    else
                        a.num[i] = Random.Range(10, 14);
                    break;
                case 6:
                    if(Random.Range(1, 101) <= 30)
                        a.num[i] = Random.Range(1, 5);
                    else if (Random.Range(1, 101) > 30 && Random.Range(1, 101) <= 60)
                        a.num[i] = Random.Range(5, 10);
                    else if (Random.Range(1, 101) > 60 && Random.Range(1, 101) <= 95)
                        a.num[i] = Random.Range(10, 14);
                    else
                        a.num[i] = Random.Range(15, 17);
                    break;
                case 7:
                    if (Random.Range(1, 101) <= 25)
                        a.num[i] = Random.Range(1, 5);
                    else if (Random.Range(1, 101) > 25 && Random.Range(1, 101) <= 50)
                        a.num[i] = Random.Range(5, 10);
                    else if (Random.Range(1, 101) > 50 && Random.Range(1, 101) <= 90)
                        a.num[i] = Random.Range(10, 14);
                    else
                        a.num[i] = Random.Range(15, 17);
                    break;
                case 8:
                    if (Random.Range(1, 101) <= 20)
                        a.num[i] = Random.Range(1, 5);
                    else if (Random.Range(1, 101) > 20 && Random.Range(1, 101) <= 45)
                        a.num[i] = Random.Range(5, 10);
                    else if (Random.Range(1, 101) > 45 && Random.Range(1, 101) <= 80)
                        a.num[i] = Random.Range(10, 14);
                    else
                        a.num[i] = Random.Range(15, 17);
                    break;
            }
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

            a.UnitObject[i].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = a.heroCost[a.num[i] - 1] + "G";

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

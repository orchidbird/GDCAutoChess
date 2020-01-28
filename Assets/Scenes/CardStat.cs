using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//시너지 관련 스크립트
public class CardStat : MonoBehaviour
{
    playervariable a;
    Text info;

    void Start()
    {
        a = GameObject.Find("field").GetComponent<playervariable>();
        info = transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>();
    }

    void Update()
    {
        int n = 0;

        for (int j = 0; j < 6; j++)
        {

            if (a.playerUnitType[a.heroType[j]] >= 3)
            {
                print("ACTIVATED_TYPE : " + a.heroType[j].ToString());
                transform.GetChild(0).GetChild(n).GetComponent<Image>().sprite = Resources.Load("Synergy/Image/속성_" + a.heroType[j], typeof(Sprite)) as Sprite;
                n++;
            }
            else
                transform.GetChild(0).GetChild(n).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
        }

        n = 0;

        for (int j = 0; j < 5; j++)
        {
            if (a.playerUnitClass[a.heroClass[j]] >= 3)
            {
                print("ACTIVATED_CLASS : " + a.heroClass[j].ToString());
                transform.GetChild(1).GetChild(n).GetComponent<Image>().sprite = Resources.Load("Synergy/Image/클래스_" + a.heroClass[j], typeof(Sprite)) as Sprite;
                n++;
            }
            else
                transform.GetChild(1).GetChild(n).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
        }

        SynergyInfo();
    }

    void SynergyInfo()
    {
        info.text = "물 : " + a.playerUnitType["물"] + " 불 : " + a.playerUnitType["불"] + " 나무 : " + a.playerUnitType["나무"] + " 땅 : " +
            a.playerUnitType["땅"] + "  빛 : " + a.playerUnitType["빛"] + " 어둠 : " + a.playerUnitType["어둠"] + "\n전사 : " +
            a.playerUnitClass["전사"] + " 마법사 : " + a.playerUnitClass["마법사"] + " 사수 : " + a.playerUnitClass["사수"] + " 암살자 : " + a.playerUnitClass["암살자"] + " 기사 : " + a.playerUnitClass["기사"];
    }

    void OnMouseEnter()
    {
        transform.GetChild(2).gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        transform.GetChild(2).gameObject.SetActive(false);
    }
}

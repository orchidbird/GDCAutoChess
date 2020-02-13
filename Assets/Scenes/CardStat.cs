using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//시너지 관련 스크립트
public class CardStat : MonoBehaviour
{
    void OnMouseEnter()
    {
        transform.GetChild(2).gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        transform.GetChild(2).gameObject.SetActive(false);
    }
    //playervariable a;
    //public Text synergyInfo;
    //public Text synergyInfo2;

    //public int[] typeLimit = { 2, 3, 2, 2, 2, 3 };
    //public int[] classLimit = { 2, 2, 2, 2, 3 };

    //void Start()
    //{
    //    a = GameObject.Find("field").GetComponent<playervariable>();
    //    synergyInfo = transform.GetChild(0).GetChild(4).GetChild(2).GetChild(0).gameObject.GetComponent<Text>();
    //    synergyInfo2 = transform.GetChild(1).GetChild(4).GetChild(2).GetChild(0).gameObject.GetComponent<Text>();
    //}

    //void Update()
    //{
    //    SynergyInfo();
    //}

    //void SynergyInfo()
    //{
    //    /////////////////////////////player1//////////////////////////
    //    int n = 0;

    //    for (int j = 0; j < 6; j++)
    //    {

    //        if (a.playerUnitType[a.heroType[j]] >= typeLimit[j])
    //        {
    //            transform.GetChild(0).GetChild(4).GetChild(0).GetChild(n).GetComponent<Image>().sprite = Resources.Load("Synergy/Image/속성_" + a.heroType[j], typeof(Sprite)) as Sprite;

    //            if (j == 0 && a.playerUnitType[a.heroType[0]] >= typeLimit[0] * 2)
    //            {
    //                transform.GetChild(0).GetChild(4).GetChild(0).GetChild(n).GetComponent<Image>().color = Color.red;
    //            }
    //            n++;
    //        }
    //        else
    //            transform.GetChild(0).GetChild(4).GetChild(0).GetChild(n).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;

    //    }

    //    n = 0;

    //    for (int j = 0; j < 5; j++)
    //    {
    //        if (a.playerUnitClass[a.heroClass[j]] >= classLimit[j])
    //        {
    //            transform.GetChild(0).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().sprite = Resources.Load("Synergy/Image/클래스_" + a.heroClass[j], typeof(Sprite)) as Sprite;

    //            if (j == 0 && a.playerUnitClass[a.heroClass[0]] >= classLimit[0] * 2)
    //                transform.GetChild(0).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().color = Color.red;
    //            if (j == 1 && a.playerUnitClass[a.heroClass[1]] >= classLimit[1] * 2)
    //                transform.GetChild(0).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().color = Color.red;
    //            n++;
    //        }
    //        else
    //            transform.GetChild(0).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
    //    }
    //    ///////////////////////////// player 2 //////////////////////////

    //    n = 0;

    //    for (int j = 0; j < 6; j++)
    //    {

    //        if (a.player2UnitType[a.heroType[j]] >= typeLimit[j])
    //        {
    //            transform.GetChild(1).GetChild(4).GetChild(0).GetChild(n).GetComponent<Image>().sprite = Resources.Load("Synergy/Image/속성_" + a.heroType[j], typeof(Sprite)) as Sprite;

    //            if (j == 0 && a.player2UnitType[a.heroType[0]] >= typeLimit[0] * 2)
    //                transform.GetChild(1).GetChild(4).GetChild(0).GetChild(n).GetComponent<Image>().color = Color.red;
    //            n++;
    //        }
    //        else
    //            transform.GetChild(1).GetChild(4).GetChild(0).GetChild(n).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;

    //    }

    //    n = 0;

    //    for (int j = 0; j < 5; j++)
    //    {
    //        if (a.player2UnitClass[a.heroClass[j]] >= classLimit[j])
    //        {
    //            transform.GetChild(1).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().sprite = Resources.Load("Synergy/Image/클래스_" + a.heroClass[j], typeof(Sprite)) as Sprite;

    //            if (j == 0 && a.player2UnitClass[a.heroClass[0]] >= classLimit[0] * 2)
    //                transform.GetChild(1).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().color = Color.red;
    //            if (j == 1 && a.player2UnitClass[a.heroClass[1]] >= classLimit[1] * 2)
    //                transform.GetChild(1).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().color = Color.red;
    //            n++;
    //        }
    //        else
    //            transform.GetChild(1).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
    //    }

    //    synergyInfo.text = "물 : " + a.playerUnitType["물"] + " 불 : " + a.playerUnitType["불"] + " 나무 : " + a.playerUnitType["나무"] + " 땅 : " +
    //        a.playerUnitType["땅"] + "  빛 : " + a.playerUnitType["빛"] + " 어둠 : " + a.playerUnitType["어둠"] + "\n전사 : " +
    //        a.playerUnitClass["전사"] + " 마법사 : " + a.playerUnitClass["마법사"] + " 사수 : " + a.playerUnitClass["사수"] + " 암살자 : " +
    //        a.playerUnitClass["암살자"] + " 기사 : " + a.playerUnitClass["기사"];

    //    synergyInfo2.text = "물 : " + a.player2UnitType["물"] + " 불 : " + a.player2UnitType["불"] + " 나무 : " + a.player2UnitType["나무"] + " 땅 : " +
    //        a.player2UnitType["땅"] + "  빛 : " + a.player2UnitType["빛"] + " 어둠 : " + a.player2UnitType["어둠"] + "\n전사 : " +
    //        a.player2UnitClass["전사"] + " 마법사 : " + a.player2UnitClass["마법사"] + " 사수 : " + a.player2UnitClass["사수"] + " 암살자 : " +
    //        a.player2UnitClass["암살자"] + " 기사 : " + a.player2UnitClass["기사"];
    //}


}

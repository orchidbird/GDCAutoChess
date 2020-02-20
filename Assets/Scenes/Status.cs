using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    playervariable a;
    public static int turn;

    public Text whoseTurn;
    public Text Exp1;
    public Text Exp2;
    public static Text Level1;
    public static Text Level2;
    public Text Gold1;
    public Text Gold2;
    public Text synergyInfo;
    public Text synergyInfo2;

    public int requiredexp = 2;
    public int requiredexp2 = 2;
    public int[] typeLimit = { 2, 3, 2, 2, 2, 3 };
    public int[] classLimit = { 2, 2, 2, 2, 3 };
    public string[] typeExplain = { "불속성 상대에게 2배의 피해", "나무속성 상대에게 2배의 피해", "땅속성 상대에게 2배의 피해", 
                                    "물속성 상대에게 2배의 피해", "어둠속성 상대에게 2배의 피해", "빛속성 상대에게 2배의 피해"};

    public string[][] classExplain = { new string[2] { "체력 +3", "체력 +6" }, new string[2] { "공격력 +2", "공격력 +4" }, 
                                       new string[2] { "명치 대미지 1.5배", null }, new string[2] { "매턴 명치 대미지 2", null },
                                       new string[2] { "받는 피해량 -3", "받는 피해량 -6" }};

    public int[] isActiveType1 = new int[6];
    public int[] isActiveClass1 = new int[5];
    public int[] isActiveType2 = new int[6];
    public int[] isActiveClass2 = new int[5];

    public string synergyText1;
    public string synergyText2;

    // Start is called before the first frame update
    void Start()
    {
        whoseTurn = GameObject.Find("누구턴").transform.GetChild(0).gameObject.GetComponent<Text>();
        Exp1 = GameObject.Find("Status_1").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>();
        Exp2 = GameObject.Find("Status_2").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>();
        Level1 = GameObject.Find("Status_1").transform.GetChild(2).gameObject.GetComponent<Text>();
        Level2 = GameObject.Find("Status_2").transform.GetChild(2).gameObject.GetComponent<Text>();
        Gold1 = GameObject.Find("Status_1").transform.GetChild(3).gameObject.GetComponent<Text>();
        Gold2 = GameObject.Find("Status_2").transform.GetChild(3).gameObject.GetComponent<Text>();

        a = GameObject.Find("field").GetComponent<playervariable>();
        synergyInfo = transform.GetChild(0).GetChild(4).GetChild(2).GetChild(0).gameObject.GetComponent<Text>();
        synergyInfo2 = transform.GetChild(1).GetChild(4).GetChild(2).GetChild(0).gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        WhoseTurn();
        Gold();
        Exp();
        Level();
        SynergyInfo();
    }

    void WhoseTurn()
    {
        turn = playervariable.Round % 2;
        if (turn == 1)
        {
            whoseTurn.text = "Blue Turn";
            whoseTurn.color = Color.blue;
        }
        else if (turn == 0)
        {
            whoseTurn.text = "Red Turn";
            whoseTurn.color = Color.red;
        }
    }

    void Gold()
    {
        Gold1.text = playervariable.playergold.ToString();
        Gold2.text = playervariable.player2gold.ToString();
    }

    void Exp()
    { 
        ////////////player1//////////////
        if (playervariable.playerlevel == 3)
        {
            requiredexp = 6;
        }
        if (playervariable.playerlevel == 4)
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
        ////////////player2//////////////
        if (playervariable.player2level == 3)
        {
            requiredexp2 = 6;
        }
        if (playervariable.player2level == 4)
        {
            requiredexp2 = 10;
        }
        if (playervariable.player2level == 5)
        {
            requiredexp2 = 20;
        }
        if (playervariable.player2level == 6)
        {
            requiredexp2 = 32;
        }
        if (playervariable.player2level == 7)
        {
            requiredexp2 = 50;
        }
        if (playervariable.player2level == 8)
        {
            requiredexp2 = 66;
        }
        if (turn == 1)
        {
           
            if (playervariable.playerexp >= requiredexp)
            {
                playervariable.playerlevel++;
                playervariable.playerexp = playervariable.playerexp - requiredexp;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (playervariable.playerlevel < 9 && playervariable.playergold >= 4)
                {
                    playervariable.playerexp += 4;
                    playervariable.playergold -= 4;
                }
            }
        }else if(turn == 0)
        {
            if (playervariable.player2exp >= requiredexp2)
            {
                playervariable.player2level++;
                playervariable.player2exp = playervariable.player2exp - requiredexp2;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (playervariable.player2level < 9 && playervariable.player2gold >= 4)
                {
                    playervariable.player2exp += 4;
                    playervariable.player2gold -= 4;
                }
            }
        }

        if(RealTime.time == 0)
        {
            playervariable.Round++;

           

            if (playervariable.playerexp >= requiredexp)
            {
                playervariable.playerlevel++;
                playervariable.playerexp = playervariable.playerexp - requiredexp;
            }

            if (playervariable.player2exp >= requiredexp2)
            {
                playervariable.player2level++;
                playervariable.player2exp = playervariable.player2exp - requiredexp2;
            }

            
            reroll.RerollButton();
            //첫 한턴 씩은 골드와 경험치 안오름
            if (turn == 1 && playervariable.Round > 3) //1P 차례 끝나고 2P차례 시작할 때의 일
            {
                playervariable.player2exp += 2;
                playervariable.player2gold += 7;
            }
            else if(turn == 0)
            {
                playervariable.playerexp += 2;
                playervariable.playergold += 7;
            }

            RealTime.time = RealTime.roundTime;
        }



        expinfo();
    }

    void expinfo()
    {
        if (playervariable.playerlevel < 9)
        {
            Exp1.text = playervariable.playerexp + "/" + requiredexp;
        }
        else
        {
            Exp1.text = "MAX";
        }

        if (playervariable.player2level < 9)
        {
            Exp2.text = playervariable.player2exp + "/" + requiredexp2;
        }
        else
        {
            Exp2.text = "MAX";
        }
    }

    void Level()
    {
        Level1.text = playervariable.playerlevel.ToString();
        Level2.text = playervariable.player2level.ToString();
    }

    void SynergyInfo()
    {
        ///////////////////////////////player1//////////////////////////
        int n = 0;

        for (int j = 0; j < 6; j++)
        {

            if (a.playerUnitType[a.heroType[j]] >= typeLimit[j])
            {
                transform.GetChild(0).GetChild(4).GetChild(0).GetChild(n).GetComponent<Image>().sprite = Resources.Load("Synergy/Image/속성_" + a.heroType[j], typeof(Sprite)) as Sprite;
                isActiveType1[j] = 1;
                if (j == 0 && a.playerUnitType[a.heroType[0]] >= typeLimit[0] * 2)
                {
                    transform.GetChild(0).GetChild(4).GetChild(0).GetChild(n).GetComponent<Image>().color = Color.red;
                    isActiveType1[j] = 2;
                }
                n++;
            }
            else
            {
                transform.GetChild(0).GetChild(4).GetChild(0).GetChild(n).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                isActiveType1[j] = 0;
            }

        }

        n = 0;

        for (int j = 0; j < 5; j++)
        {
            if (a.playerUnitClass[a.heroClass[j]] >= classLimit[j])
            {
                transform.GetChild(0).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().sprite = Resources.Load("Synergy/Image/클래스_" + a.heroClass[j], typeof(Sprite)) as Sprite;
                isActiveClass1[j] = 1;
                if (j == 0 && a.playerUnitClass[a.heroClass[0]] >= classLimit[0] * 2)
                {
                    transform.GetChild(0).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().color = Color.red;
                    isActiveClass1[j] = 2;
                }
                if (j == 1 && a.playerUnitClass[a.heroClass[1]] >= classLimit[1] * 2)
                {
                    transform.GetChild(0).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().color = Color.red;
                    isActiveClass1[j] = 2;
                }
                n++;
            }
            else
            {
                transform.GetChild(0).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                isActiveClass1[j] = 0;
            }
        }

        /////////////////////////////// player 2 //////////////////////////

        n = 0;

        for (int j = 0; j < 6; j++)
        {

            if (a.player2UnitType[a.heroType[j]] >= typeLimit[j])
            {
                transform.GetChild(1).GetChild(4).GetChild(0).GetChild(n).GetComponent<Image>().sprite = Resources.Load("Synergy/Image/속성_" + a.heroType[j], typeof(Sprite)) as Sprite;
                isActiveType2[j] = 1;
                if (j == 0 && a.player2UnitType[a.heroType[0]] >= typeLimit[0] * 2)
                {
                    transform.GetChild(1).GetChild(4).GetChild(0).GetChild(n).GetComponent<Image>().color = Color.red;
                    isActiveType2[j] = 2;
                }
                n++;
            }
            else
            { 
                transform.GetChild(1).GetChild(4).GetChild(0).GetChild(n).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                isActiveType2[j] = 0;
            }
               
        }

        n = 0;

        for (int j = 0; j < 5; j++)
        {
            if (a.player2UnitClass[a.heroClass[j]] >= classLimit[j])
            {
                transform.GetChild(1).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().sprite = Resources.Load("Synergy/Image/클래스_" + a.heroClass[j], typeof(Sprite)) as Sprite;
                isActiveClass2[j] = 1;
                if (j == 0 && a.player2UnitClass[a.heroClass[0]] >= classLimit[0] * 2)
                {
                    transform.GetChild(1).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().color = Color.red;
                    isActiveClass2[j] = 2;
                }
                if (j == 1 && a.player2UnitClass[a.heroClass[1]] >= classLimit[1] * 2)
                {
                    transform.GetChild(1).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().color = Color.red;
                    isActiveClass2[j] = 2;
                }
                n++;
            }
            else
            {
                transform.GetChild(1).GetChild(4).GetChild(1).GetChild(n).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                isActiveClass2[j] = 0;
            }
        }



        synergyText1 = null;
        synergyText2 = null;

        for (int i = 0; i < isActiveType1.Length; i++)
        {
            if (isActiveType1[i] !=0)
                synergyText1 += a.heroType[i] + isActiveType1[i] + "단계 : " + typeExplain[i];
        }
        synergyText1 += "\n";
        for (int i = 0; i < isActiveClass1.Length; i++)
        {
            if (isActiveClass1[i] != 0)
                synergyText1 += a.heroClass[i] + isActiveClass1[i] + "단계 : " + classExplain[i][isActiveClass1[i]-1];
        }

        for (int i = 0; i < isActiveType2.Length; i++)
        {
            if (isActiveType2[i] != 0)
                synergyText2 += a.heroType[i] + isActiveType2[i] + "단계 : " + typeExplain[i];
        }
        synergyText2 += "\n";
        for (int i = 0; i < isActiveClass2.Length; i++)
        {
            if (isActiveClass1[i] != 0)
                synergyText2 += a.heroClass[i] + isActiveClass2[i] + "단계 : " + classExplain[i][isActiveClass1[i]-1];
        }
        synergyInfo.text = synergyText1;
        synergyInfo2.text = synergyText2;




        //synergyInfo.text = "물 : " + a.playerUnitType["물"] + " 불 : " + a.playerUnitType["불"] + " 나무 : " + a.playerUnitType["나무"] + " 땅 : " +
        //    a.playerUnitType["땅"] + "  빛 : " + a.playerUnitType["빛"] + " 어둠 : " + a.playerUnitType["어둠"] + "\n전사 : " +
        //    a.playerUnitClass["전사"] + " 마법사 : " + a.playerUnitClass["마법사"] + " 사수 : " + a.playerUnitClass["사수"] + " 암살자 : " +
        //    a.playerUnitClass["암살자"] + " 기사 : " + a.playerUnitClass["기사"];

        //synergyInfo2.text = "물 : " + a.player2UnitType["물"] + " 불 : " + a.player2UnitType["불"] + " 나무 : " + a.player2UnitType["나무"] + " 땅 : " +
        //    a.player2UnitType["땅"] + "  빛 : " + a.player2UnitType["빛"] + " 어둠 : " + a.player2UnitType["어둠"] + "\n전사 : " +
        //    a.player2UnitClass["전사"] + " 마법사 : " + a.player2UnitClass["마법사"] + " 사수 : " + a.player2UnitClass["사수"] + " 암살자 : " +
        //    a.player2UnitClass["암살자"] + " 기사 : " + a.player2UnitClass["기사"];
    }
}

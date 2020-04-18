using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Synergy
{
    public string name;
    public string heroType;
    public string heroClass;
    public int health;
    public int power;

    public Synergy(string _name, string _heroType, string _heroClass, int _health, int _power)
    {
        this.name = _name;
        this.heroType = _heroType;
        this.heroClass = _heroClass;
        this.health = _health;
        this.power = _power;

    }

    public void Show()
    {
        Debug.Log(this.name);
        Debug.Log(this.heroType);
        Debug.Log(this.heroClass);
        Debug.Log(this.health);
        Debug.Log(this.power);
    }
}


public class playervariable : MonoBehaviour
{
	public GameObject[] UnitObject = new GameObject[4];

	public bool[] shop = new bool[4];				//상점 카드 on/off

	public int[] num = new int[4];                  //유닛 종류
	public int[] unit = new int[16];                //상점에 남아있는 유닛 풀

    public static int Round = 1;

    public string[] nameOfHero = { "라티스", "에렌", "그레네브", "유진", "영", "달케니르",
                "루베리카", "지수", "세피아", "데우스", "아르카디아", "칼드리치", "카샤스티", "레이나", "루키어스", "노엘"};
    public string[] heroType = { "물", "불", "나무", "땅", "빛", "어둠" };
    public string[] heroClass = { "전사", "마법사",  "사수", "암살자", "기사" };
    public int[] heroHealth = { 7, 2, 2, 3, 5, 5, 5, 4, 6, 6, 9, 3, 8, 9, 12, 11 };
    public int[] heroPower = { 0, 2, 2, 1, 3, 3, 1, 4, 2, 6, 3, 9, 4, 7, 4, 5 };
    public int[] heroCost = { 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4 };

    ///////////////////////////플레이어 1/////////////////////////////////

    public int[] warehouse = new int[8];            //창고에 있는 유닛의 종류
    public int[] warehouselevel = new int[8];       //창고에 있는 카드의 성급 저장

    //플레이어가 가지고 있는 n성 카드의 개수
    public int[] playerunitlevel1 = new int[16];
	public int[] playerunitlevel2 = new int[16];
	public int[] playerunitlevel3 = new int[16];

    //플레이어 능력치
    public static int playerlevel = 1;
    public static int playerhealth = 100;
    public static int playerexp = 0;
    public static int playergold;

    //보드 위의 유닛 시너지
    public Dictionary<string, int> playerUnitType = new Dictionary<string, int>();
    public Dictionary<string, int> playerUnitClass = new Dictionary<string, int>();

    //보드에 있는 유닛의 종류
    public int[] board = new int[12];
    public int[] boardLevel = new int[12];
    public int howManyOnBoard;

    //카드 속성
    public Dictionary<string, Synergy> heroMap = new Dictionary<string, Synergy>();
    
    public static bool[] whoIsOnBoard = new bool[16];  //보드 위에 있는 카드 탐색
    public string textOfUISynergy;

    ///////////////////////////플레이어 2/////////////////////////////////

    public int[] warehouse2 = new int[8];            //창고에 있는 유닛의 종류
    public int[] warehouse2level = new int[8];       //창고에 있는 카드의 성급 저장

    //플레이어가 가지고 있는 n성 카드의 개수
    public int[] player2unitlevel1 = new int[16];
    public int[] player2unitlevel2 = new int[16];
    public int[] player2unitlevel3 = new int[16];

    //플레이어 능력치
    public static int player2level = 1;
    public static int player2health = 100;
    public static int player2exp = 0;
    public static int player2gold;

    //보드 위의 유닛 시너지
    public Dictionary<string, int> player2UnitType = new Dictionary<string, int>();
    public Dictionary<string, int> player2UnitClass = new Dictionary<string, int>();

    //보드에 있는 유닛의 종류
    public int[] board2 = new int[12];
    public int[] board2Level = new int[12];
    public int howManyOnBoard2;

    //카드 속성
    public Dictionary<string, Synergy> heroMap2 = new Dictionary<string, Synergy>();

    public static bool[] whoIsOnBoard2 = new bool[16];  //보드 위에 있는 카드 탐색
    public string textOfUISynergy2;

    void Awake()
	{
        HeroStat();

		for (int i = 0; i < 4; i++)
		{
			shop[i] = true;	
			UnitObject[i] = GameObject.Find("상점" + (i + 1).ToString());
			UnitObject[i].SetActive(true);
		}
		for (int i = 0; i < 8; i++)
		{
			warehouse[i] = -1;
			warehouselevel[i] = 0;

            warehouse2[i] = -1;
            warehouse2level[i] = 0;
        }
		for (int i = 0; i < 16; i++)
		{
			unit[i] = 16;
			playerunitlevel1[i] = 0;
			playerunitlevel2[i] = 0;
			playerunitlevel3[i] = 0;

            player2unitlevel1[i] = 0;
            player2unitlevel2[i] = 0;
            player2unitlevel3[i] = 0;
        }

        //첫 상점
        for (int i = 0; i < 4; i++)
		{
			//range는 카드 종류의 수
			num[i] = Random.Range(1, 5);
			if (unit[num[i] - 1] != 0)
			{
				unit[num[i] - 1]--;
			}
			else
			{
				i--;
			}
		}
        //print(num[0] +" "+ num[1] + " " + num[2] + " " + num[3]);

		playergold = 100;
        player2gold = 100;
    }

    void Update()
    {
        TypeAndClass();
        LevelLimit();
    }
    void LevelLimit()
    {
        
    }
    void TypeAndClass()
    {
        //초기화
        for (int i = 0; i < 6; i++)
        {
            playerUnitType[heroType[i].ToString()] = 0;
            player2UnitType[heroType[i].ToString()] = 0;
        }
        for (int i = 0; i < 5; i++)
        {
            playerUnitClass[heroClass[i]] = 0;
            player2UnitClass[heroClass[i]] = 0;
        }
        for (int i = 0; i < 12; i++)
        {
            whoIsOnBoard[i] = false;
            whoIsOnBoard2[i] = false;
        }

        //조사
        for (int i = 0; i < 12; i++)
        {
            if (board[i] > 0)
            {
                if (whoIsOnBoard[board[i]-1] == false)
                    whoIsOnBoard[board[i]-1] = true;
            }

            if (board2[i] > 0)
            {
                if (whoIsOnBoard2[board2[i] - 1] == false)
                    whoIsOnBoard2[board2[i] - 1] = true;
            }
        }

        //시너지 추가
        for (int i = 0; i < whoIsOnBoard.Length; i++)
        {
            if (whoIsOnBoard[i] == true)
            {
                playerUnitType[heroMap[nameOfHero[i].ToString()].heroType]++;
                playerUnitClass[heroMap[nameOfHero[i].ToString()].heroClass]++;
            }

            if (whoIsOnBoard2[i] == true)
            {
                player2UnitType[heroMap2[nameOfHero[i].ToString()].heroType]++;
                player2UnitClass[heroMap2[nameOfHero[i].ToString()].heroClass]++;
            }
        }
    }

    public void HeroStat()
    {
        heroMap.Add(nameOfHero[0], new Synergy(nameOfHero[0], heroType[3], heroClass[0], heroHealth[0], heroPower[0]));
        heroMap.Add(nameOfHero[1], new Synergy(nameOfHero[1], heroType[5], heroClass[1], heroHealth[1], heroPower[1]));
        heroMap.Add(nameOfHero[2], new Synergy(nameOfHero[2], heroType[1], heroClass[2], heroHealth[2], heroPower[2]));
        heroMap.Add(nameOfHero[3], new Synergy(nameOfHero[3], heroType[0], heroClass[4], heroHealth[3], heroPower[3]));
        heroMap.Add(nameOfHero[4], new Synergy(nameOfHero[4], heroType[0], heroClass[3], heroHealth[4], heroPower[4]));
        heroMap.Add(nameOfHero[5], new Synergy(nameOfHero[5], heroType[5], heroClass[4], heroHealth[5], heroPower[5]));
        heroMap.Add(nameOfHero[6], new Synergy(nameOfHero[6], heroType[2], heroClass[1], heroHealth[6], heroPower[6]));
        heroMap.Add(nameOfHero[7], new Synergy(nameOfHero[7], heroType[4], heroClass[1], heroHealth[7], heroPower[7]));
        heroMap.Add(nameOfHero[8], new Synergy(nameOfHero[8], heroType[5], heroClass[0], heroHealth[8], heroPower[8]));
        heroMap.Add(nameOfHero[9], new Synergy(nameOfHero[9], heroType[3], heroClass[1], heroHealth[9], heroPower[9]));
        heroMap.Add(nameOfHero[10], new Synergy(nameOfHero[10], heroType[2], heroClass[0], heroHealth[10], heroPower[10]));
        heroMap.Add(nameOfHero[11], new Synergy(nameOfHero[11], heroType[1], heroClass[3], heroHealth[11], heroPower[11]));
        heroMap.Add(nameOfHero[12], new Synergy(nameOfHero[12], heroType[0], heroClass[2], heroHealth[12], heroPower[12]));
        heroMap.Add(nameOfHero[13], new Synergy(nameOfHero[13], heroType[1], heroClass[1], heroHealth[13], heroPower[13]));
        heroMap.Add(nameOfHero[14], new Synergy(nameOfHero[14], heroType[0], heroClass[0], heroHealth[14], heroPower[14]));
        heroMap.Add(nameOfHero[15], new Synergy(nameOfHero[15], heroType[4], heroClass[4], heroHealth[15], heroPower[15]));

        heroMap2.Add(nameOfHero[0], new Synergy(nameOfHero[0], heroType[3], heroClass[0], heroHealth[0], heroPower[0]));
        heroMap2.Add(nameOfHero[1], new Synergy(nameOfHero[1], heroType[5], heroClass[1], heroHealth[1], heroPower[1]));
        heroMap2.Add(nameOfHero[2], new Synergy(nameOfHero[2], heroType[1], heroClass[2], heroHealth[2], heroPower[2]));
        heroMap2.Add(nameOfHero[3], new Synergy(nameOfHero[3], heroType[0], heroClass[4], heroHealth[3], heroPower[3]));
        heroMap2.Add(nameOfHero[4], new Synergy(nameOfHero[4], heroType[0], heroClass[3], heroHealth[4], heroPower[4]));
        heroMap2.Add(nameOfHero[5], new Synergy(nameOfHero[5], heroType[5], heroClass[4], heroHealth[5], heroPower[5]));
        heroMap2.Add(nameOfHero[6], new Synergy(nameOfHero[6], heroType[2], heroClass[1], heroHealth[6], heroPower[6]));
        heroMap2.Add(nameOfHero[7], new Synergy(nameOfHero[7], heroType[4], heroClass[1], heroHealth[7], heroPower[7]));
        heroMap2.Add(nameOfHero[8], new Synergy(nameOfHero[8], heroType[5], heroClass[0], heroHealth[8], heroPower[8]));
        heroMap2.Add(nameOfHero[9], new Synergy(nameOfHero[9], heroType[3], heroClass[1], heroHealth[9], heroPower[9]));
        heroMap2.Add(nameOfHero[10], new Synergy(nameOfHero[10], heroType[2], heroClass[0], heroHealth[10], heroPower[10]));
        heroMap2.Add(nameOfHero[11], new Synergy(nameOfHero[11], heroType[1], heroClass[3], heroHealth[11], heroPower[11]));
        heroMap2.Add(nameOfHero[12], new Synergy(nameOfHero[12], heroType[0], heroClass[2], heroHealth[12], heroPower[12]));
        heroMap2.Add(nameOfHero[13], new Synergy(nameOfHero[13], heroType[1], heroClass[1], heroHealth[13], heroPower[13]));
        heroMap2.Add(nameOfHero[14], new Synergy(nameOfHero[14], heroType[0], heroClass[0], heroHealth[14], heroPower[14]));
        heroMap2.Add(nameOfHero[15], new Synergy(nameOfHero[15], heroType[4], heroClass[4], heroHealth[15], heroPower[15]));

        for (int i = 0; i < 6; i++)
        {
            playerUnitType.Add(heroType[i], 0);
            player2UnitType.Add(heroType[i], 0);
        }

        for (int i = 0; i < 5; i++)
        {
            playerUnitClass.Add(heroClass[i], 0);
            player2UnitClass.Add(heroClass[i], 0);
        }
    }
}

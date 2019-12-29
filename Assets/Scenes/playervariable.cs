using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Synergy
{
    public string name;
    public string heroType;
    public string heroClass;

    public Synergy(string _name, string _heroType, string _heroClass)
    {
        this.name = _name;
        this.heroType = _heroType;
        this.heroClass = _heroClass;
    }

    public void Show()
    {
        Debug.Log(this.name);
        Debug.Log(this.heroType);
        Debug.Log(this.heroClass);
    }
}

public class playervariable : MonoBehaviour
{
	public GameObject[] UnitObject = new GameObject[4];

	public bool[] shop = new bool[4];				//상점 카드 on/off

	public int[] warehouse = new int[8];            //창고에 있는 유닛의 종류
	public int[] warehouselevel = new int[8];       //창고에 있는 카드의 성급 저장

	public int[] num = new int[4];                  //유닛 종류
	public int[] unit = new int[16];                //상점에 남아있는 유닛 풀

	//플레이어가 가지고 있는 n성 카드의 개수
	public int[] playerunitlevel1 = new int[16];
	public int[] playerunitlevel2 = new int[16];
	public int[] playerunitlevel3 = new int[16];

    public Dictionary<string, int> playerUnitType = new Dictionary<string, int>();
    public Dictionary<string, int> playerUnitClass = new Dictionary<string, int>();

	public static int playergold;								//골드

    //보드에 있는 유닛의 종류
    public int[] board = new int[12];
    public int[] boardLevel = new int[12];

    //카드 속성
    public Dictionary<string, Synergy> heroMap = new Dictionary<string, Synergy>();
    public string[] nameOfHero = { "루키어스", "카샤스티", "영", "유진", "레이나", "그레네브", "칼드리치", "아르카디아", "루베리카", "라티스", "데우스", "지수", "노엘", "세피아", "에렌", "달케니르" };
    public string[] heroType = { "물", "불", "나무", "땅", "빛", "어둠" };
    public string[] heroClass = { "전사", "마법사", "사수", "암살자", "기사" };

    public string textOfUISynergy;

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
		}
		for (int i = 0; i < 16; i++)
		{
			unit[i] = 16;
			playerunitlevel1[i] = 0;
			playerunitlevel2[i] = 0;
			playerunitlevel3[i] = 0;
		}
		//랜덤 숫자 받아오기
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
		playergold = 100;        
    }

    void Update()
    {
        TypeAndClass();
    }

    void TypeAndClass()
    {
        for (int i = 0; i < 6; i++)
        {
            playerUnitType[heroType[i].ToString()] = 0;
        }

        for (int i = 0; i < 5; i++)
        {
            playerUnitClass[heroClass[i]] = 0;
        }

        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] > 0)
            {
                playerUnitType[heroMap[nameOfHero[board[i]-1].ToString()].heroType]++;
                playerUnitClass[heroMap[nameOfHero[board[i]-1].ToString()].heroClass]++;
            }
        }
    }

    void HeroStat()
    {
        heroMap.Add(nameOfHero[0], new Synergy(nameOfHero[0], heroType[0], heroClass[0]));
        heroMap.Add(nameOfHero[1], new Synergy(nameOfHero[1], heroType[0], heroClass[2]));
        heroMap.Add(nameOfHero[2], new Synergy(nameOfHero[2], heroType[0], heroClass[3]));
        heroMap.Add(nameOfHero[3], new Synergy(nameOfHero[3], heroType[0], heroClass[4]));
        heroMap.Add(nameOfHero[4], new Synergy(nameOfHero[4], heroType[1], heroClass[1]));
        heroMap.Add(nameOfHero[5], new Synergy(nameOfHero[5], heroType[1], heroClass[2]));
        heroMap.Add(nameOfHero[6], new Synergy(nameOfHero[6], heroType[1], heroClass[3]));
        heroMap.Add(nameOfHero[7], new Synergy(nameOfHero[7], heroType[2], heroClass[0]));
        heroMap.Add(nameOfHero[8], new Synergy(nameOfHero[8], heroType[2], heroClass[1]));
        heroMap.Add(nameOfHero[9], new Synergy(nameOfHero[9], heroType[3], heroClass[0]));
        heroMap.Add(nameOfHero[10], new Synergy(nameOfHero[10], heroType[3], heroClass[1]));
        heroMap.Add(nameOfHero[11], new Synergy(nameOfHero[11], heroType[4], heroClass[1]));
        heroMap.Add(nameOfHero[12], new Synergy(nameOfHero[12], heroType[4], heroClass[4]));
        heroMap.Add(nameOfHero[13], new Synergy(nameOfHero[13], heroType[5], heroClass[0]));
        heroMap.Add(nameOfHero[14], new Synergy(nameOfHero[14], heroType[5], heroClass[1]));
        heroMap.Add(nameOfHero[15], new Synergy(nameOfHero[15], heroType[5], heroClass[4]));

        for (int i = 0; i < 6; i++)
        {
            playerUnitType.Add(heroType[i], 0);
        }

        for (int i = 0; i < 5; i++)
        {
            playerUnitClass.Add(heroClass[i], 0);
        }
    }
}

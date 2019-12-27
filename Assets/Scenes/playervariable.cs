using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public static int playergold;								//골드

    //보드에 있는 유닛의 종류
    public int[] board = new int[12];
    public int[] boardLevel = new int[12];

    //카드 속성
    public Dictionary<string, Synergy> heroMap;
    public string[] name = new string[16];


    void Awake()
	{
        CardStat();

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

    void CardStat()
    {
        heroMap = new Dictionary<string, Synergy>();
        string[] heroType = { "물", "불", "나무", "땅", "빛", "어둠" };
        string[] heroClass = { "전사", "마법사", "사수", "암살자", "기사" };

        name[0] = "루키어스";
        name[1] = "카샤스티";
        name[2] = "영";
        name[3] = "유진";
        name[4] = "레이나";
        name[5] = "그레네브";
        name[6] = "칼드리치";
        name[7] = "아르카디아";
        name[8] = "루베리카";
        name[9] = "라티스";
        name[10] = "데우스";
        name[11] = "지수";
        name[12] = "노엘";
        name[13] = "세피아";
        name[14] = "에렌";
        name[15] = "달케니르";

        heroMap.Add(name[0], new Synergy(name[0], heroType[0], heroClass[0]));
        heroMap.Add(name[1], new Synergy(name[1], heroType[0], heroClass[2]));
        heroMap.Add(name[2], new Synergy(name[2], heroType[0], heroClass[3]));
        heroMap.Add(name[3], new Synergy(name[3], heroType[0], heroClass[4]));
        heroMap.Add(name[4], new Synergy(name[4], heroType[1], heroClass[1]));
        heroMap.Add(name[5], new Synergy(name[5], heroType[1], heroClass[2]));
        heroMap.Add(name[6], new Synergy(name[6], heroType[1], heroClass[3]));
        heroMap.Add(name[7], new Synergy(name[7], heroType[2], heroClass[0]));
        heroMap.Add(name[8], new Synergy(name[8], heroType[2], heroClass[1]));
        heroMap.Add(name[9], new Synergy(name[9], heroType[3], heroClass[0]));
        heroMap.Add(name[10], new Synergy(name[10], heroType[3], heroClass[1]));
        heroMap.Add(name[11], new Synergy(name[11], heroType[4], heroClass[1]));
        heroMap.Add(name[12], new Synergy(name[12], heroType[4], heroClass[4]));
        heroMap.Add(name[13], new Synergy(name[13], heroType[5], heroClass[0]));
        heroMap.Add(name[14], new Synergy(name[14], heroType[5], heroClass[1]));
        heroMap.Add(name[15], new Synergy(name[15], heroType[5], heroClass[4]));


    }
}

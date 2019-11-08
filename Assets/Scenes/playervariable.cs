using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	void Awake()
	{
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
}

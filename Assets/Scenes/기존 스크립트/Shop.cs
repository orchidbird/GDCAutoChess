using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    //public GameObject ShopObject;   
    //public GameObject[] UnitObject = new GameObject[4];
    //public GameObject reroll;

    //public bool[] shop = new bool[4];               //상점 배경. OnOff 가능한거 같은데, 이제는 그럴 필요 없음. --> 추후 변경

    //public int[] warehouse = new int[8];            //창고에 있는 유닛의 종류
    //public int[] warehouselevel = new int[8];       //창고에 있는 카드의 성급 저장

    //public int[] num = new int[4];                  //유닛 종류
    //public int[] unit = new int[16];                //상점에 남아있는 유닛 풀

    ////플레이어가 가지고 있는 n성 카드의 개수
    //public int[] playerunitlevel1 = new int[16];    
    //public int[] playerunitlevel2 = new int[16];
    //public int[] playerunitlevel3 = new int[16];

    ////초기화
    //void Awake()
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        shop[i] = true;
    //        UnitObject[i] = GameObject.Find("상점" + (i + 1).ToString());
    //        UnitObject[i].SetActive(false);
    //    }
    //    ShopObject.SetActive(true);
    //    reroll.SetActive(true);
    //    for (int i = 0; i < 8; i++)
    //    {
    //        warehouse[i] = -1;
    //        warehouselevel[i] = 0;
    //    }
    //    for (int i = 0; i < 16; i++)
    //    {
    //        unit[i] = 16;
    //        playerunitlevel1[i] = 0;
    //        playerunitlevel2[i] = 0;
    //        playerunitlevel3[i] = 0;
    //    }
    //    for(int i = 0; i < 4; i++)
    //    {
    //        num[i] = Random.Range(1, 5);
    //        if (unit[num[i]-1] != 0)
    //        {
    //            unit[num[i]-1]--;
    //        }
    //        else
    //        {
    //            i--;
    //        }
    //    }
    //}
    ////상점 활성화 여부 바꾸기
    //public void ShopSetActive()
    //{
    //    if (!ShopObject.activeSelf)
    //    {
    //        for(int i = 0; i < 4; i++)
    //        {
    //            UnitObject[i].SetActive(shop[i]);
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < 4; i++)
    //        {
    //            UnitObject[i].SetActive(false);
    //        }
    //    }
    //    ShopObject.SetActive(!ShopObject.activeSelf);
    //    reroll.SetActive(!reroll.activeSelf);
    //}
}
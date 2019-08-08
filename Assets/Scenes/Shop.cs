using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ShopObject;
    public GameObject[] UnitObject = new GameObject[4];
    public GameObject reroll;
    public bool[] shop = new bool[4];
    public int[] warehouse = new int[8];
    public int[] warehouselevel = new int[8];
    public int[] num = new int[4];
    public int[] unit = new int[16];
    public int[] playerunitlevel1 = new int[16];
    public int[] playerunitlevel2 = new int[16];
    public int[] playerunitlevel3 = new int[16];

    void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            shop[i] = true;
            UnitObject[i] = GameObject.Find("상점" + (i + 1).ToString());
            UnitObject[i].SetActive(false);
        }
        ShopObject.SetActive(false);
        reroll.SetActive(false);
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
        for(int i = 0; i < 4; i++)
        {
            num[i] = Random.Range(1, 5);
            if (unit[num[i]-1] != 0)
            {
                unit[num[i]-1]--;
            }
            else
            {
                i--;
            }
        }
    }
    // Start is called before the first frame update
    public void ShopSetActive()
    {
        if (!ShopObject.active)
        {
            for(int i = 0; i < 4; i++)
            {
                UnitObject[i].SetActive(shop[i]);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                UnitObject[i].SetActive(false);
            }
        }
        ShopObject.SetActive(!ShopObject.active);
        reroll.SetActive(!reroll.active);
    }
}
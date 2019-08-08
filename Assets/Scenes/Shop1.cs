using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shop1 : MonoBehaviour
{
    public GameObject ShopButton;
    public GameObject[] Warehouse = new GameObject[8];
    void Start()
    {
        Shop a = GameObject.Find("상점버튼").GetComponent<Shop>();
        for (int i = 0; i < 4; i++)
        {
            if (ShopButton.name == "상점" + (i+1).ToString())
            {
                gameObject.GetComponent<Image>().sprite = Resources.Load("Unit" + a.num[i].ToString(), typeof(Sprite)) as Sprite;
                break;
            }
        }
        for (int i = 0; i < 8; i++)
        {
            Warehouse[i] = GameObject.Find("유닛창고" + (i + 1).ToString());
        }
    }
    public void chosen()
    {
        ShopButton.SetActive(!ShopButton.active);
        Shop a = GameObject.Find("상점버튼").GetComponent<Shop>();
        int i;
        for (i = 0; i < 4; i++)
        {
            if (ShopButton.name == "상점" + (i + 1).ToString())
            {
                a.shop[i] = false;
                break;
            }
        }
        for(int j = 0; j < 8; j++)
        {
            if(a.warehouse[j] == -1)
            {
                Warehouse[j].GetComponent<Image>().sprite = Resources.Load("Unit" + a.num[i].ToString(), typeof(Sprite)) as Sprite;
                a.warehouse[j] = a.num[i];
                a.warehouselevel[j] = 1;
                a.playerunitlevel1[a.num[i]-1]++;
                if(a.playerunitlevel1[a.num[i]-1] == 3)
                {
                    a.playerunitlevel1[a.num[i]-1] = 0;
                    a.playerunitlevel2[a.num[i]-1]++;
                    int b = 0;
                    for(int k = 0; k < 8; k++)
                    {
                        if(a.warehouse[k] == a.num[i] && a.warehouselevel[k] == 1)
                        {
                            a.warehouse[k] = -1;
                            a.warehouselevel[k] = 0;
                            Warehouse[k].GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                            if (b == 0)
                            {
                                a.warehouse[k] = a.num[i];
                                a.warehouselevel[k] = 2;
                                Warehouse[k].GetComponent<Image>().sprite = Resources.Load("Unit" + a.num[i] + "-2".ToString(), typeof(Sprite)) as Sprite;
                                b++;
                            }
                        }
                    }
                }
                if (a.playerunitlevel2[a.num[i] - 1] == 3)
                {
                    a.playerunitlevel2[a.num[i] - 1] = 0;
                    a.playerunitlevel3[a.num[i] - 1]++;
                    int b = 0;
                    for (int k = 0; k < 8; k++)
                    {
                        if (a.warehouse[k] == a.num[i] && a.warehouselevel[k] == 2)
                        {
                            a.warehouse[k] = -1;
                            a.warehouselevel[k] = 0;
                            Warehouse[k].GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                            if (b == 0)
                            {
                                a.warehouse[k] = a.num[i];
                                a.warehouselevel[k] = 3;
                                Warehouse[k].GetComponent<Image>().sprite = Resources.Load("Unit" + a.num[i] + "-3".ToString(), typeof(Sprite)) as Sprite;
                                b++;
                            }
                        }
                    }
                }
                break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop1 : MonoBehaviour
{
    public GameObject ShopButton;
    public GameObject[] Warehouse = new GameObject[8];


	void Start()
    {
		playervariable a = GameObject.Find("field").GetComponent<playervariable>();      //상점버튼의 shop 스크립트 가져오기

        //상점칸에 카드 띄우기
        for (int i = 0; i < 4; i++)
        {
            if (ShopButton.name == "상점" + (i+1).ToString())
            {
                gameObject.GetComponent<Image>().sprite = Resources.Load("카드배경_1", typeof(Sprite)) as Sprite; //카드 이미지 보여주기
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load(a.num[i].ToString(), typeof(Sprite)) as Sprite;
                gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("카드템플릿_1", typeof(Sprite)) as Sprite;
                if (a.num[i] < 14)
					gameObject.transform.GetChild(1).GetComponent<Text>().text = "1$";
				else
					gameObject.transform.GetChild(1).GetComponent<Text>().text = "4$";
				break;
            }
        }
        //창고 오브젝트 가져오기
        for (int i = 0; i < 8; i++)
            Warehouse[i] = GameObject.Find("유닛창고" + (i + 1).ToString());
    }

    //상점에서 카드 고르기
    public void chosen()
    {
		playervariable a = GameObject.Find("field").GetComponent<playervariable>();      //변수 호출

		int i, b;
        for (i = 0; i < 4; i++)
        {
            //골라진 칸의 i 값을 기억하고 있는다
            if (ShopButton.name == "상점" + (i + 1).ToString())
            {
                break;
            }
        }
		//warehouse에 뽑은 카드 집어넣기
		for (int j = 0; j < 8; j++)
		{
			if (a.warehouse[j] == -1)
			{
				if (a.num[i] < 14)
					playervariable.playergold--;
				else
					playervariable.playergold -= 4;
				//Resources에서 Unit~ sprite로 불러와서 warehouse에 집어넣기
				Warehouse[j].GetComponent<Image>().sprite = Resources.Load("카드배경_1", typeof(Sprite)) as Sprite; //카드 이미지 보여주기
                Warehouse[j].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load(a.num[i].ToString(), typeof(Sprite)) as Sprite;
                Warehouse[j].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("카드템플릿_1", typeof(Sprite)) as Sprite;

                //warehouse의 j번째 칸에 불러온 카드 집어넣는다
                a.warehouse[j] = a.num[i];

				//상점에서 뽑아온 카드의 성급을 받아온다
				a.warehouselevel[j] = 1;

				//이제껏 뽑힌 카드개수
				a.playerunitlevel1[a.num[i] - 1]++;

				// 2성 카드 만들기
				if (a.playerunitlevel1[a.num[i] - 1] == 3)         //뽑힌 카드개수가 3개가 되면
				{
					a.playerunitlevel1[a.num[i] - 1] = 0;         //1성 카드개수 초기화
					a.playerunitlevel2[a.num[i] - 1]++;           //2성 카드개수 추가
					b = 0;
					for (int k = 0; k < 8; k++)
					{
						if (a.warehouse[k] == a.num[i] && a.warehouselevel[k] == 1)      //2성 될 카드의 1성카드들 지워주기
						{
							a.warehouse[k] = -1;        //k번째 창고에 -1 집어넣기(창고 비우기)
							a.warehouselevel[k] = 0;        //창고에 있는 카드 레벨 0으로 초기화
                            Warehouse[k].GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                            Warehouse[k].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                            Warehouse[k].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite; ;//창고가 비었을 때 다시 창고 이미지 보여주기

							//1성 카드들 중 첫번째 카드만 2성 이미지로 보여주기
							if (b == 0)
							{
								a.warehouse[k] = a.num[i];      // ??
								a.warehouselevel[k] = 2;        //창고에 있는 카드레벨 2로 만들기
                                Warehouse[k].GetComponent<Image>().sprite = Resources.Load("카드배경_2", typeof(Sprite)) as Sprite; //카드 이미지 보여주기
                                Warehouse[k].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load(a.num[i].ToString(), typeof(Sprite)) as Sprite;
                                Warehouse[k].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("카드템플릿_2", typeof(Sprite)) as Sprite;
                                b++;        // b=0일때만 작업 수행하기 때문에, 그 뒤로 발견되는 1성 카드들은 그냥 사라지기만 할 것이다.
							}
						}
					}
				}

				//2성 카드 3개면 3성 하나 만들기 (2성카드 하나 만드는 거랑 동일)
				if (a.playerunitlevel2[a.num[i] - 1] == 3)
				{
					a.playerunitlevel2[a.num[i] - 1] = 0;
					a.playerunitlevel3[a.num[i] - 1]++;
					b = 0;
					for (int k = 0; k < 8; k++)
					{
						if (a.warehouse[k] == a.num[i] && a.warehouselevel[k] == 2)
						{
							a.warehouse[k] = -1;
							a.warehouselevel[k] = 0;
							Warehouse[k].GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
							Warehouse[k].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                            Warehouse[k].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                            if (b == 0)
							{
								a.warehouse[k] = a.num[i];
								a.warehouselevel[k] = 3;
                                Warehouse[k].GetComponent<Image>().sprite = Resources.Load("카드배경_3", typeof(Sprite)) as Sprite; //카드 이미지 보여주기
                                Warehouse[k].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load(a.num[i].ToString(), typeof(Sprite)) as Sprite;
                                Warehouse[k].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("카드템플릿_3", typeof(Sprite)) as Sprite;
                                b++;
							}
						}
					}
				}
				ShopButton.SetActive(!ShopButton.active);  //ShopButton 활성화 여부 변경
				a.shop[i] = false;  //shop = 골라진 칸
				break;
			}
			if(j == 7 && a.playerunitlevel1[a.num[i] - 1] == 2)
			{
				a.playerunitlevel1[a.num[i] - 1] = 0;         //1성 카드개수 초기화
				a.playerunitlevel2[a.num[i] - 1]++;           //2성 카드개수 추가
				b = 0;
				for (int k = 0; k < 8; k++)
				{
					if (a.warehouse[k] == a.num[i] && a.warehouselevel[k] == 1)      //2성 될 카드의 1성카드들 지워주기
					{
						a.warehouse[k] = -1;        //k번째 창고에 -1 집어넣기(창고 비우기)
						a.warehouselevel[k] = 0;        //창고에 있는 카드 레벨 0으로 초기화
                        Warehouse[k].GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                        Warehouse[k].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                        Warehouse[k].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;   //창고가 비었을 때 다시 창고 이미지 보여주기

                        //1성 카드들 중 첫번째 카드만 2성 이미지로 보여주기
                        if (b == 0)
						{
							a.warehouse[k] = a.num[i];      // ??
							a.warehouselevel[k] = 2;        //창고에 있는 카드레벨 2로 만들기
                            Warehouse[k].GetComponent<Image>().sprite = Resources.Load("카드배경_2", typeof(Sprite)) as Sprite; //카드 이미지 보여주기
                            Warehouse[k].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load(a.num[i].ToString(), typeof(Sprite)) as Sprite;
                            Warehouse[k].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("카드템플릿_2", typeof(Sprite)) as Sprite;
                            b++;        // b=0일때만 작업 수행하기 때문에, 그 뒤로 발견되는 1성 카드들은 그냥 사라지기만 할 것이다.
						}
					}
				}

				//2성 카드 3개면 3성 하나 만들기 (2성카드 하나 만드는 거랑 동일)
				if (a.playerunitlevel2[a.num[i] - 1] == 3)
				{
					a.playerunitlevel2[a.num[i] - 1] = 0;
					a.playerunitlevel3[a.num[i] - 1]++;
					b = 0;
					for (int k = 0; k < 8; k++)
					{
						if (a.warehouse[k] == a.num[i] && a.warehouselevel[k] == 2)
						{
							a.warehouse[k] = -1;
							a.warehouselevel[k] = 0;
							Warehouse[k].GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
							Warehouse[k].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                            Warehouse[k].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
							if (b == 0)
							{
								a.warehouse[k] = a.num[i];
								a.warehouselevel[k] = 3;
                                Warehouse[k].GetComponent<Image>().sprite = Resources.Load("카드배경_3", typeof(Sprite)) as Sprite; //카드 이미지 보여주기
                                Warehouse[k].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load(a.num[i].ToString(), typeof(Sprite)) as Sprite;
                                Warehouse[k].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("카드템플릿_3", typeof(Sprite)) as Sprite;
                                b++;
							}
						}
					}
				}
				ShopButton.SetActive(!ShopButton.active);  //ShopButton 활성화 여부 변경
				a.shop[i] = false;  //shop = 골라진 칸
				break;
			}
		}
	}
}

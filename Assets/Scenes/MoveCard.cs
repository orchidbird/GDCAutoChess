using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveCard : MonoBehaviour
{
    public GameObject[] board = new GameObject[20]; //board numbering
    public int[] clickedData = new int[2];  //Data of selected card

    public Vector2 firstPos;    //first position of selected card
    public Image[] cardImage;    //card image sprite
    public string[] cardStat = new string[3];

    public int whatIsHit;   //number of hit board
    playervariable a;   //variable

    // Start is called before the first frame update
    void Start()
    {
        cardImage = new Image[5];
        firstPos = transform.position;

        for (int i = 0; i < 8; i++)
        {
            board[i] = GameObject.Find("유닛창고" + (i + 1).ToString());
        }
        for (int i = 0; i < 12; i++)
        {
            board[i + 8] = GameObject.Find("판" + (i + 1).ToString());
        }

        a = GameObject.Find("field").GetComponent<playervariable>();

        whatIsHit = 0;
    }

    void OnMouseDown()
    {
        cardImage[0] = transform.GetComponent<Image>();
        cardImage[1] = transform.GetChild(0).GetComponent<Image>();
        cardImage[2] = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        cardImage[3] = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        cardImage[4] = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>();

        cardStat[0] = transform.GetChild(0).GetChild(2).GetComponent<Text>().text;
        cardStat[1] = transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().text;
        cardStat[2] = transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>().text;

        if (cardImage[0].sprite != cardImage[1].sprite)
            gameObject.layer = LayerMask.NameToLayer("Selected");
        
        for (int n = 0; n < 20; n++)    //checking data of selected card
        {
            if (this.gameObject == board[n] && n < 8)
            {
                clickedData[0] = a.warehouse[n];
                clickedData[1] = a.warehouselevel[n];
                whatIsHit = n;
                //print("whatIsHit = " + n);
            }
            if (this.gameObject == board[n] && n >= 8)
            {
                clickedData[0] = a.board[n-8];
                clickedData[1] = a.boardLevel[n-8];
                whatIsHit = n;
                //print("whatIsHit = " + n);
            }
        }
    }

    void OnMouseDrag()
    {   
        //repositioning of selected object.
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    void OnMouseUp()
    {
        transform.position = firstPos;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer("onBoard");
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 1f, layerMask);
        if (hit.collider == null)
            return;

        //if the board is not empty
        if (hit.collider.gameObject.transform.GetComponent<Image>().sprite.name != "창고")
            return;
       
        //Moving data of selected card to new board
        if (hit.collider != null && hit.collider.tag == "Board")
        {
            hit.collider.gameObject.transform.GetComponent<Image>().sprite = cardImage[0].sprite;
            hit.collider.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = cardImage[1].sprite;
            hit.collider.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = cardImage[2].sprite;
            hit.collider.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = cardImage[3].sprite;
            hit.collider.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = cardImage[4].sprite;

            hit.collider.gameObject.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = cardStat[0];
            hit.collider.gameObject.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().text = cardStat[1];
            hit.collider.gameObject.transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>().text = cardStat[2];


            for (int n = 0; n < board.Length; n++)
            {
                if (hit.collider.gameObject == board[n] && n < 8)
                {
                    a.warehouse[n] = clickedData[0];
                    a.warehouselevel[n] = clickedData[1];
                    //print("SUCCESS_2ware");
                    //print("card : " + clickedData[0] + "\nstar : " + clickedData[1]);
                }

                if (hit.collider.gameObject == board[n] && n >= 8)
                {
                    a.board[n - 8] = clickedData[0];
                    a.boardLevel[n - 8] = clickedData[1];
                    //print("SUCCESS_2board");
                    //print("card : " + clickedData[0] + "\nstar : " + clickedData[1]);
                }
            }
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("onBoard");
            return;
        }

        //initialize clickedData
        clickedData = new int[2];

        if (whatIsHit < 8)
        {
            a.warehouse[whatIsHit] = -1;
            a.warehouselevel[whatIsHit] = 0;
        }
        else
        {
            a.board[whatIsHit-8] = 0;
            a.boardLevel[whatIsHit-8] = 0;
        }   

        transform.GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
        transform.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
        transform.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
        transform.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
        transform.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;

        transform.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = null;
        transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().text = null;
        transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>().text = null;


        gameObject.layer = LayerMask.NameToLayer("onBoard");

        StartCoroutine("Test");
    }

    void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            for (int n = 0; n < 20; n++)    //checking data of selected card
            {
                if (this.gameObject == board[n] && n < 8)
                {
                    if (a.warehouse[n] < 14)
                    {
                        playervariable.playergold += (int)Mathf.Pow(3,(a.warehouselevel[n] - 1));
                    }
                    else
                    {
                        playervariable.playergold += (int)Mathf.Pow(3, (a.warehouselevel[n] -  1)) * 4;
                    }
                    a.warehouse[n] = -1;
                    a.warehouselevel[n] = 0;
                    board[n].GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                    board[n].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                    board[n].transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                    board[n].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                    board[n].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;

                    board[n].transform.GetChild(0).GetChild(2).GetComponent<Text>().text = null;
                    board[n].transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().text = null;
                    board[n].transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>().text = null;

                }
                if (this.gameObject == board[n] && n >= 8)
                {
                    if (a.board[n - 8] < 14)
                    {
                        playervariable.playergold += a.warehouselevel[n] * 3;
                    }
                    else
                    {
                        playervariable.playergold += a.warehouselevel[n] * 12;
                    }
                    a.board[n-8] = -1;
                    a.boardLevel[n-8] = 0;        //창고에 있는 카드 레벨 0으로 초기화
                    board[n].GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                    board[n].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                    board[n].transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                    board[n].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
                    board[n].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;

                    board[n].transform.GetChild(0).GetChild(2).GetComponent<Text>().text = null;
                    board[n].transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().text = null;
                    board[n].transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>().text = null;
                }
            }
        }
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        print(a.playerUnitType["물"] + " " + a.playerUnitType["불"] + " " + a.playerUnitType["나무"] + " " + a.playerUnitType["땅"] + " " + a.playerUnitType["빛"] + " " + a.playerUnitType["어둠"]);
        print(a.playerUnitClass["전사"] + " " + a.playerUnitClass["마법사"] + " " + a.playerUnitClass["사수"] + " " + a.playerUnitClass["암살자"] + " " + a.playerUnitClass["기사"]);
    }
}

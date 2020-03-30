using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveCard : MonoBehaviour
{
    int turn;
    public bool isRightClick = true;

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

    void Update()
    {
        turn = playervariable.Round % 2;
        if (turn == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                board[i] = GameObject.Find("유닛창고" + (i + 1).ToString());
            }
            for (int i = 0; i < 12; i++)
            {
                board[i + 8] = GameObject.Find("판" + (i + 1).ToString());
            }
        }
        else if (turn == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                board[i] = GameObject.Find("2유닛창고" + (i + 1).ToString());
            }
            for (int i = 0; i < 12; i++)
            {
                board[i + 8] = GameObject.Find("2판" + (i + 1).ToString());
            }
        }
    }

    void OnMouseDown()
    {
        a.howManyOnBoard = 0;
        a.howManyOnBoard2 = 0;
        for (int i = 0; i < a.board.Length; i++)
        {
            if (a.board[i] > 1)
                a.howManyOnBoard++;
            if (a.board2[i] > 1)
                a.howManyOnBoard2++;
        }
        isRightClick = true;
        if(turn == 1)
        {
            if (transform.gameObject.tag == "Board2")
            {
                print("wrong card");
                isRightClick = false;
                return;
            }
        } else if(turn == 0)
        {
            if (transform.gameObject.tag == "Board")
            {
                print("wrong card");
                isRightClick = false;
                return;
            }
        }
        cardImage[0] = transform.GetComponent<Image>();
        cardImage[1] = transform.GetChild(0).GetComponent<Image>();
        cardImage[2] = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        cardImage[3] = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        cardImage[4] = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>();

        cardStat[0] = transform.GetChild(0).GetChild(2).GetComponent<Text>().text;
        cardStat[1] = transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().text;
        cardStat[2] = transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>().text;

        if(turn == 1)
        {
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
                    clickedData[0] = a.board[n - 8];
                    clickedData[1] = a.boardLevel[n - 8];
                    whatIsHit = n;
                    //print("whatIsHit = " + n);
                }
            }
        }
        else if(turn == 0)
        {
            if (cardImage[0].sprite != cardImage[1].sprite)
                gameObject.layer = LayerMask.NameToLayer("Selected2");

            for (int n = 0; n < 20; n++)    //checking data of selected card
            {
                if (this.gameObject == board[n] && n < 8)
                {
                    clickedData[0] = a.warehouse2[n];
                    clickedData[1] = a.warehouse2level[n];
                    whatIsHit = n;
                    //print("whatIsHit = " + n);
                }
                if (this.gameObject == board[n] && n >= 8)
                {
                    clickedData[0] = a.board2[n - 8];
                    clickedData[1] = a.board2Level[n - 8];
                    whatIsHit = n;
                    //print("whatIsHit = " + n);
                }
            }
        }

    }

    void OnMouseDrag()
    {   
        if(isRightClick == false)
        {
            return;
        }
        //repositioning of selected object.
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    void OnMouseUp()
    {
        if(turn == 1)
        {

            transform.position = firstPos;
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int layerMask = 1 << LayerMask.NameToLayer("onBoard");
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 1f, layerMask);

            //수정 필요 : 창고로 다시 옮기는 것이 안됨
            if (int.Parse(Status.Level1.text.ToString()) + 1 == a.howManyOnBoard)
            {
                print("Too many cards on the Board");
                isRightClick = false;
                return;
            }

            if (hit.collider == null)
                return;

            if (hit.collider.tag == "Board2")
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
                a.board[whatIsHit - 8] = 0;
                a.boardLevel[whatIsHit - 8] = 0;
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
        } else if (turn == 0)
        {
            transform.position = firstPos;
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int layerMask = 1 << LayerMask.NameToLayer("onBoard2");
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 1f, layerMask);


            if (int.Parse(Status.Level2.text.ToString()) + 1 == a.howManyOnBoard2)
            {
                print("Too many cards on the Board");
                isRightClick = false;
                return;
            }


            if (hit.collider == null)
                return;

            if (hit.collider.tag == "Board")
                return;

            //if the board is not empty
            if (hit.collider.gameObject.transform.GetComponent<Image>().sprite.name != "창고")
                return;

            //Moving data of selected card to new board
            if (hit.collider != null && hit.collider.tag == "Board2")
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
                        a.warehouse2[n] = clickedData[0];
                        a.warehouse2level[n] = clickedData[1];
                        //print("SUCCESS_2ware");
                        //print("card : " + clickedData[0] + "\nstar : " + clickedData[1]);
                    }

                    if (hit.collider.gameObject == board[n] && n >= 8)
                    {
                        a.board2[n - 8] = clickedData[0];
                        a.board2Level[n - 8] = clickedData[1];
                        //print("SUCCESS_2board");
                        //print("card : " + clickedData[0] + "\nstar : " + clickedData[1]);
                    }
                }
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("onBoard2");
                return;
            }

            //initialize clickedData
            clickedData = new int[2];

            if (whatIsHit < 8)
            {
                a.warehouse2[whatIsHit] = -1;
                a.warehouse2level[whatIsHit] = 0;
            }
            else
            {
                a.board2[whatIsHit - 8] = 0;
                a.board2Level[whatIsHit - 8] = 0;
            }

            transform.GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
            transform.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
            transform.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
            transform.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
            transform.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;

            transform.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = null;
            transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().text = null;
            transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>().text = null;


            gameObject.layer = LayerMask.NameToLayer("onBoard2");

            StartCoroutine("Test2");
        }
        
    }

    void OnMouseOver()
    {
        if (this.gameObject.transform.GetComponent<Image>().sprite.name == "창고" && this.tag == "Selected")
            this.gameObject.layer = LayerMask.NameToLayer("onBoard");
        if (this.gameObject.transform.GetComponent<Image>().sprite.name == "창고" && this.tag == "Selected2")
            this.gameObject.layer = LayerMask.NameToLayer("onBoard2");

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (this.gameObject.transform.GetComponent<Image>().sprite.name == "창고")
                return;
            if(turn == 1)
            {
                for (int n = 0; n < 20; n++)    //checking data of selected card
                {
                    if (this.gameObject == board[n] && n < 8)
                    {
                        //if (a.warehouse[n] < 14)
                        //{
                        //    playervariable.playergold += (int)Mathf.Pow(3, (a.warehouselevel[n] - 1));
                        //}
                        //else
                        //{
                        //    playervariable.playergold += (int)Mathf.Pow(3, (a.warehouselevel[n] - 1)) * 4;
                        //}
                        playervariable.playergold += (int)Mathf.Pow(3, (a.warehouselevel[n] - 1)) * a.heroCost[a.warehouse[n]-1];
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
                        //if (a.board[n - 8] < 14)
                        //{
                        //    playervariable.playergold += a.boardLevel[n] * 3;
                        //}
                        //else
                        //{
                        //    playervariable.playergold += a.boardLevel[n] * 12;
                        //}
                        playervariable.playergold += (int)Mathf.Pow(3, (a.boardLevel[n - 8] - 1)) * a.heroCost[a.board[n - 8]-1];
                        a.board[n - 8] = -1;
                        a.boardLevel[n - 8] = 0;        //창고에 있는 카드 레벨 0으로 초기화
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
            } else if(turn == 0)
            {
                for (int n = 0; n < 20; n++)    //checking data of selected card
                {
                    if (this.gameObject == board[n] && n < 8)
                    {
                        //if (a.warehouse[n] < 14)
                        //{
                        //    playervariable.player2gold += (int)Mathf.Pow(3, (a.warehouse2level[n] - 1));
                        //}
                        //else
                        //{
                        //    playervariable.player2gold += (int)Mathf.Pow(3, (a.warehouse2level[n] - 1)) * 4;
                        //}
                        playervariable.player2gold += (int)Mathf.Pow(3, (a.warehouse2level[n] - 1)) * a.heroCost[a.warehouse2[n]-1];
                        a.warehouse2[n] = -1;
                        a.warehouse2level[n] = 0;
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
                        //if (a.board2[n - 8] < 14)
                        //{
                        //    playervariable.player2gold += a.board2Level[n] * 3;
                        //    print(a.boardLevel[n]);
                        //}
                        //else
                        //{
                        //    playervariable.player2gold += a.board2Level[n] * 12;
                        //}
                        playervariable.player2gold += (int)Mathf.Pow(3, (a.board2Level[n-8] - 1)) * a.heroCost[a.board2[n-8]-1];
                        a.board2[n - 8] = -1;
                        a.board2Level[n - 8] = 0;        //창고에 있는 카드 레벨 0으로 초기화
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
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        print(a.playerUnitType["물"] + " " + a.playerUnitType["불"] + " " + a.playerUnitType["나무"] + " " + a.playerUnitType["땅"] + " " + a.playerUnitType["빛"] + " " + a.playerUnitType["어둠"]);
        print(a.playerUnitClass["전사"] + " " + a.playerUnitClass["마법사"] + " " + a.playerUnitClass["사수"] + " " + a.playerUnitClass["암살자"] + " " + a.playerUnitClass["기사"]);
    }
    IEnumerator Test2()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        print(a.player2UnitType["물"] + " " + a.player2UnitType["불"] + " " + a.player2UnitType["나무"] + " " + a.player2UnitType["땅"] + " " + a.player2UnitType["빛"] + " " + a.player2UnitType["어둠"]);
        print(a.player2UnitClass["전사"] + " " + a.player2UnitClass["마법사"] + " " + a.player2UnitClass["사수"] + " " + a.player2UnitClass["암살자"] + " " + a.player2UnitClass["기사"]);
    }
}

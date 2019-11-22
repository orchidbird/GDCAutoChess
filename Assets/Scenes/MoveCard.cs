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
    public Image[] cardImage = new Image[3];    //card image sprite

    public int whatIsHit;   //number of hit board
    playervariable a;   //variable

    // Start is called before the first frame update
    void Start()
    {
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
        cardImage[2] = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        gameObject.layer = LayerMask.NameToLayer("Selected");
        
        for (int n = 0; n < 20; n++)    //checking data of selected card
        {
            if (this.gameObject == board[n] && n < 8)
            {
                clickedData[0] = a.warehouse[n];
                clickedData[1] = a.warehouselevel[n];
                whatIsHit = n;
                print("whatIsHit = " + n);
            }
            if (this.gameObject == board[n] && n >= 8)
            {
                clickedData[0] = a.board[n-8];
                clickedData[1] = a.boardLevel[n-8];
                whatIsHit = n;
                print("whatIsHit = " + n);
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
            hit.collider.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = cardImage[2].sprite;

            for (int n = 0; n < board.Length; n++)
            {
                if (hit.collider.gameObject == board[n] && n < 8)
                {
                    a.warehouse[n] = clickedData[0];
                    a.warehouselevel[n] = clickedData[1];
                    print("SUCCESS_2ware");
                    print("card : " + clickedData[0] + "\nstar : " + clickedData[1]);
                }

                if (hit.collider.gameObject == board[n] && n >= 8)
                {
                    a.board[n - 8] = clickedData[0];
                    a.boardLevel[n - 8] = clickedData[1];
                    print("SUCCESS_2board");
                    print("card : " + clickedData[0] + "\nstar : " + clickedData[1]);
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
        transform.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite; ;
        gameObject.layer = LayerMask.NameToLayer("onBoard");
    }
}

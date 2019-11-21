using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveCard : MonoBehaviour
{
    public GameObject[] board = new GameObject[20];
    public GameObject[] clickedCard = new GameObject[3];
    public int[] clickedData = new int[2];

    public Vector2 firstPos;
    public Image[] cardImage = new Image[3];

    public int whatIsHit;
    playervariable a;

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

        for (int n = 0; n < 20; n++)
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

        if (hit.collider.gameObject.transform.GetComponent<Image>().sprite.name != "창고")
            return;
       
        if (hit.collider != null && hit.collider.tag == "Board")
        {
            hit.collider.gameObject.transform.GetComponent<Image>().sprite = cardImage[0].sprite;
            hit.collider.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = cardImage[1].sprite;
            hit.collider.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = cardImage[2].sprite;

            for (int n = 0; n < board.Length; n++)
            {
                if (hit.collider.gameObject == board[n] && n >= 8)
                {
                    a.board[n - 8] = clickedData[0];
                    a.boardLevel[n - 8] = clickedData[1];
                    print("SUCCESS- ware2board");
                }

                if (hit.collider.gameObject == board[n] && n < 8)
                {
                    a.warehouse[n] = clickedData[0];
                    a.warehouselevel[n] = clickedData[1];
                    print("SUCCESS - ware2ware");
                }
            }
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("onBoard");
            return;
        }

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
        transform.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite; ;//창고가 비었을 때 다시 창고 이미지 보여주기

        gameObject.layer = LayerMask.NameToLayer("onBoard");

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject[] board = new GameObject[9];
    public GameObject[] clickedCard = new GameObject[3];
    public int[] clickedData = new int[3];
    public GameObject selected;
    public int isClicked;
    playervariable a;
    //public Image image;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 8; i++)
        {
            board[i] = GameObject.Find("유닛창고" + (i + 1).ToString());
        }
        for(int i = 0; i < 1; i++)
        {
            board[i + 8] = GameObject.Find("판" + (i + 1).ToString());
        }

        isClicked = 0;

        a = GameObject.Find("field").GetComponent<playervariable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //CastRay();
        }
    }

    void OnMouseDrag()
    {
        float distance = 10;

        print("Drag!!");
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }
    /*
    void CastRay()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 1f);
        print(hit.collider.tag);
        clickedCard = new GameObject[3];
        if (hit.collider.tag == "Board")
        {
            isClicked = 1;

            print(hit.collider.gameObject.name);
            clickedCard[0] = hit.collider.gameObject;
            clickedCard[1] = hit.collider.gameObject.transform.GetChild(0).gameObject;
            clickedCard[2] = hit.collider.gameObject.transform.GetChild(0).GetChild(0).gameObject;  
            

            for(int i = 0; i < board.Length + a.warehouse.Length; i++)
            {
                if(board[i] == clickedCard[0])
                {
                    clickedData[0] = i;
                    if (i < 8)
                    {
                        clickedData[1] = a.warehouselevel[i];
                        clickedData[2] = a.num[a.warehouse[i]];
                    }
                    else
                    {
                        clickedData[1] = a.boardLevel[i-8];
                        clickedData[2] = a.num[a.board[i-8]];
                    }
                        
                }
            }
        }
        else
        {
            isClicked = 0;
        }
    }
    */
}

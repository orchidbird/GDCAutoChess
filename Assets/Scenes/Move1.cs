using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Move1 : MonoBehaviour
{
    public GameObject[] board = new GameObject[9];
    public GameObject[] clickedCard = new GameObject[3];
    public int[] clickedData = new int[3];

    public GameObject selected;
    public int isClicked;

    public Vector2 firstPos;
    public Image[] cardImage = new Image[3];

    playervariable a;

    // Start is called before the first frame update
    void Start()
    {
        firstPos = transform.position;

        for (int i = 0; i < 8; i++)
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
    void OnMouseDown()
    {
        cardImage[0] = transform.GetComponent<Image>();
        cardImage[1] = transform.GetChild(0).GetComponent<Image>();
        cardImage[2] = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        print(EventSystem.current.currentSelectedGameObject.name);
        gameObject.layer = LayerMask.NameToLayer("Selected");
    }


    void OnMouseDrag()
    {
        print("Drag!!");
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    void OnMouseUp()
    {
        transform.position = firstPos;
        gameObject.layer = LayerMask.NameToLayer("Selected");
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer("onBoard");
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 1f, layerMask);
        print("HIT : " + hit.collider.name);
        if (hit.collider != null && hit.collider.tag == "Board")
        {
            hit.collider.gameObject.transform.GetComponent<Image>().sprite = cardImage[0].sprite;
            hit.collider.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = cardImage[1].sprite;
            hit.collider.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = cardImage[2].sprite;
        }

        transform.GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
        transform.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite;
        transform.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load("창고", typeof(Sprite)) as Sprite; ;//창고가 비었을 때 다시 창고 이미지 보여주기

        gameObject.layer = LayerMask.NameToLayer("Selected");

    }
}

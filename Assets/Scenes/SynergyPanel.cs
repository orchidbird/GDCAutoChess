using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//시너지 관련 스크립트
public class SynergyPanel : MonoBehaviour
{
    void OnMouseEnter()
    {
        transform.GetChild(2).gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        transform.GetChild(2).gameObject.SetActive(false);
    }

}

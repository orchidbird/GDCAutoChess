using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public GameObject LevelLabel;
    Text PlayerLevel;

    void Awake()
    {
        PlayerLevel = LevelLabel.GetComponent<Text>();
    }

    void Update()
    {
        PlayerLevel.text = playervariable.playerlevel.ToString();
    }
}

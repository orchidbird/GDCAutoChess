using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gold : MonoBehaviour
{
	public GameObject GoldLabel;
	Text PlayerGold;

	void Awake()
    {
		PlayerGold = GoldLabel.GetComponent<Text>();
    }

	void Update()
	{
		PlayerGold.text = playervariable.playergold.ToString();
	}
}

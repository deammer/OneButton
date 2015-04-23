﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverMenu : MonoBehaviour
{
	public Text CoinsDisplay;
	public Text HeightDisplay;

	void Start()
	{
		CoinsDisplay.text = "Coins collected: " + GameManager.instance.CoinsPickedUp;
		HeightDisplay.text = "Height reached: " + GameManager.instance.HeightReached.ToString("F2");
	}

	void Update ()
	{
		if (Input.anyKeyDown)
			Application.LoadLevel("Game");
	}
}

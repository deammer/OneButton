using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverMenu : MonoBehaviour
{
	public Text CoinsDisplay;
	public Text HeightDisplay;

	void Start()
	{
		//if (GameManager.instance != null)
		{
			CoinsDisplay.text = "Coins collected: " + GameManager.CoinsPickedUp;
			HeightDisplay.text = "Height reached: " + GameManager.HeightReached.ToString("F2");
		}
	}

	void Update ()
	{
		if (Input.anyKeyDown)
			Application.LoadLevel("Game");
	}
}

using UnityEngine;
using System.Collections;

public class GameOverMenu : MonoBehaviour
{
	void Update ()
	{
		if (Input.anyKeyDown)
			Application.LoadLevel("Game");
	}
}

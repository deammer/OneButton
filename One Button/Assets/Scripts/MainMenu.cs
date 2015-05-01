using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{

	void Update ()
	{
		if (Input.anyKeyDown)
			Application.LoadLevel("Game");
	}
}

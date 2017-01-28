using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour {
	[SerializeField]
	private GameObject menuCanvas;

	private bool isPaused = false;

	void Awake()
	{
		menuCanvas.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			TogglePause();
		}
	}

	public void TogglePause()
	{
		isPaused = !isPaused;

		Time.timeScale = isPaused ? 0 : 1;

		menuCanvas.SetActive(isPaused);
	}

	public void QuitGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("MainMenu");
	}
}

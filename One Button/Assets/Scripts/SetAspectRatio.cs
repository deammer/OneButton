using UnityEngine;
using System.Collections;

public class SetAspectRatio : MonoBehaviour
{
	void Awake()
	{
		float xFactor = Screen.width / 384f;
		float yFactor = Screen.height / 576f;

		Camera.main.rect = new Rect(0, 0, 1f, xFactor / yFactor);
	}
}
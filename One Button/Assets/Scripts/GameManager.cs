using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	[RangeAttribute(0f,2f)]
	public float PlatformSpeed = .5f;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this) // THERE CAN BE ONLY ONE
			Destroy(gameObject);
	}
}

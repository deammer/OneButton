using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	[RangeAttribute(0f,2f)]
	public float PlatformSpeed = .5f;
	[HideInInspector]
	public Transform SpawnZone;
	[HideInInspector]
	public Transform RemoveZone;
	[HideInInspector]
	public int CoinsPickedUp = 0;
	[HideInInspector]
	public float HeightReached = 0f;

	void Awake()
	{
		// there can be only one
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		// round up the children
		SpawnZone = transform.Find("SpawnZone");
		RemoveZone = transform.Find("RemoveZone");
	}

	void Start()
	{
		// reset the game
		CoinsPickedUp = 0;
		HeightReached = 0f;
	}

	void Update()
	{
		HeightReached += Time.deltaTime;
	}
}

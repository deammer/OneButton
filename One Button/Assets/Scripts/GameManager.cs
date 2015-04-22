using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	[RangeAttribute(0f,20f)]
	public float PlatformSpeed = .5f;
	private float currentPlatformSpeed;

	[HideInInspector]
	public Transform SpawnZone;
	[HideInInspector]
	public Transform RemoveZone;
	[HideInInspector]
	public int CoinsPickedUp = 0;
	[HideInInspector]
	public float HeightReached = 0f;

	private PlatformSpawner spawner;

	private bool gameStarted = false;

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

		// cache the spawner
		spawner = transform.Find("PlatformSpawner").GetComponent<PlatformSpawner>();
	}

	void Start()
	{
		// reset the game
		CoinsPickedUp = 0;
		HeightReached = 0f;
		gameStarted = false;

		currentPlatformSpeed = PlatformSpeed;
		PlatformSpeed = 0;

		spawner.Disable();
	}

	IEnumerator BeginGame()
	{
		float elapsed = 0;
		float start = PlatformSpeed;
		while (elapsed < 1.5f)
		{
			elapsed = Mathf.MoveTowards(elapsed, 1.5f, Time.deltaTime);
			PlatformSpeed = start + (currentPlatformSpeed - start) * (elapsed / 1.5f);
			yield return 0;
		}
		PlatformSpeed = currentPlatformSpeed;

		// enable spawning
		spawner.Enable();
	}

	void Update()
	{
		HeightReached += Time.deltaTime;
	}

	public void CoinCollected()
	{
		if (!gameStarted)
		{
			gameStarted = true;
			StartCoroutine(BeginGame());
		}
	}
}

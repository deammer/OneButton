using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	[RangeAttribute(0f,20f)]
	public float NormalPlatformSpeed = 2f;
	public float PlatformSpeedMod = 1.5f;
	public float PlatformSpeed { get { return NormalPlatformSpeed * heightMod; } }
	private float currentPlatformSpeed;
	private float heightMod = 1f;
	private float distanceFromCenterToTop;

	[HideInInspector]
	public Transform SpawnZone;
	[HideInInspector]
	public Transform RemoveZone;
	[HideInInspector]
	public int CoinsPickedUp = 0;
	[HideInInspector]
	public float HeightReached = 0f;

	private PlatformSpawner spawner;
	private TrapSpawner trapSpawner;

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

		// cache the spawners
		trapSpawner = transform.Find("TrapSpawner").GetComponent<TrapSpawner>();
		spawner = transform.Find("PlatformSpawner").GetComponent<PlatformSpawner>();
	}

	void Start()
	{
		// reset the game
		CoinsPickedUp = 0;
		HeightReached = 0f;
		gameStarted = false;

		currentPlatformSpeed = NormalPlatformSpeed;
		NormalPlatformSpeed = 0;

		distanceFromCenterToTop = transform.Find("CameraTop").position.y;

		spawner.Disable();
		trapSpawner.Disable();
	}

	void Update()
	{
		heightMod = Mathf.Max(1f, 1f + PlayerController.instance.transform.position.y / distanceFromCenterToTop);

		HeightReached += Time.deltaTime;
	}

	public void GameOver()
	{
		PlayerController.instance.gameObject.SetActive(false);
		trapSpawner.Disable();
		spawner.Disable();
		StartCoroutine(TweenToGameOver());
	}

	IEnumerator TweenToGameOver()
	{
		float duration = 3f;
		float tweenDuration = 1f;
		float elapsed = 0;
		currentPlatformSpeed = NormalPlatformSpeed;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;

			NormalPlatformSpeed = Mathf.Lerp(currentPlatformSpeed, 0, elapsed / tweenDuration);

			yield return 0;
		}

		Application.LoadLevel("GameOver");
	}

	IEnumerator BeginGame()
	{
		float elapsed = 0;
		while (elapsed < 1.5f)
		{
			elapsed = Mathf.MoveTowards(elapsed, 1.5f, Time.deltaTime);
			NormalPlatformSpeed = Mathf.Lerp(0, currentPlatformSpeed, elapsed / 1.5f);
			yield return 0;
		}
		NormalPlatformSpeed = currentPlatformSpeed;

		// enable spawning
		spawner.Enable();
		trapSpawner.Enable();
	}

	public void CoinCollected()
	{
		CoinsPickedUp ++;
		
		if (!gameStarted)
		{
			gameStarted = true;
			StartCoroutine(BeginGame());
		}
	}
}

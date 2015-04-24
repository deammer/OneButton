using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour
{
	public static PlatformSpawner instance;

	public Transform Platform2;
	public Transform Platform3;
	public Transform Platform4;
	public Transform Platform5;
	private Transform[] platforms;

	public Transform Coin;

	public Transform LeftBoundary;
	public Transform RightBoundary;
	private float left;
	private float right;

	public int SpawnDistance = 3;
	private Transform lastPlatformSpawned;
	public Transform LatestPlatform { get { return lastPlatformSpawned; } }
	private float spawnY;

	void Awake()
	{
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);
	}

	void Start ()
	{
		platforms = new Transform[]{Platform2, Platform3, Platform4, Platform5};

		left = LeftBoundary.position.x;
		right = RightBoundary.position.x;

		spawnY = GameManager.instance.SpawnZone.position.y;

		SpawnPlatform();
	}

	void Update ()
	{
		float distanceFromLastPlatform = Mathf.Abs(lastPlatformSpawned.position.y - spawnY);

		if (distanceFromLastPlatform >= SpawnDistance)
			SpawnPlatform();
	}

	private void SpawnPlatform()
	{
		Transform platform = Instantiate(platforms[Random.Range(0, platforms.Length)]);
		float halfWidth = platform.GetComponent<BoxCollider2D>().size.x * .5f;
		Vector3 position = new Vector3(Random.Range(left + halfWidth, right - halfWidth), GameManager.instance.SpawnZone.position.y);

		platform.position = position;
		platform.SetParent(transform);

		lastPlatformSpawned = platform;
	}

	public void Enable() { enabled = true; }
	public void Disable() { enabled = false; }
}

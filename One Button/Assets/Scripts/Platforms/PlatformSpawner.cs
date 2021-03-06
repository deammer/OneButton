﻿using UnityEngine;
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
	public Transform LastPlatformSpawned { get; private set; }
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
		float distanceFromLastPlatform = Mathf.Abs(LastPlatformSpawned.position.y - spawnY);

		if (distanceFromLastPlatform >= SpawnDistance)
			SpawnPlatform();
	}

	private void SpawnPlatform()
	{
		Transform platform = Instantiate(platforms[Random.Range(0, platforms.Length)]);
		EdgeCollider2D collider = platform.GetComponent<EdgeCollider2D>();
		float halfWidth = Mathf.Abs(collider.points[0].x - collider.points[1].x) * .5f;

		// keep the x whole
		float x = left + halfWidth + Random.Range(0, (int)Mathf.Floor(right - left - halfWidth * 2));
		Vector3 position = new Vector3(x, GameManager.instance.SpawnZone.position.y);

		platform.position = position;
		platform.SetParent(transform);

		LastPlatformSpawned = platform;

		// spawn coins
		int numCoins = Random.Range(0, (int)3);
		int width = (int)(halfWidth * 2f);
		int amount = Random.Range(0, width);

		if (amount == 0) return;

		position.x -= (amount - 1) / width;
		position.y += 1f; // spawn above the platform

		for (int i = 0; i < amount; i++)
		{
			position.x = platform.position.x - halfWidth + (float)i * (float)width / (float)amount + (float)width / (float)amount * .5f;
			Transform coin = Instantiate(Coin, position, Quaternion.identity) as Transform;
			coin.SetParent(platform);
		}
	}

	public void Enable() { enabled = true; }
	public void Disable() { enabled = false; }
}

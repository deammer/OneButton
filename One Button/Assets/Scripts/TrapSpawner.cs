﻿using UnityEngine;
using System.Collections;

public class TrapSpawner : MonoBehaviour
{
	public Transform [] Traps;

	public int SpawnSpacing = 10;
	private Transform lastTrapSpawned;
	private float spawnY;

	void Start ()
	{
		spawnY = GameManager.instance.SpawnZone.position.y;

		if (Traps.Length == 0)
		{
			Debug.LogError("Can't spawn any traps.");
			gameObject.SetActive(false);
		}

		SpawnTrap();
	}
	
	void Update ()
	{
		float distanceFromLastSpawn = Mathf.Abs(lastTrapSpawned.position.y - spawnY);
		
		if (distanceFromLastSpawn >= SpawnSpacing)
			SpawnTrap();
	}

	private void SpawnTrap()
	{
		Transform trap = Instantiate(Traps[Random.Range(0, Traps.Length)]);
		trap.SetParent(transform);

		int side = Random.Range(0, 1f) > .5f ? 1 : -1;

		// set the location
		Vector3 location = trap.position;
		location.x *= side;
		if (lastTrapSpawned == null)
			location.y = spawnY;
		else
			location.y = lastTrapSpawned.position.y + SpawnSpacing;
		trap.position = location;

		// todo: check collision with platforms

		// set the scale
		Vector3 scale = trap.localScale;
		scale.x *= side;
		trap.localScale = scale;

		lastTrapSpawned = trap;
	}

	public void Enable() { enabled = true; }
	public void Disable() { enabled = false; }
}
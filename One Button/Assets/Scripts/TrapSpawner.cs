using UnityEngine;
using System.Collections;

public class TrapSpawner : MonoBehaviour
{
	public Transform [] Traps;

	public int SpawnSpacing = 10;
	private float distanceSinceSpawn;

	void Start ()
	{
		if (Traps.Length == 0)
		{
			Debug.LogError("Can't spawn any traps.");
			gameObject.SetActive(false);
		}

		distanceSinceSpawn = 0;
	}
	
	void Update ()
	{
		distanceSinceSpawn += Time.deltaTime * GameManager.instance.PlatformSpeed;
		if (distanceSinceSpawn >= SpawnSpacing)
		{
			SpawnTrap();
			distanceSinceSpawn -= SpawnSpacing;
		}
	}

	private void SpawnTrap()
	{
		Transform trap = Instantiate(Traps[Random.Range(0, Traps.Length)]);
		trap.SetParent(transform);

		int side = Random.Range(0, 1f) > .5f ? 1 : -1;

		// set the location
		Vector3 location = trap.position;
		location.x *= side;
		location.y = GameManager.instance.SpawnZone.position.y;

		// don't spawn on platforms (TODO: improve this so we can spawn on the same y but not the same x?)
		while (Mathf.Abs(location.y - PlatformSpawner.instance.LatestPlatform.position.y) < 1.5f)
			location.y += 1f;
		trap.position = location;

		// set the rotation (we're not scaling to x = -1, because we wanna keep the particles in the correct spot)
		Vector3 angle = trap.localEulerAngles;
		if (side == -1)
		{
			angle.y += 180;
			trap.localEulerAngles = angle;
			trap.GetComponent<Trap>().Flipped = true;
		}

		GameManager.instance.ShowTrapIndicator(trap);
	}

	public void Enable() { enabled = true; }
	public void Disable() { enabled = false; }
}
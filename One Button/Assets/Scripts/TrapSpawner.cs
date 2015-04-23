using UnityEngine;
using System.Collections;

public class TrapSpawner : MonoBehaviour
{
	public Transform [] Traps;

	public float SpawnPeriod = 3.5f;

	private float delay = 0;

	void Start ()
	{
		if (Traps.Length == 0)
		{
			Debug.LogError("Can't spawn any traps.");
			gameObject.SetActive(false);
		}
	}
	
	void Update ()
	{
		delay -= Time.deltaTime;
		
		if (delay <= 0)
		{
			SpawnTrap();
			delay += SpawnPeriod;
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
		trap.position = location;

		// set the scale
		Vector3 scale = trap.localScale;
		scale.x *= side;
		trap.localScale = scale;
	}

	public void Enable() { enabled = true; }
	public void Disable() { enabled = false; }
}
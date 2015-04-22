using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour
{
	private float spawnY;
	private float removeY;

	void Start ()
	{
		spawnY = GameManager.instance.SpawnZone.position.y;
		removeY = GameManager.instance.RemoveZone.position.y;
	}
	
	void Update ()
	{
		Vector3 position = transform.position;

		position.y -= Time.deltaTime * GameManager.instance.PlatformSpeed;
		transform.position = position;

		if (position.y < removeY)
			Destroy(gameObject);
	}
}

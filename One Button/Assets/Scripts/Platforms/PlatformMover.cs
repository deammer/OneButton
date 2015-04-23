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
		float y = transform.position.y - Time.deltaTime * GameManager.instance.PlatformSpeed;
		transform.SetY(y);

		if (transform.position.y < removeY)
			Destroy(gameObject);
	}
}

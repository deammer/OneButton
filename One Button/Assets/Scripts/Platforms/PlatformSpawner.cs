using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour {

	public Transform Platform2;
	public Transform Platform3;
	public Transform Platform4;
	public Transform Platform5;
	private Transform[] platforms;

	public Transform LeftBoundary;
	public Transform RightBoundary;
	private float left;
	private float right;

	public float SpawnPeriod = 1f;
	private float delay;

	void Start ()
	{
		platforms = new Transform[]{Platform2, Platform3, Platform4, Platform5};

		left = LeftBoundary.position.x;
		right = RightBoundary.position.x;
	}
	
	void Update ()
	{
		delay -= Time.deltaTime;

		if (delay <= 0)
		{
			SpawnPlatform();
			delay += SpawnPeriod;
		}
	}

	private void SpawnPlatform()
	{
		Transform platform = Instantiate(platforms[Random.Range(0, platforms.Length)]);
		float halfWidth = platform.GetComponent<BoxCollider2D>().size.x * .5f;
		Vector3 position = new Vector3(Random.Range(left + halfWidth, right - halfWidth), GameManager.instance.SpawnZone.position.y);

		platform.position = position;
		platform.SetParent(transform);
	}

	public void Enable() { enabled = true; }
	public void Disable() { enabled = false; }
}

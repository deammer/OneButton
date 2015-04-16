using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour
{
	public Transform Boundaries;

	private BoxCollider2D screenBounds;

	void Start ()
	{
		screenBounds = Boundaries.GetComponent<BoxCollider2D>();
	}
	
	void Update ()
	{
		Vector3 position = transform.position;
		if (position.y < screenBounds.transform.position.y - screenBounds.size.y * .5f)
			position.y = screenBounds.bounds.center.y + screenBounds.size.y * .5f;
		
		position.y -= Time.deltaTime * GameManager.instance.PlatformSpeed;
		transform.position = position;
	}
}

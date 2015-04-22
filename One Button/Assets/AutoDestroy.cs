using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour
{
	public float Lifespan = 1f;
	public bool MoveWithPlatforms = true;

	void Start ()
	{
		Destroy(gameObject, Lifespan);
	}

	void Update()
	{
		if (MoveWithPlatforms)
			transform.Translate(0, -Time.deltaTime * GameManager.instance.PlatformSpeed, 0);
	}
}

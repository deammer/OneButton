using UnityEngine;
using System.Collections;

public class SpawnZone : MonoBehaviour
{
	private float originalY;
	private float offsetY;

	void Start ()
	{
		originalY = Mathf.Floor(transform.position.y);
		offsetY = 0;
	}
	
	void Update ()
	{
		offsetY = Mathf.Repeat(offsetY + Time.deltaTime * GameManager.instance.PlatformSpeed, 1f);
		transform.SetY(originalY - offsetY);
	}
}

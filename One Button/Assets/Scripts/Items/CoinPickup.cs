using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour
{
	public Transform CollectEffect;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			GameManager.instance.CoinCollected();

			// add effect
			if (CollectEffect)
				Instantiate(CollectEffect, transform.position, Quaternion.identity);

			Destroy(gameObject);
		}
	}
}

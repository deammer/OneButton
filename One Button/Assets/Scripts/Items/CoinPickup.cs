﻿using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			GameManager.instance.CoinCollected();
			Destroy(gameObject);
		}
	}
}

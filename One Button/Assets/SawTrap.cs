using UnityEngine;
using System.Collections;

public class SawTrap : MonoBehaviour {

	public Transform BloodParticles;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Transform blood = Instantiate(BloodParticles);
			blood.position = transform.position;
			Destroy(gameObject);
		}
	}
}

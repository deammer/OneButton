using UnityEngine;
using System.Collections;

public class SawTrap : MonoBehaviour {

	public Transform BloodParticles;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Transform blood = Instantiate(BloodParticles);
			if (transform.localScale.x == -1f)
			{
				Vector3 rotation = transform.localEulerAngles;
				rotation.y = 270;
				blood.localEulerAngles = rotation;
			}

			blood.position = transform.position;

			GameManager.instance.GameOver();
		}
	}
}

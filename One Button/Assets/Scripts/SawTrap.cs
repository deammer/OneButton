using UnityEngine;
using System.Collections;

public class SawTrap : Trap
{
	public Transform BloodParticles;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Transform blood = Instantiate(BloodParticles);
			if (Flipped)
			{
				Vector3 rotation = blood.transform.localEulerAngles;
				rotation.y = 270;
				blood.localEulerAngles = rotation;
			}

			blood.position = transform.position;

			GameManager.instance.GameOver();
		}
	}
}

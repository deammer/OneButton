using UnityEngine;
using System.Collections;

public class SpikeTrap : Trap
{
	public Transform Gibs;

	private bool collidingWithPlayer = false;
	private bool triggeredGameOver = false; // TODO: deactivate the traps in the GameManager when GameOver() is called

	void Start()
	{
		Transform platform = transform.parent;

		// remove any coins on that platform
		foreach (Transform child in platform)
			if (child != transform)
				GameObject.Destroy(child.gameObject);
	}

	void Update()
	{
		if (!triggeredGameOver && collidingWithPlayer)
		{
			// make sure the player is going downwards
			if (PlayerController.instance.Velocity.y < -1f)
			{
				// gore it up
				if (Gibs)
				{
					Transform gibs = Instantiate(Gibs, transform.position, Quaternion.identity) as Transform;
					gibs.SetParent(transform);
					gibs.position = new Vector3(0, 0, 0);
					gibs.localPosition = new Vector3(0, 0, 0);
				}

				GetComponent<Animator>().Play(Animator.StringToHash("SpikesDead"));

				triggeredGameOver = true;
				GameManager.instance.GameOver();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			collidingWithPlayer = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			collidingWithPlayer = false;
		}
	}
}

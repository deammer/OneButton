using UnityEngine;
using System.Collections;

public class BlockerButton : MonoBehaviour
{
	public Transform Blocker;

	private bool hasBeenActivated = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (hasBeenActivated) return;

		if (other.gameObject.tag == "Player" && PlayerController.instance.Velocity.y < -1f)
		{
			GetComponent<Animator>().SetBool("IsPressed", true);
			Blocker.GetComponent<BlockerTrap>().Open();
			hasBeenActivated = true;
		}
	}
}

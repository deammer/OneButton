using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class BlockerButton : MonoBehaviour
{
	public Transform Blocker;
    public UnityEvent callOnActivate;

	private bool hasBeenActivated = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (hasBeenActivated) return;

		if (other.gameObject.tag == "Player" && PlayerController.instance.Velocity.y < -1f)
		{
			GetComponent<Animator>().SetBool("IsPressed", true);
			Blocker.GetComponent<BlockerTrap>().Open();
			hasBeenActivated = true;

            if (callOnActivate != null)
            {
                callOnActivate.Invoke();
            }
		}
	}
}

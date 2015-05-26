using UnityEngine;
using System.Collections;

public class BlockerTrap : MonoBehaviour
{
	public Transform Explosion;

	private Transform lockedTransform;
	private Transform slotsTransform;
	private Transform rightArm;
	private Transform leftArm;

	void Start()
	{
		lockedTransform = transform.FindChild("Locked");
		slotsTransform = transform.FindChild("Slots");
		rightArm = transform.FindChild("RightArm");
		leftArm = transform.FindChild("LeftArm");

		lockedTransform.gameObject.SetActive(true);
		slotsTransform.gameObject.SetActive(false);
		rightArm.gameObject.SetActive(false);
		leftArm.gameObject.SetActive(false);
	}

	public void Open()
	{
		// disable collision
		GetComponent<BoxCollider2D>().enabled = false;

		lockedTransform.gameObject.SetActive(false);
		slotsTransform.gameObject.SetActive(true);
		rightArm.gameObject.SetActive(true);
		leftArm.gameObject.SetActive(true);

		if (Explosion)
			Instantiate(Explosion, transform.position, Quaternion.identity);


		// animate the arms
		StartCoroutine(rightArm.MoveTo(new Vector3(5.374f, 0), .3f, Ease.ExpoIn));
		StartCoroutine(leftArm.MoveTo(new Vector3(-5.374f, 0), .3f, Ease.ExpoIn));
	}
}

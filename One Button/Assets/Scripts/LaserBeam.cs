using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour
{
	public float FlickerPeriod = .05f;

	private float originalScaleY;
	private Vector3 currentScale;

	void Start()
	{
		currentScale = transform.localScale;
		originalScaleY = currentScale.y;

		StartCoroutine(Flicker());
	}

	private IEnumerator Flicker()
	{
		while (true)
		{
			yield return new WaitForSeconds(FlickerPeriod);
			currentScale.y = originalScaleY * Random.Range(.5f, 1.5f);
			transform.localScale = currentScale;
		}
	}
}

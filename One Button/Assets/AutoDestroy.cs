using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour
{
	public float Lifespan = 1f;

	void Start ()
	{
		Destroy(gameObject, Lifespan);
	}
}

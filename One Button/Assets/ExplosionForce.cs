using UnityEngine;
using System.Collections;

public class ExplosionForce : MonoBehaviour
{
	public float radius = 5.0f;
	public float power = 10.0f;
	
	void Start()
	{
		Vector3 explosionPos = transform.position;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
		foreach (Collider2D hit in colliders)
		{
			Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
			
			if (rb != null)
				rb.AddExplosionForce(power, explosionPos, radius, 3f);
		}
	}
}

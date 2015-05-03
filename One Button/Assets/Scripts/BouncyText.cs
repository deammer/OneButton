using UnityEngine;
using System.Collections;

public class BouncyText : MonoBehaviour
{
	public float Speed = 4f;
	public float AmountX = .2f;
	public float AmountY = .1f;

	public float MaxAngle = 10f;
	public float RotationSpeed = 5f;

	private Vector3 originalAngle;
	private float timer;

	void Start()
	{
		originalAngle = transform.localEulerAngles;
		timer = Random.Range(0, 2f * Mathf.PI);
	}
	
	void Update()
	{
		timer = Mathf.Repeat(timer + Time.deltaTime, Mathf.PI * 2f);
	}

	void OnGUI()
	{
		Vector3 scale = transform.localScale;
		scale.x = 1f + Mathf.Sin(timer * Speed) * AmountX;
		scale.y = 1f + Mathf.Cos (timer * Speed) * AmountY;
		transform.localScale = scale;

		Vector3 rotation = originalAngle;
		rotation.z += Mathf.Sin(timer * RotationSpeed) * MaxAngle;
		transform.localEulerAngles = rotation;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Ext
{
	public static bool Approximately(this Vector3 point, Vector3 other)
	{
		return Mathf.Approximately(point.x, other.x) && Mathf.Approximately(point.y, other.y) && Mathf.Approximately(point.z, other.z);
	}
	
	public static bool IsZero(this Vector3 point)
	{
		return Approximately(point, Vector3.zero);
	}
	
	public static void SetX(this Transform transform, float value)
	{
		var p = transform.localPosition;
		p.x = value;
		transform.localPosition = p;
	}
	
	public static void SetY(this Transform transform, float value)
	{
		var p = transform.localPosition;
		p.y = value;
		transform.localPosition = p;
	}
	
	public static void SetZ(this Transform transform, float value)
	{
		var p = transform.localPosition;
		p.z = value;
		transform.localPosition = p;
	}
	
	public static Vector3 DirectionTo(this Transform transform, Vector3 position)
	{
		return Vector3.Normalize(position - transform.position);
	}

	public static Vector3 DirectionTo(this Transform transform, Transform other)
	{
		return Vector3.Normalize(other.position - transform.position);
	}

	public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
	{
		var dir = (body.transform.position - explosionPosition);
		float wearoff = 1 - (dir.magnitude / explosionRadius);
		body.AddForce(dir.normalized * explosionForce * wearoff);
	}
	
	public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
	{
		var dir = (body.transform.position - explosionPosition);
		float wearoff = 1 - (dir.magnitude / explosionRadius);
		Vector3 baseForce = dir.normalized * explosionForce * wearoff;
		body.AddForce(baseForce);
		
		float upliftWearoff = 1 - upliftModifier / explosionRadius;
		Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
		body.AddForce(upliftForce);
	}
}

using UnityEngine;
using System.Collections;

public class ZOrderByHeight : MonoBehaviour 
{

	private SpriteRenderer rend;

	void Start () 
	{
		rend = GetComponent<SpriteRenderer>();
	}

	void Update () 
	{
		rend.sortingOrder = (int)(transform.position.y);
	}
}

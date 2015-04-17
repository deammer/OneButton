using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour {

	[RangeAttribute(0f,1f)]
	public float ScrollSpeedRatio;
	private Vector2 savedOffset;
	private Renderer renderer;
	private float textureOffset;

	void Start ()
	{
		savedOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset ("_MainTex");
		renderer = GetComponent<Renderer>();
		textureOffset = renderer.material.mainTextureScale.y / transform.localScale.y;
	}
	
	void Update ()
	{
		float y = Mathf.Repeat (Time.time * ScrollSpeedRatio * GameManager.instance.PlatformSpeed * textureOffset, 1f);
		Vector2 offset = new Vector2 (savedOffset.x, y);
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}
	
	void OnDisable ()
	{
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}
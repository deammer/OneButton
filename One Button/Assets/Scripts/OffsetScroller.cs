using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour {

	[RangeAttribute(0f,1f)]
	public float ScrollSpeedRatio;
	private Vector2 savedOffset;
	private Renderer _renderer;
	private float textureOffset;

	void Start ()
	{
		savedOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset ("_MainTex");
		_renderer = GetComponent<Renderer>();
		textureOffset = _renderer.material.mainTextureScale.y / transform.localScale.y;
	}
	
	void Update ()
	{
		float currentOffset = _renderer.sharedMaterial.GetTextureOffset("_MainTex").y;
		float y = Mathf.Repeat (currentOffset + Time.deltaTime * ScrollSpeedRatio * GameManager.instance.PlatformSpeed * textureOffset, 1f);
		Vector2 offset = new Vector2 (savedOffset.x, y);
		_renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}
	
	void OnDisable ()
	{
		_renderer.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}
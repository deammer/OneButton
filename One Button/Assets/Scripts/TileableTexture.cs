using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TileableTexture : MonoBehaviour 
{
	public Vector2 InitialScale;
	public int PosInLayer;

	private Renderer rend;

	void Start()
	{
		rend = GetComponent<Renderer>();
		rend.sortingOrder = PosInLayer;

	}

	void Update () 
	{
		rend.sharedMaterial.mainTextureScale = new Vector2(transform.lossyScale.x/InitialScale.x,transform.lossyScale.y/InitialScale.y);
	}
}

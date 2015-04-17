using UnityEngine;
using System.Collections;
using UnityEditor;

public class ResetPlayerRefs : MonoBehaviour
{
	[MenuItem("Edit/Reset Playerprefs")]
	public static void DeletePlayerPrefs() { PlayerPrefs.DeleteAll(); }
}

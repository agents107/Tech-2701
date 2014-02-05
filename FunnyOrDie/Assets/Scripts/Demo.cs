using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour 
{
	void OnGUI()
	{
		if(GUILayout.Button("Viking")) Application.LoadLevel(1);		// Load Viking Demo
		if(GUILayout.Button("TestBuild"))Application.LoadLevel(2);		// Load Own Build Demo
	}
}

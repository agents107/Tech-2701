using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour 
{
	// Use this for initialization
	private void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
		DontDestroyOnLoad(this);
	}

}

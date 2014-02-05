using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour 
{
	[SerializeField] private TextMesh mRoomName;
	private void Start()
	{
		PhotonNetwork.Instantiate("playerPref",Vector3.up,Quaternion.identity,0);	// Create our player
	}

	private void Update()
	{
		mRoomName.text  = PhotonNetwork.room.name;								// Random Text to display room name
		mRoomName.text += "\n " + PhotonNetwork.room.playerCount.ToString(); 	// Random Text to display num of players
	}
}

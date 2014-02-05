using UnityEngine;
using System.Collections;

public class LobbyScript : MonoBehaviour
{
	// room ID
	private string	mRoomName		= "Your Room Name";
	private int		mNumOfPeople	= 4;

	private void OnGUI()
	{
		// Connection Status
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		// Checking Current Status
		if(PhotonNetwork.connectionState == ConnectionState.Connected)
		{

			// Checking if there are any rooms created
			if(PhotonNetwork.GetRoomList().Length > 0)
			{
				foreach(RoomInfo room in PhotonNetwork.GetRoomList())
				{
					GUILayout.BeginHorizontal("Room");
					GUILayout.Label(room.name + " " + room.playerCount + "/" + room.maxPlayers);
					if(GUILayout.Button("Join"))	PhotonNetwork.JoinRoom(room.name);
					GUILayout.EndHorizontal();
				}

			}
			else 
				GUILayout.Label("No Room is Created");	// No rooms Created

			// Textbox for Room Name
			GUILayout.BeginHorizontal("RoomName");
			GUILayout.Label("Room Name: ");
			mRoomName		= GUILayout.TextField(mRoomName,16);
			GUILayout.EndHorizontal();

			// Text Box for Number of People
			GUILayout.BeginHorizontal("RoomPeople");
			GUILayout.Label("Number of People: ");
			mNumOfPeople	= int.Parse( GUILayout.TextField(mNumOfPeople.ToString(),8) );
			GUILayout.EndHorizontal();

			// Create Button
			if(GUILayout.Button("Create Room"))
			{
				if(mNumOfPeople > 0 && mRoomName != "")
				PhotonNetwork.CreateRoom(mRoomName,true,true,mNumOfPeople);
			}

		}
	}

	// When we joined a room
	private void OnJoinedRoom()
	{
		PhotonNetwork.LoadLevel(3);
	}

}


using UnityEngine; using System.Collections;
using System.Collections.Generic;
public class Text : Photon.MonoBehaviour { 
	private static PhotonView TextPhotonView;
	[SerializeField] public List<string> Strings;
	[SerializeField] public GUIStyle style;
	[SerializeField] public GUIStyle customStyle;
	[SerializeField] private GameObject Manager;
	private GameManager ManagerScript;
	void Start(){
		TextPhotonView = this.GetComponent<PhotonView>();
		style.normal.textColor = Color.white;
		ManagerScript = Manager.GetComponent<GameManager>();
	}
	void Update(){
	}
	void OnGUI() {
		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
		{
			TextPhotonView.RPC ("UpdateString", PhotonTargets.All, PhotonNetwork.player.ID - 1, Strings [PhotonNetwork.player.ID - 1]);
		}
		if(GUI.Button(new Rect(800,718,50,50),"Send"))
		   {
			TextPhotonView.RPC ("UpdateString",PhotonTargets.All,PhotonNetwork.player.ID-1,Strings[PhotonNetwork.player.ID-1]);
		   }
		if(	PhotonNetwork.player.ID==1){
			GUILayout.BeginArea(new Rect(0, 0, 1024, 40), Strings[1],style);
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(0,600,200,40),"Score: " + ManagerScript.Player1Score,style);
			GUILayout.EndArea();
			if(ManagerScript.Player1Clicked == true){
				GUILayout.BeginArea(new Rect(512,512,1024,40), "Waiting For Other Player!",style);
				GUILayout.EndArea();
			}
		}
		if(	PhotonNetwork.player.ID==2){
			GUILayout.BeginArea(new Rect(0, 0, 1024, 40), Strings[0],style);
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(0,600,200,40),"Score: " + ManagerScript.Player2Score,style);
			GUILayout.EndArea();
			if(ManagerScript.Player2Clicked == true){
				GUILayout.BeginArea(new Rect(512,512,1024,40), "Waiting For Other Player!",style);
				GUILayout.EndArea();
			}
		}
		Strings[PhotonNetwork.player.ID-1] = GUI.TextArea(new Rect(0, 728, 800, 40), Strings[PhotonNetwork.player.ID-1], 1000, customStyle); 
	} 
	[RPC]
	void UpdateString(int StringID, string NewString){
		Strings[StringID]=NewString;
	}
}
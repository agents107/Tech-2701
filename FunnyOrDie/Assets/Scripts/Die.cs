using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Die : MonoBehaviour {
	[SerializeField] private GameObject Manager;
	private GameManager ManagerScript;
	private GUITexture GuiImage;
	[SerializeField] private List<Texture> images;
	// Use this for initialization
	void Start () {
		ManagerScript = Manager.GetComponent<GameManager>();
		GuiImage = this.GetComponent<GUITexture>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseDown(){
		if(PhotonNetwork.player.ID==1 && ManagerScript.Player1Clicked == false){
			ManagerScript.GamePhotonView.RPC ("PlayerClicked", PhotonTargets.All, 1, 0);
		}
		if(PhotonNetwork.player.ID==2 && ManagerScript.Player2Clicked == false){
			ManagerScript.GamePhotonView.RPC ("PlayerClicked", PhotonTargets.All, 2, 0);
			GuiImage.texture = images[1];
		}
	}
	void OnMouseOver(){
		GuiImage.texture = images[1];
	}
	void OnMouseExit(){
		GuiImage.texture = images[0];
	}
}

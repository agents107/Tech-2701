using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Photon.MonoBehaviour {
	public PhotonView GamePhotonView;
	[SerializeField] private GameObject TextObject; 
	[SerializeField] private GUITexture image;
	[SerializeField] public List<Texture> imagearray;
	private Text TextScript;
	private int ImageNum;
	private int ClickedNum;
	private int ClickedNum2;
	public int Player1Score;
	public int Player2Score;
	public bool Player1Clicked = false;
	public bool Player2Clicked = false;
	public bool IsTiming = false;
	private float timer = 0;
	// Use this for initialization
	void Start () {
		if(PhotonNetwork.playerList.Length == 0)
		{
			return;
		}
		GamePhotonView = this.GetComponent<PhotonView>();
		TextScript = TextObject.GetComponent<Text>();
		Player1Clicked = false;
		Player2Clicked = false;
		if(PhotonNetwork.player.ID == 1){
			int size = 0;
			size =imagearray.Count;
			ImageNum =Random.Range(0,size);
			image.texture=imagearray[ImageNum];
		}
		else{
			image.texture=imagearray[ImageNum];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(IsTiming)
		{
			timer += Time.deltaTime;	
		}
		if(PhotonNetwork.player.ID == 1){
			if(ClickedNum>0||ClickedNum2>0){
				if(ClickedNum+ClickedNum2 >= 2){
					if(IsTiming == false){
						BeginTimer();
					}
					if(timer > 2.0){
						ChangeImage();
						EndTimer();
					}

				}
			}
		}
		if(PhotonNetwork.player.ID == 1){
			GamePhotonView.RPC ("SyncImage", PhotonTargets.Others, ImageNum);
		}
	}
	void BeginTimer()
	{
		timer = 0;
		IsTiming = true;
	}
	void EndTimer(){
		IsTiming = false;
	}
	void ChangeImage(){
		if(ImageNum<imagearray.Count){
			ImageNum++;
		}
		else{
			ImageNum = 0;
		}
		image.texture = imagearray[ImageNum];
		GamePhotonView.RPC ("ResetPlayerClicked", PhotonTargets.All);
		GamePhotonView.RPC ("SyncImage", PhotonTargets.Others, ImageNum);
	}
	[RPC]
	void ResetPlayerClicked(){
		ClickedNum = 0;
		ClickedNum2 = 0;
		Player1Clicked = false;
		Player2Clicked = false;
		TextScript.Strings[0] = "Type your caption here!";
		TextScript.Strings[1] = "Type your caption here!";
	}
	[RPC]
	void SyncImage(int CorrectImage){
		ImageNum= CorrectImage;
		image.texture = imagearray[ImageNum];
	}
	[RPC]
	void PlayerClicked(int playerNum, int Score){
		if(playerNum == 1){
			Player1Clicked = true;
			ClickedNum = 1;
			Player2Score += Score;
		}
		else if(playerNum == 2){
			Player2Clicked = true;
			ClickedNum2 = 1;
			Player1Score += Score;
		}
	}
}

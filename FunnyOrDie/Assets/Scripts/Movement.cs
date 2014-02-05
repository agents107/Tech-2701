using UnityEngine;
using System.Collections;

public class Movement : MoveByKeys
{
	[SerializeField] private TextMesh mText;
	private Vector3 correctPlayerPos = Vector3.zero;
	private Quaternion correctPlayerRot = Quaternion.identity;

	protected override void Start()
	{
		base.Start();	

		// init some of the components
		gameObject.GetComponentInChildren<TextMesh>().text =	"Player " + photonView.ownerId.ToString();
		gameObject.GetComponentInChildren<Camera>().enabled	=	this.photonView.isMine;
	}

	protected override void Update()
	{
		base.Update();
		// If the script is not the local player, Update the position
		if(!photonView.isMine)
		{
			if(Vector3.Distance(transform.position,correctPlayerPos) > 0.2f)	transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
			if(Quaternion.Dot(transform.rotation,correctPlayerRot) > 0.2f)		transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5) ;
		}

		// Displaying Purpose
		mText.text =	"Player " + photonView.ownerId.ToString();
		mText.text += "\nCurrent Position: " + transform.position;
		mText.text += "\nCurren Player Position: " + correctPlayerPos;
		mText.text += "\nVector Distance:" + Vector3.Distance(transform.position,correctPlayerPos);
	}

	
	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else
		{
			// Network player, receive data
			this.correctPlayerPos = (Vector3)stream.ReceiveNext();
			this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
		}
	}
}

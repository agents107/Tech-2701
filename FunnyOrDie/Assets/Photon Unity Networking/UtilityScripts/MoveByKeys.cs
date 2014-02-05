using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhotonView))]
public class MoveByKeys : Photon.MonoBehaviour
{
    public float speed = 10f;

	// Update is called once per frame
	protected virtual void Update ()
	{
		if(photonView.isMine)
		{
		    if (Input.GetKey(KeyCode.A))	        this.transform.position += Vector3.left * (speed * Time.deltaTime);
			if (Input.GetKey(KeyCode.D))            this.transform.position += Vector3.right * (speed * Time.deltaTime);
	        if (Input.GetKey(KeyCode.W))            this.transform.position += Vector3.forward * (speed * Time.deltaTime);
	        if (Input.GetKey(KeyCode.S))            this.transform.position += Vector3.back * (speed * Time.deltaTime);
		}
	}


	protected virtual void Start()
    {
		if(photonView.isMine)
		{
        	this.enabled = this.photonView.isMine;
		}
    }
}

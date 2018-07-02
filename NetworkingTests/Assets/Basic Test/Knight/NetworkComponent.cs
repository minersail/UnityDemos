using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkComponent : Photon.PunBehaviour, IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(GetComponentInChildren<Health>().health);
        }
        else
        {
            // Network player, receive data
            GetComponentInChildren<Health>().health = (float)stream.ReceiveNext();
        }
    }

    // Use this for initialization
    void Start ()
    {
		if (!photonView.isMine)
        {
            GetComponentInChildren<Camera>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*if (GetComponentInChildren<Health>().health <= 0f)
        {
            Manager.Instance.LeaveRoom();
        }*/
    }
}
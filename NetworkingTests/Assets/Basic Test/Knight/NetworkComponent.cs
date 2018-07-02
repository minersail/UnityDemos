using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkComponent : Photon.PunBehaviour, IPunObservable
{
    public static GameObject LocalPlayerInstance;

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
    public void Awake()
    {
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (photonView.isMine)
        {
            LocalPlayerInstance = gameObject;
        }

        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
		if (!photonView.isMine)
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<FlareLayer>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
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
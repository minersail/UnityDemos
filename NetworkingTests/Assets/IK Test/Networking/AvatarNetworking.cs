using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarNetworking : Photon.PunBehaviour, IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.GetChild(1).position);
        }
        else
        {
            transform.GetChild(1).position = (Vector3)stream.ReceiveNext();
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

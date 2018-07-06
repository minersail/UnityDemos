using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarNetworking : Photon.PunBehaviour, IPunObservable
{
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject avatarBody;
    public GameObject avatarCamera;

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

    public void Start()
    {
        if (photonView.isMine)
        {
            avatarBody.SetActive(false);
        }
        else
        {
            leftHand.SetActive(false);
            rightHand.SetActive(false);
            avatarCamera.SetActive(false);
        }
    }
}

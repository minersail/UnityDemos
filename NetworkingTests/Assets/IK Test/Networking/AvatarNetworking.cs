using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarNetworking : Photon.PunBehaviour, IPunObservable
{
    public GameObject leftHandMesh;
    public GameObject rightHandMesh;
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
            avatarBody.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            leftHandMesh.GetComponent<MeshRenderer>().enabled = false;
            rightHandMesh.GetComponent<MeshRenderer>().enabled = false;
            avatarCamera.SetActive(false);
        }
    }
}

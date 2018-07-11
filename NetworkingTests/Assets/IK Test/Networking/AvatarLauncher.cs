using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarLauncher : Photon.PunBehaviour
{
    public GameObject playerPrefab;

    private string _gameVersion = "1";
    private bool isConnecting = false;

    void Awake()
    {
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;
    }

    void Start()
    {
        Connect();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Connect()
    {
        isConnecting = true;

        if (PhotonNetwork.connected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(_gameVersion);
        }
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
    }

    public override void OnDisconnectedFromPhoton()
    {
        isConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 1.5f, 0f), Quaternion.identity, 0);
    }
}

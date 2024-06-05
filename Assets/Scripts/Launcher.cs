using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using System;

public class Launcher : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";

    [SerializeField]
    private byte maxPlayersPerRoom = 6;
    private void Awake()
    {
        //this makes sure that we can use LoadLevel()
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        Connect();

    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    #region MonobehaviourPunCallbackks

    public override void OnConnectedToMaster()
    {
        print("OnConnectedToMaster");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("OnDisconnected");

    }

    public override void OnJoinedRoom()
    {
        print("Joined room");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Room joining failed, creating a room");
        PhotonNetwork.CreateRoom(null, new RoomOptions{ MaxPlayers = maxPlayersPerRoom });
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using System;
namespace com.kalyan.photontut
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        string gameVersion = "1";

        [SerializeField]
        private byte maxPlayersPerRoom = 6;

        [SerializeField]
        private GameObject controlPanel, progressLabel;
        private void Awake()
        {
            //this makes sure that we can use LoadLevel()
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            controlPanel.SetActive(true);
            progressLabel.SetActive(false);
        }

        public void Connect()
        {
            controlPanel.SetActive(false);
            progressLabel.SetActive(true);
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
            controlPanel.SetActive(true);
            progressLabel.SetActive(false);
        }

        public override void OnJoinedRoom()
        {
            print("Joined room");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            print("Room joining failed, creating a room");
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }
        #endregion
    }

}


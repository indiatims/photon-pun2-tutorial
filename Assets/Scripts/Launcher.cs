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
        bool isConnecting;

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
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        #region MonobehaviourPunCallbackks

        public override void OnConnectedToMaster()
        {
            if (isConnecting)
            {
                isConnecting = false;
                print("OnConnectedToMaster");
            
                PhotonNetwork.JoinRandomRoom();
            }
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
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("We load the 'Room for 1' ");

                // #Critical
                // Load the Room Level.
                PhotonNetwork.LoadLevel("Room for 1");
            }
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            print("Room joining failed, creating a room");
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }
        #endregion
    }

}


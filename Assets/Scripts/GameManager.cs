using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

namespace com.kalyan.photontut
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : MonoBehaviour
{
    public GameObject playerPrefab;
    [SerializeField] private GameObject lobbyCamera;
    private Vector3 defaultStartPosition;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");
        defaultStartPosition = new Vector3(0, 0, 0);
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom(
            "Room",
            new RoomOptions(){MaxPlayers=2},
            TypedLobby.Default
        );
    }

    void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(
            playerPrefab.name,
            defaultStartPosition,
            Quaternion.identity,
            0x0
        );
        Debug.Log("Someone Joined");
        lobbyCamera.SetActive(false);
    }
}

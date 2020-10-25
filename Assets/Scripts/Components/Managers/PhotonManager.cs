using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : MonoBehaviour
{
    public RoomData roomData;

    public GameObject playerPrefab;
    [SerializeField] private GameObject lobbyCamera;
    private Vector3 defaultStartPosition;

    void Start()
    {
        if(!PhotonNetwork.connected)
            PhotonNetwork.ConnectUsingSettings(Configs.version);
        defaultStartPosition = new Vector3(0, 0, 0);
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom(
            this.roomData.roomName ?? "Room",
            new RoomOptions(){
                MaxPlayers=Configs.maxPlayers,
                IsVisible=true
            },
            TypedLobby.Default
        );
        Debug.Log(this.roomData.roomName);
    }

    void OnJoinedRoom()
    {
        InitializePlayer();
    }

    void InitializePlayer()
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

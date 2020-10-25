using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonRoomService
{
    public static RoomInfo[] GetRooms()
    {
        return PhotonNetwork.GetRoomList();
    }

    public static void CreateRoom(string name)
    {
        PhotonNetwork.CreateRoom(
            name,
            new RoomOptions(){
                MaxPlayers=Configs.maxPlayers,
                IsVisible=true
            },
            TypedLobby.Default
        );
        Debug.Log($"Created room: {name}");
    }
}

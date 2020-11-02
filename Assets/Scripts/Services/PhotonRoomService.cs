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

    public static void CreateAndJoinRoom(string name, bool loadToRoom = true)
    {
        CreateRoom(name);
        if(loadToRoom)
            LevelManager.Instance.LoadScene(Constants.Levels.SampleScene, true);
    }

    public static void JoinRoom(string roomName, bool loadToRoom = true)
    {
        PhotonNetwork.JoinRoom(roomName);
        if(loadToRoom)
            LevelManager.Instance.LoadScene(Constants.Levels.SampleScene, true);
    }

    public static void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        LevelManager.Instance.LoadScene(Constants.Levels.MainMenu, true);
    }
}

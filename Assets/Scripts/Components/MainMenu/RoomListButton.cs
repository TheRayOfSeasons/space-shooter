using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RoomListButton : MonoBehaviour
{
    public string roomName;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => {
            PhotonRoomService.JoinRoom(this.roomName);
        });
    }
}

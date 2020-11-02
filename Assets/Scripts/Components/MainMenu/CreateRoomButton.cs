using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomButton : MonoBehaviour
{
    public InputField roomInput;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => {
            string roomName = this.roomInput.text;
            PhotonRoomService.CreateAndJoinRoom(roomName);
        });
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => {
            string roomName = "TestRoom";
            PhotonRoomService.CreateAndJoinRoom(roomName);
        });
    }
}

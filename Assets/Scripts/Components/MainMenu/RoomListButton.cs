﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RoomListButton : MonoBehaviour
{
    public string roomName;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => {
            MenuManager.Instance.roomData.roomName = this.roomName;
            PhotonNetwork.JoinRoom(this.roomName);
            LevelManager.Instance.LoadScene(1, true);
        });
    }
}

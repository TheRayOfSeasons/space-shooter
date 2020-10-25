using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public RoomData roomData;

    private static MenuManager instance;
    public static MenuManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
        PhotonNetwork.ConnectUsingSettings(Configs.version);
    }
}

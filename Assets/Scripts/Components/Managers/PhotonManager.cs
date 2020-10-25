using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : MonoBehaviour
{
    public RoomData roomData;

    private static PhotonManager instance;
    public static PhotonManager Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if(!PhotonNetwork.connected)
            PhotonNetwork.ConnectUsingSettings(Configs.version);
    }
}

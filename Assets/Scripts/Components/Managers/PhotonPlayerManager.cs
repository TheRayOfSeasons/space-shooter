using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonPlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    [SerializeField] private GameObject lobbyCamera;
    private Vector3 defaultStartPosition;

    private static PhotonPlayerManager instance;
    public static PhotonPlayerManager Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        defaultStartPosition = new Vector3(0, 0, 0);
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
            Constants.ByteGroups.FIRST
        );
        Debug.Log("Someone Joined");
        lobbyCamera.SetActive(false);
    }
}

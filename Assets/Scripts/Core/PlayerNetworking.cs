using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworking : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private MonoBehaviour[] scriptsToIgnore;

    public PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        Initialize();
    }

    private void Initialize()
    {
        if(!photonView.isMine)
        {
            playerCamera.SetActive(false);
            foreach(MonoBehaviour script in scriptsToIgnore)
                script.enabled = false;
        }
    }
}

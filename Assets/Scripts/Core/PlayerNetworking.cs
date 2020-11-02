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
        this.photonView = GetComponent<PhotonView>();
        this.Initialize();
    }

    private void Initialize()
    {
        if(!photonView.isMine)
        {
            playerCamera.SetActive(false);
            foreach(MonoBehaviour script in scriptsToIgnore)
                script.enabled = false;
        }
        else
        {
            InitializePlayerColor();
        }
    }

    /// <summary>
    /// Publish player color so other players can see.
    /// </summary>
    [PunRPC]
    private void RPCInjectColor(string hexcode)
    {
        Player player = this.GetComponent<Player>();
        player.AssignColor(hexcode);
    }

    private void InitializePlayerColor()
    {
        Player player = this.GetComponent<Player>();
        string hexcode = Constants.EntityColor.GetRandomColorHex();
        Debug.Log(hexcode);
        player.AssignColor(hexcode);
        photonView.RPC("RPCInjectColor", PhotonTargets.AllBuffered, hexcode);
    }
}

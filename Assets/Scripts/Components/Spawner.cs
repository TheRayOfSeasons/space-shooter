using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterEngine;

public class Spawner : MonoBehaviour
{
    public GameObject toSpawn;
    public bool isOnline = true;
    private TimedAction spawn;
    private bool canSpawn = false;

    void Start()
    {
        spawn = new TimedAction(5f, () => {
            if(isOnline)
                SpawnOnServer();
            else
                SpawnOffline();
        });
    }

    void OnJoinedRoom()
    {
        this.canSpawn = true;
    }

    void SpawnOnServer()
    {
        GameObject enemy = PhotonNetwork.Instantiate(
            this.toSpawn.name,
            this.transform.position,
            Quaternion.identity,
            Constants.ByteGroups.FIRST
        );
    }

    void SpawnOffline()
    {
        GameObject entity = Instantiate(toSpawn);
        Enemy enemy = entity.GetComponent<Enemy>();
        enemy.AssignColor(Constants.EntityColor.GetRandomColorHex());
        entity.transform.position = transform.position;
    }

    void Update()
    {
        if(this.canSpawn)
            spawn.Run(Time.deltaTime);
    }
}

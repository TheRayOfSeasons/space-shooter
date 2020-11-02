using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterEngine;

public class Spawner : MonoBehaviour
{
    public GameObject toSpawn;
    private TimedAction spawn;

    void Start()
    {
        spawn = new TimedAction(5f, () => {
            GameObject entity = Instantiate(toSpawn);
            Enemy enemy = entity.GetComponent<Enemy>();
            enemy.AssignColor(Constants.EntityColor.GetRandomColorHex());
            entity.transform.position = transform.position;
        });
    }

    void Update()
    {
        spawn.Run(Time.deltaTime);
    }
}

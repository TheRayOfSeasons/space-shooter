using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterEngine;

public class WaypointEnemy : Enemy
{
    public List<Transform> destinations;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject front;
    [SerializeField] private float distanceOffset;
    private Waypoint2D waypoints;
    private TimedAction shoot;
    private Shooter shooter;

    void Start()
    {
        List<Vector2> destinationVectors = new List<Vector2>();
        foreach(Transform destination in destinations)
            destinationVectors.Add(destination.position);
        waypoints = new Waypoint2D(this, destinationVectors, distanceOffset);
        waypoints.loop = true;
        waypoints.slerp = true;

        shooter = new Shooter(this.gameObject, bullet);
        shoot = new TimedAction(attackSpeed, () => {
            Vector3 bulletDirection = (front.transform.position - transform.position).normalized;
            GameObject bullet = shooter.Shoot(bulletDirection, 500f);
            bullet.transform.localEulerAngles = this.transform.localEulerAngles;
        });
    }

    void Update()
    {
        waypoints.TravelToNextDestination(Time.deltaTime);
        shoot.Run(Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterEngine;

public class WaypointEnemy : Enemy
{
    public List<Transform> destinations;
    private Waypoint2D waypoints;

    void Start()
    {
        List<Vector2> destinationVectors = new List<Vector2>();
        foreach(Transform destination in destinations)
            destinationVectors.Add(destination.position);
        waypoints = new Waypoint2D(this, destinationVectors, 0.1f);
        waypoints.loop = true;
    }

    void Update()
    {
        waypoints.TravelToNextDestination(Time.deltaTime);
    }
}

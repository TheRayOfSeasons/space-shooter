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
    public PhotonView photonView;
    private Waypoint2D waypoints;
    private TimedAction shoot;
    private Shooter shooter;

    void Start()
    {
        Debug.Log("Initializing Enemy");
        // TODO: Decouple this logic
        GameObject fortress = GameObject.FindGameObjectWithTag(Tags.FORTRESS);
        destinations.Add(fortress.transform);

        List<Vector2> destinationVectors = new List<Vector2>();
        foreach(Transform destination in destinations)
            destinationVectors.Add(destination.position);
        waypoints = new Waypoint2D(this, destinationVectors, distanceOffset);
        waypoints.loop = true;
        waypoints.slerp = true;

        // TODO: Decouple. Must have a different color injection if game is offline
        photonView = this.GetComponent<PhotonView>();
        string hexcode = Constants.EntityColor.GetRandomColorHex();
        photonView.RPC("RPCInjectEnemyColor", PhotonTargets.AllBuffered, hexcode);

        shooter = new Shooter(this.gameObject, bullet);
        shoot = new TimedAction(attackSpeed, () => {
            photonView.RPC("RPCEnemyShoot", PhotonTargets.AllBuffered, null);
        });
    }

    void Update()
    {
        waypoints.TravelToNextDestination(Time.deltaTime);
        shoot.Run(Time.deltaTime);
    }

    [PunRPC]
    public void RPCEnemyShoot()
    {
        Fire();
    }

    void Fire()
    {
        Vector3 bulletDirection = (front.transform.position - transform.position).normalized;
        GameObject bullet;

        try
        {
            bullet = shooter.Shoot(bulletDirection, 500f);
        }
        catch(System.NullReferenceException)
        {
            // Fail silently if called too early. Can happen during photon instantiation.
            return;
        }

        Bullet bulletScript;
        try
        {
            bulletScript = bullet.GetComponent<Bullet>();
        }
        catch(System.NullReferenceException)
        {
            // Fail silently if bullet got destroyed too early.
            return;
        }

        bulletScript.AssignColor(this.hexcode);
        bullet.transform.localEulerAngles = this.transform.localEulerAngles;
    }
}

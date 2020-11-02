using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterEngine;

public class Player : Entity
{
    public float damage;

    [SerializeField] private Camera gameCamera;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject front;
    [SerializeField] private GameObject mainObject;
    [SerializeField] private float xBoundOffset;
    [SerializeField] private float yBoundOffset;

    private ControlSystem controls;
    private Shooter shooter;
    private Vector2 direction;
    private TimedAction shoot;
    public PhotonView photonView;

    void Start()
    {
        controls = new ControlSystem(this.mainObject, this);
        controls.enableColorChange = true;
        shooter = new Shooter(this.mainObject, bullet);
        photonView = this.GetComponent<PhotonView>();

        direction = new Vector2(0, 1f);
        shoot = new TimedAction(attackSpeed, () => {
            photonView.RPC("RPCShoot", PhotonTargets.AllBuffered, null);
        });
    }

    [PunRPC]
    public void RPCShoot()
    {
        Fire();
    }

    void Fire()
    {
        Transform mainTransform = this.mainObject.transform;
        Vector3 bulletDirection = (
            front.transform.position - mainTransform.position).normalized;

        GameObject bullet;
        try
        {
            bullet = shooter.Shoot(bulletDirection, 1000f);
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

        bulletScript.damage = damage;
        bulletScript.AssignColor(this.hexcode);
        bullet.transform.localEulerAngles = mainTransform.localEulerAngles;
    }

    void Update()
    {
        controls.Navigate(this.mainObject.transform, Time.deltaTime, movementSpeed);
        controls.ControlColor();

        if(Input.GetKey(Configs.SHOOT))
            shoot.Run(Time.deltaTime);

        gameCamera.transform.position = new Vector3(
            this.mainObject.transform.position.x,
            this.mainObject.transform.position.y,
            gameCamera.transform.position.z
        );
    }

}

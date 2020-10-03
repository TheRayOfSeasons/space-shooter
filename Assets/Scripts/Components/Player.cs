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
    [SerializeField] private float xBoundOffset;
    [SerializeField] private float yBoundOffset;

    private ControlSystem controls;
    private Shooter shooter;
    private Vector2 direction;
    private TimedAction shoot;

    void Start()
    {
        controls = new ControlSystem(this.gameObject);
        shooter = new Shooter(this.gameObject, bullet);

        direction = new Vector2(0, 1f);
        shoot = new TimedAction(attackSpeed, () => {
            Vector3 bulletDirection = (front.transform.position - transform.position).normalized;
            GameObject bullet = shooter.Shoot(bulletDirection, 1000f);
            bullet.GetComponent<Bullet>().damage = damage;
            bullet.transform.localEulerAngles = this.transform.localEulerAngles;
        });
    }

    void Update()
    {
        controls.Navigate(Time.deltaTime, movementSpeed);

        if(Input.GetKey(Configs.SHOOT))
            shoot.Run(Time.deltaTime);
    }
}

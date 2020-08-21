using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterEngine;

public class BasicEnemy : Enemy
{
    [SerializeField] private GameObject bullet;

    private Rigidbody2D rigidbody;
    private Shooter shooter;
    private TimedAction shoot;

    void Start()
    {
        Vector2 direction = new Vector2(0, -1f);
        float speed = 50f;
        float bulletSpeed = 100f;
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(direction * speed);
        shooter = new Shooter(this.gameObject, bullet);
        shoot = new TimedAction(1f, () => {
            shooter.Shoot(direction, bulletSpeed);
        });
    }

    void Update()
    {
        shoot.Run(Time.deltaTime);
    }
}

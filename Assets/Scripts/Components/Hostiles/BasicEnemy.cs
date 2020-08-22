using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterEngine;

public class BasicEnemy : Enemy
{
    [SerializeField] private GameObject bullet;

    private Shooter shooter;
    private TimedAction shoot;
    private Vector2 bulletDirection = Vector2.down;
    private Vector2 movementDirection = Vector2.up;

    void Start()
    {
        float bulletSpeed = 100f;
        attackSpeed = 1f;
        shooter = new Shooter(this.gameObject, bullet);
        shoot = new TimedAction(attackSpeed, () => {
            shooter.Shoot(bulletDirection, bulletSpeed);
        });
    }

    void Update()
    {
        Move(movementDirection, Time.deltaTime);
        shoot.Run(Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterEngine;

public class Player : Entity
{
    public float speed;
    public float damage;
    public float attackSpeed;

    [SerializeField] private Camera gameCamera;
    [SerializeField] private GameObject bullet;
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
            GameObject bullet = shooter.Shoot(direction, 1000f);
            bullet.GetComponent<Bullet>().damage = damage;
        });
    }

    void Update()
    {
        controls.Navigate(Time.deltaTime, speed);

        if(Input.GetKey(Configs.SHOOT))
            shoot.Run(Time.deltaTime);
    }

    void LateUpdate()
    {
        Vector3 screenBounds = gameCamera.ScreenToWorldPoint(
            new Vector3(
                Screen.width,
                Screen.height,
                gameCamera.transform.position.z
            )
        );
        controls.ClampMovement(screenBounds.x, screenBounds.y, xBoundOffset, yBoundOffset);
    }
}

﻿using System.Collections;
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

    void Start()
    {
        controls = new ControlSystem(this.mainObject);
        shooter = new Shooter(this.mainObject, bullet);

        direction = new Vector2(0, 1f);
        shoot = new TimedAction(attackSpeed, () => {
            Transform mainTransform = this.mainObject.transform;
            Vector3 bulletDirection = (
                front.transform.position - mainTransform.position).normalized;
            GameObject bullet = shooter.Shoot(bulletDirection, 1000f);
            bullet.GetComponent<Bullet>().damage = damage;
            bullet.transform.localEulerAngles = mainTransform.localEulerAngles;
        });
    }

    void Update()
    {
        controls.Navigate(this.mainObject.transform, Time.deltaTime, movementSpeed);

        if(Input.GetKey(Configs.SHOOT))
            shoot.Run(Time.deltaTime);

        gameCamera.transform.position = new Vector3(
            this.mainObject.transform.position.x,
            this.mainObject.transform.position.y,
            gameCamera.transform.position.z
        );
    }
}

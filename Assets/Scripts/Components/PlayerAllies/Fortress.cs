using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterEngine;

public class Fortress : Entity
{
    void Start()
    {
        this.maxHitPoints = 100f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            this.Damage(1f);
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Damage(enemy.currentHitPoints);
        }
    }

    public override void OnDamage()
    {
        Debug.Log("Fortress got hit!");
    }
}

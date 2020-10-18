using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterEngine;

public class Bullet : Projectile
{
    void Start()
    {
        this.timeout = 2f;
    }

    public override void AttachPayload(Entity entity)
    {
        if(this.HasEqualColorWith(entity))
        {
            entity.Damage(this.damage);
            Destroy(gameObject);
        }
    }
}

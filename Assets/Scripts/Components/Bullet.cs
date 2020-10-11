using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterEngine;

public class Bullet : Projectile
{
    public override void AttachPayload(Entity entity)
    {
        if(this.HasEqualColorWith(entity))
        {
            entity.Damage(this.damage);
            Destroy(gameObject);
        }
    }
}

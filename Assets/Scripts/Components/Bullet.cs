using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterEngine;

public class Bullet : Projectile
{
    public override void AttachPayload(Entity entity)
    {
        Debug.Log("Attached Payload");
        Debug.Log($"Shot: {this.spriteRenderer.color} to {entity.color}");
        if(this.spriteRenderer.color == entity.color)
        {
            entity.Damage(this.damage);
            Destroy(gameObject);
        }
    }
}

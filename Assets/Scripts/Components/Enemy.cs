using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterEngine;

public class Enemy : Entity
{
    public override void OnZeroHitPoints()
    {
        GameManager.Instance.AddScore(scoreReward);
        Destroy(gameObject);
    }

    // void OnTriggerEnter2D(Collider2D collider)
    // {
    //     GameObject other = collider.gameObject;
    //     string tag = other.tag;
    //     Tags.Validate(tag);
    //     if(tag == Tags.PLAYERBULLET)
    //         Damage(other.GetComponent<Bullet>().damage);
    // }
}

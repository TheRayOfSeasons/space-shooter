using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterEngine;

public class Obstacle : Entity
{
    public override void OnZeroHitPoints()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterEngine;

public class Obstacle : Entity
{
    [SerializeField] private string color;

    void Start()
    {
        this.AssignColor(Constants.EntityColor.hexCode[this.color]);
    }

    public override void OnZeroHitPoints()
    {
        Destroy(gameObject);
    }
}

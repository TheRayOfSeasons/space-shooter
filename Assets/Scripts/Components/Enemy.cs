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

    [PunRPC]
    public void RPCInjectEnemyColor(string hexcode)
    {
        this.AssignColor(hexcode);
    }
}

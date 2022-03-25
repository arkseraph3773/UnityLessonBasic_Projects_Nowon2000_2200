using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Laser : Tower
{
    public int damage;
    public float attackDelay;
    public float attackTime;
    public float reloadTime;
    public float reloadTimer;
    public override void Update()
    {
        base.Update();

        if (reloadTimer < 0)
        {
            if (target != null)
            {
                Attack();
                reloadTimer = reloadTime;
            }
        }
        else
        {
            reloadTimer -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        target.GetComponent<Enemy>().hp -= damage;
    }
}
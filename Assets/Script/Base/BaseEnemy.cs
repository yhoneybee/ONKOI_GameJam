using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseObject
{
    public int gold;

    public override void Move()
    {
    }

    public override void Die()
    {
        base.Die();
        UIManager.Instance.GoldDrop(this);
    }
}

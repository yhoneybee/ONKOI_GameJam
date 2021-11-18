using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : BaseObject
{
    public override void Move()
    {
    }

    public override void Die()
    {
        base.Die();
        // TODO : 게임 종료
        GameManager.Instance.GameEnd();
        gameObject.SetActive(false);
    }

    public override void Attack()
    {
    }
}

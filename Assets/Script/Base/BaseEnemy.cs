using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : NEED STATE PATTERN APPLY
public class BaseEnemy : BaseObject
{
    public BaseObject Target
    {
        get { return target; }
        set 
        {
            target = value;
        }
    }
    public int gold;
    public float recognitionRange;

    private BaseObject target;

    public override void Move()
    {
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();
        GameManager.Instance.Gold += gold;
        UIManager.Instance.GoldDrop(this);
        UnitManager.Instance.ReturnObject(this);
    }
}

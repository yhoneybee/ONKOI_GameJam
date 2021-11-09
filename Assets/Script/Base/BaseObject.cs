using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stat
{
    public float HP;
    public float maxHP;
    public float AD;
    public float AR;
    public float CP;
    public float MS;
    public int lv;
}

[RequireComponent(typeof(Animator))]
public abstract class BaseObject : MonoBehaviour
{
    public Stat stat;
    public float HP
    {
        get { return stat.HP; }
        set
        {
            stat.HP = value;
            if (stat.HP > stat.maxHP) stat.HP = stat.maxHP;
            else if (stat.HP <= 0)
            {
                stat.HP = 0;
                Die();
            }
        }
    }

    protected SpriteRenderer sr;

    Animator animator;

    protected void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        HP = stat.maxHP;
    }

    protected virtual void Update()
    {
        Move();
        if (HP < 0)
        {
            HP = 30;
            Die();
        }
    }

    public abstract void Move();

    public virtual void Die()
    {
        // TODO : Animator의 Die 에니메이션 플레이
    }
}

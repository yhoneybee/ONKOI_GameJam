using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stat
{
    public float HP;
    public float maxHP;
    public float AD;
    public float AS;
    public float CP;
    public float CD;
    public float MS;
    public float JP;
    public int lv;

    public static Stat operator +(Stat stat1, Stat stat2)
    {
        return new Stat { AD = stat1.AD + stat2.AD, AS = stat1.AS + stat2.AS, CP = stat1.CP + stat2.CP, CD = stat1.CD + stat2.CD, maxHP = stat1.maxHP + stat2.maxHP, MS = stat1.MS + stat2.MS, JP = stat1.JP + stat2.JP };
    }
    public static Stat operator -(Stat stat1, Stat stat2)
    {
        return new Stat { AD = stat1.AD - stat2.AD, AS = stat1.AS - stat2.AS, CP = stat1.CP - stat2.CP, CD = stat1.CD - stat2.CD, maxHP = stat1.maxHP - stat2.maxHP, MS = stat1.MS - stat2.MS, JP = stat1.JP - stat2.JP };
    }
}

public enum eStat
{
    HP,
    AD,
    AS,
    CP,
    CD,
    MS,
    JP,
}

public enum eOperate
{
    ADD,
    SUB,
    MUL,
}

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public abstract class BaseObject : MonoBehaviour
{
    public Stat stat;
    public string Name;
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
    protected Animator animator;
    protected Rigidbody2D rb2d;
    protected BoxCollider2D boxCol2d;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        boxCol2d = GetComponent<BoxCollider2D>();
        HP = stat.maxHP;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    protected virtual void Update()
    {
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
        // animator.GetBool(""); <- 이것으로 에니메이션 끝날때까지 while문으로 돌기
    }
}

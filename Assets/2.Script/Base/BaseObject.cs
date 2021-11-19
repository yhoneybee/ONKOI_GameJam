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

    private static float CheckZero(float value) => value < 0 ? 0 : value;

    public static Stat operator +(Stat stat1, Stat stat2)
    {
        return new Stat
        {
            HP = CheckZero(stat1.HP + stat2.HP),
            AD = CheckZero(stat1.AD + stat2.AD),
            AS = CheckZero(stat1.AS + stat2.AS),
            CP = CheckZero(stat1.CP + stat2.CP),
            CD = CheckZero(stat1.CD + stat2.CD),
            maxHP = CheckZero(stat1.maxHP + stat2.maxHP),
            MS = CheckZero(stat1.MS + stat2.MS),
            JP = CheckZero(stat1.JP + stat2.JP)
        };
    }
    public static Stat operator -(Stat stat1, Stat stat2)
    {
        return new Stat
        {
            HP = CheckZero(stat1.HP - stat2.HP),
            AD = CheckZero(stat1.AD - stat2.AD),
            AS = CheckZero(stat1.AS - stat2.AS),
            CP = CheckZero(stat1.CP - stat2.CP),
            CD = CheckZero(stat1.CD - stat2.CD),
            maxHP = CheckZero(stat1.maxHP - stat2.maxHP),
            MS = CheckZero(stat1.MS - stat2.MS),
            JP = CheckZero(stat1.JP - stat2.JP)
        };
    }
}

public enum eStat
{
    HP,
    MaxHP,
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
        Move();
    }

    public virtual void Move()
    {

    }

    public virtual void Die()
    {
        // TODO : Animator의 Die 에니메이션 플레이
        // animator.GetBool(""); <- 이것으로 에니메이션 끝날때까지 while문으로 돌기
        animator.SetBool("isDie", true);
    }

    public virtual void Attack()
    {
        if (!animator.GetBool("isAttack") && !animator.GetBool("isJump"))
        {
            animator.SetBool("isAttack", true);
            AudioManager.Instance.Play(eMUSIC.Attack);
        }
    }

    public virtual void AttackEnd()
    {
        if (animator.GetBool("isAttack"))
            animator.SetBool("isAttack", false);
    }
}

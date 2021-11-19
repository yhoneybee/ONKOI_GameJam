using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Player : BaseObject
{
    public BaseSkill skill;

    public float LastDir
    {
        get { return lostDir; }
        set
        {
            if (value != 0) lostDir = value;
        }
    }
    public int multiKey = 0;

    private float firstDir = 0;
    private float dir;
    private float lostDir;

    public override void Die()
    {
        base.Die();
        GameManager.Instance.GameEnd();
        // TODO : 게임 종료
    }

    public override void Move()
    {
        base.Move();
        dir = GetAxisRaw(KeyCode.LeftArrow, KeyCode.RightArrow);
        LastDir = dir;
        sr.flipX = LastDir == -1;
        animator.SetBool("isMove", dir != 0);

        if ((dir > 0 && transform.position.x < 17) || (dir < 0 && -17 < transform.position.x))
            transform.Translate(Vector2.right * dir * stat.MS * Time.deltaTime);

        var hit = Physics2D.Raycast(transform.position, Vector2.down, stat.JP / 200, LayerMask.GetMask("Platform"));
        animator.SetBool("isJump", !hit.transform);
        if (hit.transform && Input.GetKeyDown(KeyCode.UpArrow)) rb2d.AddForce(Vector2.up * stat.JP);
    }


    public override void Attack()
    {
        base.Attack();
        print("ATTACK");
        float damage = stat.AD;
        if (UnityEngine.Random.Range(0, 101) < stat.CP)
        {
            damage += stat.CD;
        }
        var hits = Physics2D.RaycastAll(transform.position, Vector2.right * LastDir, 3, LayerMask.GetMask("Enemy"));
        hits.ToList().ForEach(hit => { UIManager.Instance.AttackText(this, hit.transform.GetComponent<BaseObject>(), damage); if (hit.transform) hit.transform.GetComponent<BaseEnemy>().HP -= damage; });
    }

    public override void AttackEnd()
    {
        base.AttackEnd();
        print("ATTACK END");
    }

    public void CheckInputKey()
    {
        if (Input.GetKeyDown(KeyCode.A) && !animator.GetBool("isAttack"))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (skill)
            {
                if (skill.IsReady) // TODO : 여기에서 쿨타임 확인하기
                {
                    print($"{skill.Name} skill was trigger!");
                    skill.Excute();
                    skill.IsReady = false;
                }
                else
                {
                    print($"{skill.Name} skill has cooldown({skill.coolDown})");
                }
            }
            else print("you not have any skill...");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            print("dash");
            if ((LastDir > 0 && transform.position.x < 15) || (LastDir < 0 && -15 < transform.position.x))
                transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right * LastDir * 2, stat.MS);
        }
    }

    protected override void Start()
    {
        base.Start();
        skill.curCoolDown = 0;
    }

    protected override void Update()
    {
        base.Update();
        CheckInputKey();
        skill.Update();
    }

    public float GetAxisRaw(KeyCode left, KeyCode right)
    {
        float ReturnDir = 0;

        if (Time.timeScale == 0) return ReturnDir;

        if (Input.GetKey(left))
        {
            ReturnDir = -1;

            if (Input.GetKeyDown(left))
                ++multiKey;

            if (multiKey == 1)
                firstDir = ReturnDir;
        }

        if (Input.GetKey(right))
        {
            ReturnDir = 1;

            if (Input.GetKeyDown(right))
                ++multiKey;

            if (multiKey == 1)
                firstDir = ReturnDir;
        }

        if (multiKey == 2)
            ReturnDir = -firstDir;

        if (Input.GetKeyUp(left) || Input.GetKeyUp(right))
            --multiKey;

        if (multiKey == 0)
            firstDir = 0;

        return ReturnDir;
    }
}
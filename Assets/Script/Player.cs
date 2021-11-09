using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseObject
{
    public BaseSkill skill;

    int MultiKey = 0;
    float FirstDir = 0;
    float Dir;

    public override void Die()
    {
        base.Die();
        // TODO : 게임 종료
    }

    public override void Move()
    {
        Dir = GetAxisRaw(KeyCode.LeftArrow, KeyCode.RightArrow);
        if (Dir != 0) sr.flipX = Dir == -1;
        transform.Translate(Vector2.right * Dir * stat.MS * Time.deltaTime);
    }

    public void CheckInputKey()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("attack");
            // TODO : 투사체 발사!
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (skill)
            {
                if (skill) // TODO : 여기에서 쿨타임 확인하기
                {
                    print($"{skill.Name} skill has cooldown({skill.coolDown})");
                }
                else
                {
                    print($"{skill.Name} skill was trigger!");
                    skill.Excute();
                }
            }
            else print("you not have any skill...");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            print("dash");
            transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right * Dir * 2, stat.MS);
        }
    }

    protected override void Update()
    {
        base.Update();
        CheckInputKey();
    }

    public float GetAxisRaw(KeyCode left, KeyCode right)
    {
        float ReturnDir = 0;

        if (Time.timeScale == 0) return ReturnDir;

        if (Input.GetKey(left))
        {
            ReturnDir = -1;

            if (Input.GetKeyDown(left))
                ++MultiKey;

            if (MultiKey == 1)
                FirstDir = ReturnDir;
        }

        if (Input.GetKey(right))
        {
            ReturnDir = 1;

            if (Input.GetKeyDown(right))
                ++MultiKey;

            if (MultiKey == 1)
                FirstDir = ReturnDir;
        }

        if (MultiKey == 2)
            ReturnDir = -FirstDir;

        if (Input.GetKeyUp(left) || Input.GetKeyUp(right))
            --MultiKey;

        if (MultiKey == 0)
            FirstDir = 0;

        return ReturnDir;
    }
}
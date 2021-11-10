using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseObject
{
    public BaseSkill skill;
    public List<BaseAbility> abilities;

    public float LostDir
    {
        get { return lostDir; }
        set
        {
            if (value != 0) lostDir = value;
        }
    }

    private int MultiKey = 0;
    private float FirstDir = 0;
    private float Dir;
    private float lostDir;

    public override void Die()
    {
        base.Die();
        // TODO : ���� ����
    }

    public override void Move()
    {
        Dir = GetAxisRaw(KeyCode.LeftArrow, KeyCode.RightArrow);
        LostDir = Dir;
        if (Dir != 0) sr.flipX = Dir == -1;
        transform.Translate(Vector2.right * Dir * stat.MS * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb2d.AddForce(Vector2.up * stat.MS);
        }
    }

    public void CheckInputKey()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("attack");
            // TODO : ����ü �߻�!
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (skill)
            {
                if (skill.IsReady) // TODO : ���⿡�� ��Ÿ�� Ȯ���ϱ�
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
            transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right * LostDir * 2, stat.MS);
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
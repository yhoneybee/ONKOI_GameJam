using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum eFSM
{
    TrackingTarget,
    DetectedNewTarget,
    ChangeNearTarget,
    TargetOnAttackRange,
}

public class BaseEnemy : BaseObject
{
    public BaseObject Target
    {
        get { return target; }
        set
        {
            if (value != target && !Aggro) // 새로운 값을 할당할때, 어드로 상태가 아닐 때
            {
                target = value;
                Aggro = true; // 타겟이 할당되었으므로 5초간 어그로
            }
        }
    }
    public eFSM FSM
    {
        get { return fsm; }
        set
        {
            OnFSMExit(fsm);
            fsm = value;
            OnFSMEnter(fsm);
        }
    }
    public int gold;
    public float recognitionRange;
    public bool Aggro
    {
        get { return aggro; }
        set
        {
            if (value)
            {
                Invoke(nameof(AggroClear), 5);
            }
            aggro = value;
        }
    }

    [SerializeField] private BaseObject target;
    private eFSM fsm;
    [SerializeField] private bool aggro;
    [SerializeField] private bool isAttack;
    private float disPlayer;
    private float disHouse;

    public override void Move()
    {
        base.Move();
        if (Target && !isAttack)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, stat.MS * Time.deltaTime);
            animator.SetBool("isMove", true);
        }
        sr.flipX = transform.position.x - Target.transform.position.x < 0;
    }

    protected override void Start()
    {
        base.Start();
        RefreshDistance();
        target = GameManager.Instance.house;
        FSM = eFSM.TrackingTarget;
    }

    protected override void Update()
    {
        base.Update();
        RefreshDistance();
        FSMUpdate();
    }

    private void RefreshDistance()
    {
        disPlayer = Vector2.Distance(transform.position, GameManager.Instance.player.transform.position);
        disHouse = Vector2.Distance(transform.position, GameManager.Instance.house.transform.position);
    }

    public override void Die()
    {
        base.Die();
        GameManager.Instance.thisGameData.KillCount++;
        GameManager.Instance.Gold += gold;
        RoundManager.Instance.EnemyCount--;
        //UIManager.Instance.GoldDrop(this);
    }

    public void ReturnObj() => UnitManager.Instance.ReturnObject(this);

    public override void Attack()
    {
        base.Attack();
        isAttack = true;
        UIManager.Instance.AttackText(this, Target, stat.AD);
        Invoke(nameof(ResetAttack), stat.AS);
        Target.HP -= stat.AD;
    }

    void ResetAttack()
    {
        isAttack = false;
        FSM = eFSM.TrackingTarget;
    }

    void AggroClear()
    {
        Aggro = false;
        FSM = eFSM.ChangeNearTarget;
    }

    public void OnFSMEnter(eFSM fsm)
    {
        switch (fsm)
        {
            case eFSM.TrackingTarget:
                FSM = eFSM.DetectedNewTarget;
                break;
            case eFSM.DetectedNewTarget:
                break;
            case eFSM.ChangeNearTarget:

                if (disPlayer > disHouse)
                    Target = GameManager.Instance.house;
                else
                    Target = GameManager.Instance.player;

                FSM = eFSM.TrackingTarget;

                break;
            case eFSM.TargetOnAttackRange:
                if (!isAttack)
                {
                    CancelInvoke(nameof(ResetAttack));
                    Attack();
                }

                break;
        }
    }

    public void FSMUpdate()
    {
        switch (fsm)
        {
            case eFSM.TrackingTarget:
                break;
            case eFSM.DetectedNewTarget:
                {
                    var hits = Physics2D.RaycastAll(transform.position + Vector3.right * recognitionRange, Vector2.left, recognitionRange * 2, LayerMask.GetMask("Player") | LayerMask.GetMask("House"));

                    if (!Aggro) // 어그로가 아닐 때
                    {
                        if (hits.Length > 1)
                        {
                            Target = hits.Where(x => x.transform.gameObject.layer != 6).Select(x => x).FirstOrDefault().transform.GetComponent<BaseObject>();
                        }
                        else if (hits.Length > 0)
                        {
                            Target = hits.FirstOrDefault().transform.GetComponent<BaseObject>();
                        }

                        if (hits.Length <= 0) // 감지 되지 않음
                        {
                            isAttack = false;
                            FSM = eFSM.ChangeNearTarget;
                        }
                        else
                        {
                            if (!isAttack)
                            {
                                FSM = eFSM.TargetOnAttackRange;
                            }
                        }
                    }
                }
                break;
            case eFSM.ChangeNearTarget:
                break;
            case eFSM.TargetOnAttackRange:


                break;
        }
    }

    public void OnFSMExit(eFSM fsm)
    {
        switch (fsm)
        {
            case eFSM.TrackingTarget:
                break;
            case eFSM.DetectedNewTarget:
                break;
            case eFSM.ChangeNearTarget:
                break;
            case eFSM.TargetOnAttackRange:
                break;
        }
    }
}

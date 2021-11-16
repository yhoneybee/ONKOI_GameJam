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
                CancelInvoke(nameof(Attack));
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
    private bool isAttack;
    private float disPlayer;
    private float disHouse;

    public override void Move()
    {
        if (Target && !isAttack) transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, stat.MS * Time.deltaTime);
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
        GameManager.Instance.Gold += gold;
        UIManager.Instance.GoldDrop(this);
        UnitManager.Instance.ReturnObject(this);
    }

    public virtual void Attack()
    {
        // TODO : 
        isAttack = false;
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

                print("ATTACK");
                isAttack = true;
                InvokeRepeating(nameof(Attack), 0, 1 / stat.AS);
                FSM = eFSM.TrackingTarget;

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

                    print($"hits : {hits.Length}");

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
                            FSM = eFSM.ChangeNearTarget;
                        }
                        else
                        {
                            FSM = eFSM.TargetOnAttackRange;
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

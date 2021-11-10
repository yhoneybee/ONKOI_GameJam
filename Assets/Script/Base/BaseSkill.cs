using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : ScriptableObject
{
    public string Name;
    public float coolDown;
    public float curCoolDown;
    public int level;
    public bool IsReady
    {
        get { return isReady; }
        set
        {
            if (isReady)
            {
                isReady = value;
                if (!isReady) curCoolDown = coolDown;
            }
        }
    }

    private bool isReady;

    public void Update()
    {
        if (!isReady)
        {
            curCoolDown -= Time.deltaTime;
            if (curCoolDown <= 0)
            {
                curCoolDown = 0;
                isReady = true;
            }
        }
    }

    public abstract void Excute();
}

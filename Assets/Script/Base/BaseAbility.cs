using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public struct AbilityOperate
{
    public eOperate operateType;
    public eStat statType;
    public float value;

    public override string ToString()
    {
        string stat = statType switch
        {
            eStat.HP => "체력이",
            eStat.AD => "공격력이",
            eStat.AS => "공격 속도가",
            eStat.CP => "치명타 확률이",
            eStat.CD => "치명타 피해가",
            eStat.MS => "이동 속도가",
            eStat.JP => "점프력이",
            _ => "",
        };
        string operate = operateType switch
        {
            eOperate.ADD => "만큼 늘어납니다",
            eOperate.SUB => "만큼 줄어듭니다",
            eOperate.MUL => "배가 됩니다",
            _ => "",
        };
        return $"{stat} {value}{operate}";
    }
}

[CreateAssetMenu(menuName = "SCO/Base/Ability", fileName = "Ability")]
public class BaseAbility : ScriptableObject
{
    public BaseObject Owner;
    public List<AbilityOperate> operateList;
    public int level;
    public Stat stat;

    public List<eStat> SortBigStat()
    {
        Dictionary<float, int> dstat = new Dictionary<float, int>()
        {
            { stat.maxHP, 0 },
            { stat.AD, 1 },
            { stat.AS, 2 },
            { stat.CP, 3 },
            { stat.CD, 4 },
            { stat.MS, 5 },
            { stat.JP, 6 },
        };

        return dstat.OrderByDescending(o => o.Key).Select(o => (eStat)o.Value).ToList();
    }

    public float Operate(eOperate operateType, ref float targetValue, float value)
    {
        float returnValue = targetValue;
        switch (operateType)
        {
            case eOperate.ADD:
                targetValue += value;
                break;
            case eOperate.SUB:
                targetValue -= value;
                break;
            case eOperate.MUL:
                targetValue *= value;
                break;
        }
        return targetValue - returnValue;
    }

    public void Equipped(BaseObject owner)
    {
        Owner = owner;
        Unequipped();
        stat = new Stat();
        foreach (var ol in operateList)
        {
            switch (ol.statType)
            {
                case eStat.HP:
                    stat.HP += Operate(ol.operateType, ref Owner.stat.HP, ol.value);
                    break;
                case eStat.AD:
                    stat.AD += Operate(ol.operateType, ref Owner.stat.AD, ol.value);
                    break;
                case eStat.AS:
                    stat.AS += Operate(ol.operateType, ref Owner.stat.AS, ol.value);
                    break;
                case eStat.CP:
                    stat.CP += Operate(ol.operateType, ref Owner.stat.CP, ol.value);
                    break;
                case eStat.CD:
                    stat.CD += Operate(ol.operateType, ref Owner.stat.CD, ol.value);
                    break;
                case eStat.MS:
                    stat.MS += Operate(ol.operateType, ref Owner.stat.MS, ol.value);
                    break;
                case eStat.JP:
                    stat.JP += Operate(ol.operateType, ref Owner.stat.JP, ol.value);
                    break;
            }
        }
    }

    public void Unequipped()
    {
        Owner.stat -= stat;
    }
}

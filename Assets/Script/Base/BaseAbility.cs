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
            eStat.HP => "ü����",
            eStat.AD => "���ݷ���",
            eStat.AS => "���� �ӵ���",
            eStat.CP => "ġ��Ÿ Ȯ����",
            eStat.CD => "ġ��Ÿ ���ذ�",
            eStat.MS => "�̵� �ӵ���",
            eStat.JP => "��������",
            _ => "",
        };
        string operate = operateType switch
        {
            eOperate.ADD => "��ŭ �þ�ϴ�",
            eOperate.SUB => "��ŭ �پ��ϴ�",
            eOperate.MUL => "�谡 �˴ϴ�",
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

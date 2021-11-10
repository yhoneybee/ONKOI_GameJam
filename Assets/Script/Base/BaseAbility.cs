using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AbilityOperate
{
    public eOperate operateType;
    public eStat statType;
    public float value;
}

[CreateAssetMenu(menuName = "SCO/Base/Ability", fileName = "Ability")]
public class BaseAbility : ScriptableObject
{
    public List<AbilityOperate> operateList;
    public int level;
    public Stat stat;

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
            case eOperate.DIV:
                targetValue /= value;
                break;
        }
        return targetValue - returnValue;
    }

    public Stat Equipped(Stat playerStat)
    {
        stat = new Stat();
        foreach (var ol in operateList)
        {
            switch (ol.statType)
            {
                case eStat.HP:
                    stat.HP += Operate(ol.operateType, ref playerStat.HP, ol.value);
                    break;
                case eStat.AD:
                    stat.AD += Operate(ol.operateType, ref playerStat.AD, ol.value);
                    break;
                case eStat.AS:
                    stat.AS += Operate(ol.operateType, ref playerStat.AS, ol.value);
                    break;
                case eStat.CP:
                    stat.CP += Operate(ol.operateType, ref playerStat.CP, ol.value);
                    break;
                case eStat.CD:
                    stat.CD += Operate(ol.operateType, ref playerStat.CD, ol.value);
                    break;
                case eStat.MS:
                    stat.MS += Operate(ol.operateType, ref playerStat.MS, ol.value);
                    break;
                case eStat.JP:
                    stat.JP += Operate(ol.operateType, ref playerStat.JP, ol.value);
                    break;
            }
        }
        return stat;
    }

    public void Unequipped(ref Stat playerStat)
    {
        playerStat -= stat;
    }
}

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
    public float[] levelUpAddtionValues;

    public override string ToString()
    {
        string stat = statType switch
        {
            eStat.HP => "체력이",
            eStat.MaxHP => "최대 체력이",
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
            _ => "",
        };
        return $"{stat},{operate}";
    }
}

[CreateAssetMenu(menuName = "SCO/Base/Ability", fileName = "Ability")]
public class BaseAbility : ScriptableObject
{
    public List<AbilityOperate> operateList;
    public string Name;
    public int level;
    public bool epicRank;

    public override string ToString()
    {
        string result = $"[ {Name}(lv.{level}) ]\n";
        foreach (var operate in operateList)
        {
            var statAndOperate = operate.ToString().Split(',');
            result += $"{statAndOperate[0]} {operate.levelUpAddtionValues[level]}{statAndOperate[1]}\n";
        }
        return result;
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
        }
        return targetValue - returnValue;
    }

    public void Equipped()
    {
        //GameManager.Instance.thisGameData.AddAbility(this);
        var player = GameManager.Instance.player;
        foreach (var ol in operateList)
        {
            switch (ol.statType)
            {
                case eStat.HP:
                    Operate(ol.operateType, ref player.stat.HP, ol.levelUpAddtionValues[level]);
                    break;
                case eStat.MaxHP:
                    Operate(ol.operateType, ref player.stat.maxHP, ol.levelUpAddtionValues[level]);
                    break;
                case eStat.AD:
                    Operate(ol.operateType, ref player.stat.AD, ol.levelUpAddtionValues[level]);
                    break;
                case eStat.AS:
                    Operate(ol.operateType, ref player.stat.AS, ol.levelUpAddtionValues[level]);
                    break;
                case eStat.CP:
                    Operate(ol.operateType, ref player.stat.CP, ol.levelUpAddtionValues[level]);
                    break;
                case eStat.CD:
                    Operate(ol.operateType, ref player.stat.CD, ol.levelUpAddtionValues[level]);
                    break;
                case eStat.MS:
                    Operate(ol.operateType, ref player.stat.MS, ol.levelUpAddtionValues[level]);
                    break;
                case eStat.JP:
                    Operate(ol.operateType, ref player.stat.JP, ol.levelUpAddtionValues[level]);
                    break;
            }
        }
        level++;
    }

    public void LevelUp()
    {
        level++;
    }
}

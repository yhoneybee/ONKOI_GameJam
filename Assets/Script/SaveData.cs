using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class SaveData
{
    public int killCount;
    public int clearRound;
    // TODO : 능력을 추가해야함
    public List<BaseAbility> Abilities = new List<BaseAbility>();

    public void AddAbility(BaseAbility ability)
    {
        Abilities.Add(ability);
        Abilities = Abilities.OrderByDescending(x => x.level).Select(x => x).ToList();
    }
    public void RemoveAbility(BaseAbility ability)
    {
        if (Abilities.Contains(ability))
            Abilities.Remove(ability);
    }
}

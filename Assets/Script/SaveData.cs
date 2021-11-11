using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int killCount;
    public int clearRound;
    // TODO : 능력을 추가해야함
    public List<BaseAbility> Abilities;
}

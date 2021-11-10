using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SCO/Data/SaveData", fileName = "SaveData")]
[System.Serializable]
public class SaveData : ScriptableObject
{
    public int killCount;
    public int clearRound;
    // TODO : 능력을 추가해야함
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public string Name;
    public float coolDown;
    public int level;


    public abstract void Excute();
}

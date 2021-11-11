using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }

    public List<BaseAbility> Abilities;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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

    public List<BaseAbility> ChoiceAbility(bool epic)
    {
        List<BaseAbility> list = new List<BaseAbility>
        {
            Abilities[UnityEngine.Random.Range(0, Abilities.Count)],
            Abilities[UnityEngine.Random.Range(0, Abilities.Count)],
            Abilities[UnityEngine.Random.Range(0, Abilities.Count)],
        };
        if (epic)
        {
            var epicAbilities = Abilities.Where(x => x.epicRank == true).ToList();
            list = new List<BaseAbility>
            {
                epicAbilities[UnityEngine.Random.Range(0, epicAbilities.Count)],
                epicAbilities[UnityEngine.Random.Range(0, epicAbilities.Count)],
                epicAbilities[UnityEngine.Random.Range(0, epicAbilities.Count)],
            };
        }

        return list;
    }
}

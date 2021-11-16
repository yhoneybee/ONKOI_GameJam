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

    public List<BaseAbility> ChoiceAbility() =>
        new List<BaseAbility>
        {
                Abilities[Random.Range(0, Abilities.Count)],
                Abilities[Random.Range(0, Abilities.Count)],
                Abilities[Random.Range(0, Abilities.Count)],
        };
}

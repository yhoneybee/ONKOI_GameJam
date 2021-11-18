using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityChoiceLinker : MonoBehaviour
{
    public BaseAbility Ability
    {
        get { return ability; }
        set
        {
            ability = value;
            if (ability)
            {
                info.text = ability.ToString();
            }
        }
    }
    public Button choice;
    public TextMeshProUGUI info;

    private BaseAbility ability;

    private void Start()
    {
        choice.onClick.RemoveAllListeners();
        choice.onClick.AddListener(OnClick);
    }

    private void OnEnable()
    {
        choice.onClick.RemoveAllListeners();
        choice.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        ability.Equipped();
        UIManager.Instance.ActiveAbilityChoice(false);
        GameManager.Instance.player.HP = int.MaxValue;
    }
}

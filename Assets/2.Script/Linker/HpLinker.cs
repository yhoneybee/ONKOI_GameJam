using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpLinker : MonoBehaviour
{
    public BaseObject owner;
    public Slider sliderHP;
    public TextMeshProUGUI txtHP;

    private void Update()
    {
        sliderHP.value = owner.HP;
        sliderHP.maxValue = owner.stat.maxHP;
        txtHP.text = $"{owner.HP} / {owner.stat.maxHP}";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI txtRoundCount;
    public TextMeshProUGUI txtGold;
    public TextMeshProUGUI txtKillCount;
    public RectTransform rtrnCanvas;
    public GoldDropLinker goldDropPrefab;
    public List<AbilityChoiceLinker> linkAbilityChoices;
    public List<DataCardLinker> linkDataCards;
    public TextMeshProUGUI txtChoice;
    public Image imgFade;
    public Image imgGameData;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        txtGold.text = "";
    }

    public void GoldDrop(BaseEnemy be)
    {
        var go = Instantiate(goldDropPrefab, rtrnCanvas, false);
        go.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(be.transform.position) - new Vector3(1920 / 2, 1080 / 2);
        go.txtGoldCount.text = $"+{be.gold:#,0}";
    }

    public void AttackText(BaseObject bo, BaseObject target, float damage, bool wasDamage = true)
    {
        var go = Instantiate(goldDropPrefab, rtrnCanvas, false);
        go.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(target.transform.position) - new Vector3(1920 / 2, 1080 / 2);
        go.rtrnIcon.gameObject.SetActive(false);
        go.txtGoldCount.color = wasDamage ? Color.red : Color.green;
        go.txtGoldCount.text = $"{damage}";
    }

    public void ActiveAbilityChoice(bool active)
    {
        int posX = -600;
        foreach (var linker in linkAbilityChoices)
        {
            linker.gameObject.SetActive(active);
            linker.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, 0);
            posX += 600;
            linker.choice.enabled = false;
        }
        txtChoice.gameObject.SetActive(active);
        txtChoice.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -540);

        Time.timeScale = 1;
    }

    public void ShowAbilityChoice(bool epic)
    {
        ActiveAbilityChoice(true);
        var abilities = AbilityManager.Instance.ChoiceAbility(epic);

        for (int i = 0; i < 3; i++) linkAbilityChoices[i].Ability = abilities[i];

        DOTween.Sequence()
            .Insert(0, linkAbilityChoices[0].GetComponent<RectTransform>().DOAnchorPosY(-150, 1))
            .Insert(0, linkAbilityChoices[1].GetComponent<RectTransform>().DOAnchorPosY(-150, 1))
            .Insert(0, linkAbilityChoices[2].GetComponent<RectTransform>().DOAnchorPosY(-150, 1))
            .Insert(0, txtChoice.GetComponent<RectTransform>().DOAnchorPosY(-200, 1))
            .onComplete = () =>
             {
                 Time.timeScale = 0;
                 foreach (var linker in linkAbilityChoices)
                     linker.choice.enabled = true;
             };
    }
}

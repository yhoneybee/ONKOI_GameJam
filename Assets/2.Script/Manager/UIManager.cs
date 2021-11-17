using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI txtRoundCount;
    public TextMeshProUGUI txtGold;
    public RectTransform rtrnCanvas;
    public GoldDropLinker goldDropPrefab;
    public List<AbilityChoiceLinker> linkAbilityChoice;
    public TextMeshProUGUI txtChoice;
    public Image imgFade;

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

    public void HouseEnter()
    {
        imgFade.DOFade(1, 1);
    }

    public void HouseExit()
    {
        imgFade.DOFade(0, 1);
    }

    public void ActiveAbilityChoice(bool active)
    {
        int posX = -600;
        foreach (var linker in linkAbilityChoice)
        {
            linker.gameObject.SetActive(active);
            linker.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, 0);
            posX += 600;
        }
        txtChoice.gameObject.SetActive(active);
        txtChoice.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -540);

        Time.timeScale = 1;
    }

    public void ShowAbilityChoice()
    {
        ActiveAbilityChoice(true);
        var abilities = AbilityManager.Instance.ChoiceAbility();
        for (int i = 0; i < 3; i++)
        {
            linkAbilityChoice[i].Ability = abilities[i];
        }
        DOTween.Sequence()
            .Insert(0, linkAbilityChoice[0].GetComponent<RectTransform>().DOAnchorPosY(-150, 1))
            .Insert(0, linkAbilityChoice[1].GetComponent<RectTransform>().DOAnchorPosY(-150, 1))
            .Insert(0, linkAbilityChoice[2].GetComponent<RectTransform>().DOAnchorPosY(-150, 1))
            .Insert(0, txtChoice.GetComponent<RectTransform>().DOAnchorPosY(-200, 1))
            .onComplete = () =>
             {
                 Time.timeScale = 0;
             };
    }
}

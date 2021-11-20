using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public BaseObject player;
    public House house;
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            if (!UIManager.Instance.txtGold) Debug.LogError($"{UIManager.Instance.name}의 txtGold변수가 비어있습니다");
            else UIManager.Instance.txtGold.text = $"{gold:#,0} Gold";
        }
    }

    private int gold;

    public System.Tuple<float, int, int, int, int, int, int> temp;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Gold = 0;
        GameStart();
    }

    private void Update()
    {
    }

    public void GameStart()
    {
        print($"MaxStage : {PlayerPrefs.GetInt("maxStage")}");
    }

    public void GameEnd()
    {
        AudioManager.Instance.Play(eMUSIC.GameOver);

        int maxStage = PlayerPrefs.GetInt("maxStage", 0);

        if (maxStage < RoundManager.Instance.RoundCount)
            PlayerPrefs.SetInt("maxStage", RoundManager.Instance.RoundCount);

        print($"MaxStage : {PlayerPrefs.GetInt("maxStage")}");

        ShowGameData();

        player.gameObject.SetActive(false);
        house.gameObject.SetActive(false);
    }

    public void ShowGameData()
    {
        DOTween.Sequence()
            .Insert(0, UIManager.Instance.imgFade.DOFade(1, 1))
            .Insert(1, UIManager.Instance.imgGameData.DOFade(1, 1))
            .onComplete = () =>
            {
                UIManager.Instance.linkDataCards[0].Show("KillCount", $"{RoundManager.Instance.KillCount}");
                UIManager.Instance.linkDataCards[1].Show("Round", $"{RoundManager.Instance.RoundCount}");
                UIManager.Instance.linkDataCards[2].Show("High Level", $"{AbilityManager.Instance.GetHighLevel().Name}\n(lv.{AbilityManager.Instance.GetHighLevel().level})", 60);

                foreach (var ability in AbilityManager.Instance.Abilities)
                {
                    ability.level = 0;
                }
            };
        DOTween.Sequence()
            .Insert(2, UIManager.Instance.linkDataCards[0].GetComponent<RectTransform>().DOLocalMoveY(0, 1))
            .Insert(2, UIManager.Instance.linkDataCards[1].GetComponent<RectTransform>().DOLocalMoveY(0, 1))
            .Insert(2, UIManager.Instance.linkDataCards[2].GetComponent<RectTransform>().DOLocalMoveY(0, 1))
            .onComplete = () =>
            {
                UIManager.Instance.btnConfirm.targetGraphic.raycastTarget = UIManager.Instance.btnConfirm.enabled = true;
            };
    }
}

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
    public SaveData thisGameData;

    public List<SaveData> gameLogData = new List<SaveData>();
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

    private void LoadAndSaveLogData()
    {
        var loads = SaveManager.Load<(int, int)>("GameLogData");
        if (loads != null)
        {
            foreach (var load in loads)
                gameLogData.Add(new SaveData { clearRound = load.Item1, KillCount = load.Item2 });
        }

        if (gameLogData.Count > 10)
            gameLogData.RemoveRange(10, gameLogData.Count - 10);
        SaveManager.Save(gameLogData.Select(x => (x.clearRound, x.KillCount)), "GameLogData");
    }

    private void Update()
    {
    }

    public void GameStart()
    {
        thisGameData = new SaveData();
        print($"MaxStage : {PlayerPrefs.GetInt("maxStage")}");
        LoadAndSaveLogData();
    }

    public void GameEnd()
    {
        thisGameData.clearRound = RoundManager.Instance.RoundCount;

        int maxStage = PlayerPrefs.GetInt("maxStage", 0);

        if (maxStage < thisGameData.clearRound)
            PlayerPrefs.SetInt("maxStage", thisGameData.clearRound);

        print($"MaxStage : {PlayerPrefs.GetInt("maxStage")}");

        gameLogData.Add(thisGameData);
        LoadAndSaveLogData();
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
                UIManager.Instance.linkDataCards[0].Show("KillCount", $"{thisGameData.KillCount}");
                UIManager.Instance.linkDataCards[1].Show("Round", $"{thisGameData.clearRound}");
                UIManager.Instance.linkDataCards[2].Show("High Level", $"NONE");
            };
        DOTween.Sequence()
            .Insert(2, UIManager.Instance.linkDataCards[0].GetComponent<RectTransform>().DOLocalMoveY(0, 1))
            .Insert(2, UIManager.Instance.linkDataCards[1].GetComponent<RectTransform>().DOLocalMoveY(0, 1))
            .Insert(2, UIManager.Instance.linkDataCards[2].GetComponent<RectTransform>().DOLocalMoveY(0, 1));
    }
}

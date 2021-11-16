using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Gold = 0;
        GameStart();
        LoadAndSaveLogData();
    }

    private void LoadAndSaveLogData()
    {
        SaveManager.Load(ref gameLogData, "GameLogDatas");
        if (gameLogData.Count > 10)
        {
            gameLogData.RemoveRange(10, gameLogData.Count - 10);
            SaveManager.Save(gameLogData, "GameLogDatas");
        }
    }

    private void Update()
    {
    }

    public void GameStart()
    {
        thisGameData = new SaveData();
        gameLogData.Add(thisGameData);
    }
}

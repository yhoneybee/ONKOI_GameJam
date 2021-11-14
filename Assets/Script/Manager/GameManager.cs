using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<SaveData> GameLogData;
    public BaseObject player;
    public House House;
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
        LoadAndSaveLogData();
    }

    private void LoadAndSaveLogData()
    {
        SaveManager.Load(ref GameLogData, "GameLogDatas");
        if (GameLogData.Count > 10)
        {
            GameLogData.RemoveRange(10, GameLogData.Count - 10);
            SaveManager.Save(GameLogData, "GameLogDatas");
        }
    }

    private void Update()
    {
    }
}

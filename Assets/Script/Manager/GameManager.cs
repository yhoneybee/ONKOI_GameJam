using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<SaveData> GameLogData;
    public BaseObject player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
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

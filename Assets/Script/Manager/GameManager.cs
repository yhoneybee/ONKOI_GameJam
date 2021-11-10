using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<SaveData> SaveDatas
    {
        get 
        {
            if (saveDatas.Count > 10)
            {
                saveDatas.RemoveAt(saveDatas.Count - 1);
                SaveManager.Save(saveDatas, "SaveData");
            }
            return saveDatas;
        }
        set { saveDatas = value; }
    }

    private List<SaveData> saveDatas;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance { get; private set; }

    public List<Transform> spawnPoints;
    public int RoundCount
    {
        get { return roundCount; }
        set 
        { 
            roundCount = value;
            UIManager.Instance.txtRoundCount.text = $"Round : {roundCount}";
        }
    }

    private int roundCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RoundCount = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RoundCount++;
        }
    }

    public void Spawn()
    {

    }
}

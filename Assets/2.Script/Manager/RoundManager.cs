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
            UIManager.Instance.ShowAbilityChoice(roundCount % 3 == 0);
        }
    }
    public int EnemyCount
    {
        get { return enemyCount; }
        set
        {
            enemyCount = value;
            if (enemyCount <= 0)
            {
                CancelInvoke(nameof(Spawn));
                RoundCount++;
                enemyCount = 5 * RoundCount - (2 * (RoundCount - 1));
                InvokeRepeating(nameof(Spawn), 3, 3);
            }
        }
    }

    private int roundCount;
    private int enemyCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EnemyCount = 0;
    }

    private void Update()
    {
    }

    public void Spawn()
    {
        var enemy = UnitManager.Instance.GetRandomEnemy(spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position);
        enemy.stat.HP = enemy.stat.HP = 40 + (RoundCount * 10);
    }
}

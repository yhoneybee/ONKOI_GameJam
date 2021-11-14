using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI txtRoundCount;
    public TextMeshProUGUI txtGold;
    public RectTransform rtrnCanvas;
    public GoldDropLinker goldDropPrefab;

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
}

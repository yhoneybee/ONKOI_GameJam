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
    public List<AbilityChoiceLinker> linkAbilityChoice;
    public TextMeshProUGUI txtChoice;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        txtGold.text = "";
        ShowAbilityChoice();
    }

    public void GoldDrop(BaseEnemy be)
    {
        var go = Instantiate(goldDropPrefab, rtrnCanvas, false);
        go.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(be.transform.position) - new Vector3(1920 / 2, 1080 / 2);
        go.txtGoldCount.text = $"+{be.gold:#,0}";
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

        if (active)
            Time.timeScale = 0;
        else
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
    }
}

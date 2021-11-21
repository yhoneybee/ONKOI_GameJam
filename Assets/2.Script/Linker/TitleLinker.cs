using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class TitleLinker : MonoBehaviour
{
    public Button btnStart;
    public TextMeshProUGUI txtStart;
    public Button btnQuit;
    public TextMeshProUGUI txtQuit;
    public TextMeshProUGUI txtHighStage;
    public Image imgFade;

    private int count = 3;

    public void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
    }

    private void Start()
    {
        count = 3;

        if (txtHighStage)
            txtHighStage.text = $"High Stage : {PlayerPrefs.GetInt("maxStage")}";

        if (btnStart)
            btnStart.onClick.AddListener(() =>
            {
                AudioManager.Instance.Play(eMUSIC.Button);
                SceneChange("Ingame");
            });

        if (btnQuit)
            btnQuit.onClick.AddListener(() =>
            {
                AudioManager.Instance.Play(eMUSIC.Button);
                switch (count)
                {
                    case 3:
                        txtQuit.DOText("ㄹㅇ 왜 킴?", 1);
                        break;
                    case 2:
                        txtQuit.DOText("아니 ㄹㅇ 왜 킴..?", 1);
                        break;
                    case 1:
                        txtQuit.DOText("응애..?", 1);
                        break;
                    case 0:
                        Application.Quit();
                        break;
                }
                count--;
            });
    }
}

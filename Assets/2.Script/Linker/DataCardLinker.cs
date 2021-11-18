using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DataCardLinker : MonoBehaviour
{
    public TextMeshProUGUI txtTitle;
    public TextMeshProUGUI txtCount;

    public void Show(string title, string count)
    {
        DOTween.Sequence()
            .Insert(0, GetComponent<Image>().DOFade(1, 1))
            .Insert(1, txtTitle.DOFade(1, 1))
            .Insert(1.5f, txtTitle.DOFontSize(55, 1))
            .Insert(1, txtCount.DOFade(1, 1))
            .Insert(1.5f, txtCount.DOFontSize(100, 1))
            .onComplete = () =>
            {
                txtTitle.DOText(title, 1);
                txtCount.DOText(count, 1);
            };
    }
}

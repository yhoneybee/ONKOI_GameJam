using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GoldDropLinker : MonoBehaviour
{
    public RectTransform rtrnIcon;
    public TextMeshProUGUI txtGoldCount;

    private RectTransform rtrn;
    private RectTransform rtrnGoldCount;

    private void Start()
    {
        rtrn = GetComponent<RectTransform>();
        rtrnGoldCount = txtGoldCount.GetComponent<RectTransform>();

        DOTween.Sequence()
            .Append(
                rtrn.DOLocalMoveY(100, 1)
                .SetRelative()
                .SetLoops(2, LoopType.Yoyo)
                )
            .Insert(
                1,
                txtGoldCount.DOFade(0, 1.5f)
            )
            .Insert(
                1,
                rtrnIcon.GetComponent<Image>().DOFade(0, 1.75f)
            )
            .Insert(
                2,
                rtrn.DOLocalMoveY(-100, 0.75f)
                .SetRelative()
            )
            .onComplete = () => 
            {
                Destroy(gameObject);
            };
    }

    private void Update()
    {
        rtrnIcon.sizeDelta = Vector2.one * rtrn.sizeDelta.y;
        rtrnGoldCount.sizeDelta = new Vector2(rtrn.sizeDelta.x - rtrn.sizeDelta.y, rtrn.sizeDelta.y);
    }
}

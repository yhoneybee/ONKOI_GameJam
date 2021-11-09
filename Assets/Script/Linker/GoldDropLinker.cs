using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    }

    private void Update()
    {
        rtrnIcon.sizeDelta = Vector2.one * rtrn.sizeDelta.y;
        rtrnGoldCount.sizeDelta = new Vector2(rtrn.sizeDelta.x - rtrn.sizeDelta.y, rtrn.sizeDelta.y);
    }
}

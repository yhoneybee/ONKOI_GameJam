using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public RectTransform rtrnCanvas;
    public GoldDropLinker goldDropPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void GoldDrop(BaseEnemy be)
    {
        StartCoroutine(EGoldDrop(be));
    }
    IEnumerator EGoldDrop(BaseEnemy be)
    {
        // TODO : be의 위치에서 위로 올라갔다가 내려오고, 원래 위치보다 내려가면 사라짐
        var go = Instantiate(goldDropPrefab, rtrnCanvas, false);
        go.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(be.transform.position) - new Vector3(1920 / 2, 1080 / 2);
        go.txtGoldCount.text = $"+{be.gold}";
        Destroy(go, 5);
        // TODO : width : 50 -> 230 ( Lerp )

        yield return null;
    }
}

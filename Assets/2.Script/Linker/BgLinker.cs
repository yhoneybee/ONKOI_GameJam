using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLinker : MonoBehaviour
{
    public GameObject[] goBg;

    readonly float resetXValue = 30;

    void Update()
    {
        for (int i = 0; i < goBg.Length; i++)
        {
            goBg[i].transform.Translate(Vector3.left * 1 * Time.deltaTime);
            if (goBg[i].transform.position.x <= -35.8f)
                goBg[i].transform.position = new Vector3(resetXValue, goBg[i].transform.position.y);
        }
    }
}

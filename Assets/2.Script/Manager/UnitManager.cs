using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }
    public List<BaseEnemy> originList;
    public Dictionary<string, List<BaseEnemy>> dicEnemy;
    public RectTransform rtrnEnemyParent;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dicEnemy = new Dictionary<string, List<BaseEnemy>>();
    }

    public BaseEnemy GetObject(string name, Vector2 pos)
    {
        BaseEnemy obj;
        if (originList.Count == 0)
        {
            Debug.LogError($"{name}안에 있는 UnitManager의 originList에 비어있습니다");
            return null;
        }
        else
        {
            if (!dicEnemy.ContainsKey(name)) dicEnemy.Add(name, new List<BaseEnemy>());

            if (dicEnemy[name].Count > 0)
            {
                dicEnemy[name].RemoveAll(o => o == null);
                obj = dicEnemy[name][0];
                dicEnemy[name].RemoveAt(0);
            }
            else
            {
                obj = Instantiate(originList.Where(o => o.name == name).FirstOrDefault());
            }
        }

        obj.transform.position = pos;
        obj.gameObject.SetActive(true);

        return obj;
    }

    public void ReturnObject(BaseEnemy be)
    {
        if (!dicEnemy.ContainsKey(be.name)) dicEnemy.Add(be.name, new List<BaseEnemy>());

        dicEnemy[be.name].Add(be);
        be.gameObject.SetActive(false);
        be.transform.position = Vector3.zero;
    }
}

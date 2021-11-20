using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject followObject;
    public float followSpeed;

    private void Update()
    {
        float x = Mathf.Lerp(transform.position.x, followObject.transform.position.x, Time.deltaTime * followSpeed);
        if (-8.5f < x && x < 8.5f)
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}

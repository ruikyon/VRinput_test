using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    private bool flag;
    private Vector3 basePos;

    // Start is called before the first frame update
    void Start()
    {
        flag = false;
        basePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!flag) return;

        transform.RotateAround(basePos, Vector3.up, 180 * Time.deltaTime);
    }

    public void Activate()
    {
        GetComponent<Renderer>().material.color = Color.red;
        GetComponent<Collider>().enabled = true;
        transform.position += Vector3.forward * 1;
        flag = true;

    }
}

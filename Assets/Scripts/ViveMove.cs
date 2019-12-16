using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveMove : MonoBehaviour
{
    private Rigidbody rb;
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        flag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!flag) return;
        var move = InputVive.left.trackpad.Value;
        rb.velocity = move ? Vector3.forward : Vector3.zero;

        var dir = InputVive.left.horizontal.Value;
        
    }
}

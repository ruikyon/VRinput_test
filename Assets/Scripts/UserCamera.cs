﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCamera : MonoBehaviour // HMDのカメラを制御
{
    [SerializeField]
    public Transform mycamera, leftHand, rightHand;
    private readonly Vector3 head = new Vector3(0, 1.35f, 0);
    private readonly float lengthHandToHand = 1.14f;
    //vector: from player(=mycamera) to camerarig
    public Vector3 offset = Vector3.zero;
    private float bodyScale = 0.5f;
    public static float length = 1.14f;
    private bool first = true;

    private void Start()
    {
        LoadWidth();
    }

    private void Update()
    {
        if(first)
        {
             HeightReset();
             first = false;
        }
        var temp = mycamera.position - mycamera.forward * 0.075f - Player.instance.transform.position;
        temp.y = 0;
        Player.instance.transform.position += temp;
        transform.position -= temp;
    }

    public void HeightReset()
    {
        var temp = Player.instance.transform.position.y + 1.35f - mycamera.position.y;
        transform.position += Vector3.up * temp;
    }

    public void WidthReset()
    {
        var newlength = Vector3.Distance(leftHand.position, rightHand.position);
        if (newlength > 0)
            length = newlength;
        transform.localScale *= lengthHandToHand / length;
        Debug.Log("length: " + newlength);
    }

    public void LoadWidth()
    {
        transform.localScale *= lengthHandToHand / length;
    }
}

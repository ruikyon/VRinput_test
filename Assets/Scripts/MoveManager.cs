using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField]
    private float mvSpeed, modelWidth;
    [SerializeField]
    private Transform player, hmd_rig,  box;
    private Transform hmd_cam;
    private Vector3 ang;
    private Vector3 prePosi;

    private readonly float minAng = 10, maxAng = 30;

    public bool moving = true;

    // Start is called before the first frame update
    void Start()
    {
        if (player.GetComponent<Rigidbody>() == null) Debug.LogError("player does not have rigidbody");
        foreach (Transform child in hmd_rig)        
            if (child.GetComponent<Camera>() != null)
                hmd_cam = child;

        UDPReceiver.RotCallBack = RotAction;
        UDPReceiver.UDPStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving) return;

        box.eulerAngles = ang;//回転情報のチェック用

        //Rig移動
        hmd_rig.position += player.position - prePosi;

        //PC移動
        var temp = hmd_cam.position - hmd_cam.forward * 0.075f - player.position;
        temp.y = 0;
        player.position += temp;

        //向き制御
        player.eulerAngles = ang.y * Vector3.up;

        //速度制御
        var dx = AngToSpeed(ang.z);
        var dz = AngToSpeed(ang.x);
        var deg = (dx == 0) ? 0 : Mathf.Atan(dz / dx) * 360 / (2 * Mathf.PI);
        player.GetComponent<Rigidbody>().velocity = mvSpeed * (Quaternion.AngleAxis(deg, Vector3.up) * player.forward);

        prePosi = player.position;
    }

    //デバイスの傾きをモデルの速さに変換
    private float AngToSpeed(float deg)
    {
        if (deg < -maxAng) return -1;
        else if (deg < -minAng) return (deg + minAng) / (maxAng - minAng);
        else if (deg < minAng) return 0;
        else if (deg < maxAng) return (deg - minAng) / (maxAng - minAng);
        else return 1;
    }

    //回転によってカメラ方向を変更
    private void RotAction(float xx, float yy, float zz, float ww)
    {
        ang = (new Quaternion(-xx, -zz, -yy, ww) * Quaternion.Euler(90f, 0, 0)).eulerAngles;
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

    public void ResetRot()
    {
        var dir = userCamera.mycamera.transform.forward;
        dir.y = 0;
        transform.forward = dir;
        dirOffset = Controller.AccelData().x - transform.eulerAngles.y;
    }
}

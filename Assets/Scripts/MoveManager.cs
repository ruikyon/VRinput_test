using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class MoveManager : MonoBehaviour
{
    [SerializeField]
    private float mvSpeed, modelWidth;
    [SerializeField]
    private Transform player, hmd_rig, box;
    private Transform hmd_cam;
    private Vector3 ang;
    private Vector3 prePosi;
    private float dirOffset = 0;
    private Quaternion playerRot;

    private readonly float minAng = 20, maxAng = 40;

    public bool moving = true, vive;

    private readonly SteamVR_Action_Boolean trackBottun = SteamVR_Actions._default.Teleport;
    private readonly SteamVR_Action_Vector2 track = SteamVR_Actions._default.trackposi;

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

    private void Update()
    {
        //Debug.Log("player vel: "+ player.GetComponent<Rigidbody>().velocity);
        if (Input.GetKeyDown(KeyCode.R))
        {
            //正面リセット
            var forward = hmd_cam.forward;
            forward.y = 0;
            player.forward = forward;
            dirOffset = ang.y - player.eulerAngles.y;

            var boxDir = box.forward;
            boxDir.y = 0;
            playerRot.SetFromToRotation(boxDir, forward);
        }
    }


    private void FixedUpdate()
    {
        var moveFlag = trackBottun.GetState(SteamVR_Input_Sources.LeftHand);
        //if (moveFlag)
        //{
        //    ang.x = -90;
        //}
        //else
        //{
        //    ang.x = 0;
        //}

        if (vive) 
        {
            var dirVal = track.GetAxis(SteamVR_Input_Sources.RightHand).x;

            //回転処理
            //プレイヤーだけでなくてカメラも回す必要あり
            if (trackBottun.GetState(SteamVR_Input_Sources.RightHand))
            {
                ang.y += (Mathf.Abs(dirVal) < 0.5f) ? 0 : Mathf.Sign(dirVal) * 0.8f;
                var pivot = hmd_rig.parent;
                var pre = hmd_rig.position;
                pivot.position = player.position;
                hmd_rig.position = pre;
                pivot.Rotate(0, (Mathf.Abs(dirVal) < 0.5f) ? 0 : Mathf.Sign(dirVal) * 0.8f, 0);
            }
        }

        if (!moving)
        {
            Debug.Log("disconnected");
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            return;
        }

        box.eulerAngles = ang;//回転情報のチェック用
        //Debug.Log(ang);

        //Rig移動
        hmd_rig.position += player.position - prePosi;

        //PC移動
        var temp = hmd_cam.position - hmd_cam.forward * 0.075f - player.position;
        temp.y = 0;
        player.position += temp;

        //向き制御
        var tmpDir = box.forward;
        tmpDir.y = 0;
        tmpDir = playerRot * tmpDir;
        player.forward = tmpDir;
        //player.eulerAngles = (ang.x - dirOffset) * Vector3.up;
        //Debug.Log((ang.y - dirOffset));

        ////速度制御
        //if (ang.x > 180) ang.x -= 360;
        //var dx = AngToSpeed(ang.z - 180);
        //var dz = AngToSpeed(ang.x);
        //dz = 1;
        ////Debug.Log(dz+", "+dx);
        //if (dz >= 0)
        //{
        //    player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //}
        //else
        //{
        //    //var deg = (dx == 0) ? 0 : Mathf.Atan(dz / dx) * 360 / (2 * Mathf.PI);
        //    //Debug.Log("deg: "+deg);

        //    //var deg = (dx == 0) ? 90 : Mathf.Atan(Mathf.Abs(dz / dx)) * 360 / (2 * Mathf.PI);
        //    //int offset = 90;

        //    //if (dx < 0 || (dx == 0 && dz < 0)) offset += 180;

        //    //player.GetComponent<Rigidbody>().velocity = mvSpeed * (Quaternion.AngleAxis(deg + offset, Vector3.up) * player.forward);
        //    player.GetComponent<Rigidbody>().velocity = -1* mvSpeed * player.forward * dz;
        //}
        player.GetComponent<Rigidbody>().velocity = moveFlag ? mvSpeed * player.forward : Vector3.zero;
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

    // public void HeightReset()
    // {
    //     var temp = Player.instance.transform.position.y + 1.35f - mycamera.position.y;
    //     transform.position += Vector3.up * temp;
    // }

    // public void WidthReset()
    // {
    //     var newlength = Vector3.Distance(leftHand.position, rightHand.position);
    //     if (newlength > 0)
    //         length = newlength;
    //     transform.localScale *= lengthHandToHand / length;
    //     Debug.Log("length: " + newlength);
    // }

    // public void ResetRot()
    // {
    //     var dir = userCamera.mycamera.transform.forward;
    //     dir.y = 0;
    //     transform.forward = dir;
    //     dirOffset = Controller.AccelData().x - transform.eulerAngles.y;
    // }

    public void SetAng(Vector3 value) 
    {
        ang = value;
    }
}

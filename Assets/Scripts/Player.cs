using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PCに関するメインのクラス
public class Player : MonoBehaviour
{
    public static Player instance;
    public static readonly int kindOfAction = 3;

    public bool acting = false;

    private Animator animator;
    private Rigidbody rb;

    public UserCamera userCamera;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject ArmR, ArmL, Menu, ContR, ContL;//消去候補

    private bool listening = false;
    private Vector3 offset = Vector3.forward;

    public float dirOffset = 0;

    [SerializeField]
    private Transform pivot;


    private bool preMove = false, mvCool = false;

    protected void Start()
    {
        //if (GameManager.newGame)
        //{
        //    var temp = new CharacterStatus("Player", 1000, 100, 100, 50, AttackAttribute.normal);
        //    Init(temp, 1);//仮のステータス
        //}
        //else storage.Load();
        //storage.player = this;
        //IsPlayer = true;
        //instance = this;
        //animator = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>();
        //actionList = new List<IActionable>[kindOfAction];        
        //luRate = 1.1f;s
    }

    

    //private void FixedUpdate()
    //{
    //    var data = Controller.AccelData();
    //    var move = !(data.x < 0);

    //    //if (preMove != move)
    //    //{
    //    //    mvCool = true;
    //    //    Scheduler.AddEvent(() => mvCool = false, 0.8f);
    //    //}
    //    //preMove = move;


    //    if (move && !listening)
    //    {
    //        var dir = Quaternion.AngleAxis(data.x, Vector3.up) * transform.forward;
    //        rb.velocity = dir * moveSpeed * (Controller.StateLT() ? 0.5f : 1);
    //        animator.SetBool("Running", true);
    //    }
    //    else
    //    {
    //        rb.velocity = Vector3.zero;
    //        animator.SetBool("Running", false);
    //    }

    //    transform.eulerAngles = Vector3.up * (data.y - dirOffset);
    //    //pivot.eulerAngles = Vector3.zero;
    //}

    //public void ResetRot()
    //{
    //    var dir = userCamera.mycamera.transform.forward;
    //    dir.y = 0;
    //    transform.forward = dir;
    //    dirOffset = Controller.AccelData().x - transform.eulerAngles.y;
    //}
}
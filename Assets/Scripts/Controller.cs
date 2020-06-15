using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Valve.VR;

public class Controller : MonoBehaviour
{
    private static Controller instance;

    //[SerializeField]
    //private GameObject ArmR, ArmL, Menu, ContR, ContL;
    //private bool modeFlag = false, menuFlag = false, mvCool = false, move = false;
    //private float mvAngle, rotAngle;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("eroor: already exist instance");
            Destroy(gameObject);
        }
    }

    //private void Start()
    //{
    //    Menu.SetActive(false);
    //    ArmR.SetActive(false);
    //    ArmL.SetActive(false);
    //    ContR.SetActive(true);
    //    ContL.SetActive(true);

    //    //UDPReceiver.AccelCallBack += AccelAction;
    //    UDPReceiver.RotCallBack += RotAction;
    //    UDPReceiver.UDPStart();
    //}

    private void Update()
    {
        if (InputVive.right.trackpad.GetBottunDown())
        {

        }

        if (InputVive.right.trigger.GetBottunDown())
        {

        }
        if (InputVive.right.menu.GetBottunDown()) { }

    }
}
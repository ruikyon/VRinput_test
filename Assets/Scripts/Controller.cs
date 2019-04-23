using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Valve.VR;

public class Controller : MonoBehaviour
{
    private static Controller instance;

    [SerializeField]
    private GameObject ArmR, ArmL, Menu, ContR, ContL;
    private bool modeFlag = false, menuFlag = false, mvCool = false, move = false;
    private float mvAngle, rotAngle;

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

    private void Start()
    {
        Menu.SetActive(false);
        ArmR.SetActive(false);
        ArmL.SetActive(false);
        ContR.SetActive(true);
        ContL.SetActive(true);

        UDPReceiver.AccelCallBack += AccelAction;
        UDPReceiver.RotCallBack += RotAction;
        UDPReceiver.UDPStart();
    }

    private void Update()
    {
        
    }

    public static Vector2 AccelData()
    {
        return new Vector2((instance.move)?instance.mvAngle:-1, instance.rotAngle);
    }

    public static bool StateLT()
    {
        //return SteamVR_Input._default.inActions.InteractUI.GetState(SteamVR_Input_Sources.LeftHand);
        return true;
    }

    //加速度に応じて移動フラグ変更
    private void AccelAction(float xx, float yy, float zz)
    {
        if (mvCool) return;
        if (move && yy > 1.25 && Mathf.Abs(xx) < 0.5f && Mathf.Abs(zz) < 0.5f) move = false;
        else if (!move)
        {
            if (zz < -1.2)
            {
                move = true;
                mvAngle = 0;
            }
            else if (zz > 0.8)
            {
                move = true;
                mvAngle = 180;
            }
            else if (xx < -1.35)
            {
                move = true;
                mvAngle = 90;
            }
            else if (xx > 1.35)
            {
                move = true;
                mvAngle = 270;
            }
        }
    }

    //回転によってカメラ方向を変更
    private void RotAction(float xx, float yy, float zz, float ww)
    {

        rotAngle = (new Quaternion(0, -zz, 0, ww) * Quaternion.Euler(0, 90f, 0)).eulerAngles.y;
    }
}
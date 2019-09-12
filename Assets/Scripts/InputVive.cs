using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR;

public class InputVive : MonoBehaviour
{
    public static bool vr;
    public static Controller left, right;
    private static InputVive instance;
    public static int LastUpdate { get; private set; }
    private Action updateCallback;

    private readonly SteamVR_Action_Boolean menu = SteamVR_Actions._default.Menu;
    private readonly SteamVR_Action_Boolean trigger = SteamVR_Actions._default.InteractUI;
    private readonly SteamVR_Action_Boolean trackBottun = SteamVR_Actions._default.Teleport;
    private readonly SteamVR_Action_Vector2 track = SteamVR_Actions._default.TrackPosi;
    private readonly SteamVR_Action_Vibration haptic = SteamVR_Actions._default.Haptic;

    private readonly float border = 0.5f;

    private void Start()
    {
        if (instance != null)
        {
            Debug.LogError("instance already exists.");
            Destroy(gameObject);
            return;
        }

        instance = this;

        //以下要変更
        if (vr)
        {
            right = new Controller(
                new Controller.Bottun(() => menu.GetStateDown(SteamVR_Input_Sources.RightHand)),
                new Controller.Bottun(() => trigger.GetStateDown(SteamVR_Input_Sources.RightHand)),
                new Controller.Bottun(() => trackBottun.GetStateDown(SteamVR_Input_Sources.RightHand)),
                new Controller.AxisRaw(() =>
                {
                    var value = track.GetAxis(SteamVR_Input_Sources.RightHand).x;
                    if (value < -border) return -1;
                    else if (value < border) return 0;
                    else return 1;
                }),
                new Controller.AxisRaw(() =>
                {
                    var value = track.GetAxis(SteamVR_Input_Sources.RightHand).y;
                    if (value < -border) return -1;
                    else if (value < border) return 0;
                    else return 1;
                })
                );

            left = new Controller(
                new Controller.Bottun(() => menu.GetStateDown(SteamVR_Input_Sources.LeftHand)),
                new Controller.Bottun(() => trigger.GetStateDown(SteamVR_Input_Sources.LeftHand)),
                new Controller.Bottun(() => trackBottun.GetStateDown(SteamVR_Input_Sources.LeftHand)),
                new Controller.AxisRaw(() =>
                {
                    var value = track.GetAxis(SteamVR_Input_Sources.LeftHand).x;
                    if (value < -border) return -1;
                    else if (value < border) return 0;
                    else return 1;
                }),
                new Controller.AxisRaw(() =>
                {
                    var value = track.GetAxis(SteamVR_Input_Sources.LeftHand).y;
                    if (value < -border) return -1;
                    else if (value < border) return 0;
                    else return 1;
                })
                );
        }
        else//not vr
        {
            right = new Controller(
                new Controller.Bottun(() => Input.GetKeyDown(KeyCode.M)),
                new Controller.Bottun(() => Input.GetKeyDown(KeyCode.T)),
                new Controller.Bottun(() => Input.GetKeyDown(KeyCode.Return)),
                new Controller.AxisRaw(() => (int)Input.GetAxisRaw("Horizontal")),
                new Controller.AxisRaw(() => (int)Input.GetAxisRaw("Vertical"))
                );

            left = new Controller(
                new Controller.Bottun(() => false),
                new Controller.Bottun(() => false),
                new Controller.Bottun(() => false),
                new Controller.AxisRaw(() => 0),
                new Controller.AxisRaw(() => 0)
                );
        }
    }

    private void LateUpdate()
    {
        LastUpdate = Time.frameCount;
        updateCallback();
    }

    public static void AddCallBack(Action value)
    {
        instance.updateCallback += value;
    }

    public class Controller
    {
        public readonly Bottun trigger, menu, trackpad;
        public readonly AxisRaw horizontal, vertical;

        public Controller(Bottun _trigger, Bottun _menu, Bottun _trackpad, AxisRaw _horizontal, AxisRaw _vertical)
        {
            trigger = _trigger;
            menu = _menu;
            trackpad = _trackpad;
            horizontal = _horizontal;
            vertical = _vertical;
        }

        public class Bottun : InputTemplate<bool>
        {
            public Bottun(Func<bool> value) : base(value) { }

            public bool GetBottunDown()
            {
                return (Value && !PreValue);
            }
        }

        public class AxisRaw : InputTemplate<int>
        {
            public AxisRaw(Func<int> value) : base(value) { }

            public bool ChangeNZ()
            {
                return (Value != 0 && PreValue == 0);
            }
        }

        public class Position : InputTemplate<Vector2>
        {
            public Position(Func<Vector2> value) : base(value) { }
        }
    }
}
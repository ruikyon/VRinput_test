using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR;

public class InputVive : MonoBehaviour
{
    public static Controller left, right;
    private static InputVive instance;
    public static int LastUpdate { get; private set; }
    private Action updateCallback;

    private SteamVR_Action_Boolean menu = SteamVR_Actions._default.Menu;
    private SteamVR_Action_Vector2 track = SteamVR_Actions._default.TrackPosi;
    private SteamVR_Action_Vibration haptic = SteamVR_Actions._default.Haptic;

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
        Func<bool>[] tempL = {
            ()=>false,
            ()=>false,
            ()=>false
        };
        left = new Controller(this, tempL);

        Func<bool>[] tempR = {
            ()=>false,
            ()=>false,
            ()=>false
        };
        right = new Controller(this, tempR);
    }

    private void LateUpdate()
    {
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

        public Controller(InputVive parent, Func<bool>[] funcs)
        {
            trigger = new Bottun(funcs[0]);
            menu = new Bottun(funcs[1]);
            trackpad = new Bottun(funcs[2]);
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputVive : MonoBehaviour
{
    public static Controller left, right;
    private static InputVive instance;
    private Action updateCallback;

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
        Func<bool>[] temp1 = {
            ()=>false,
            ()=>false,
            ()=>false
        };
        left = new Controller(this, temp1);

        Func<bool>[] temp2 = {
            ()=>false,
            ()=>false,
            ()=>false
        };
        right = new Controller(this, temp2);
    }

    private void LateUpdate()
    {
        updateCallback();
    }

    public class Controller
    {
        public readonly Bottun trigger, menu, trackpad;
        public readonly AxisRaw horizontal, vertical;

        public Controller(InputVive parent, Func<bool>[] funcs)
        {
            trigger = new Bottun(parent, funcs[0]);
            menu = new Bottun(parent, funcs[1]);
            trackpad = new Bottun(parent, funcs[2]);
        }

        public class Bottun
        {
            public bool State { get; private set; }
            private bool preState;

            public Bottun(InputVive parent, Func<bool> update)
            {
                parent.updateCallback += () =>
                {
                    preState = State;
                    State = update();
                };
            }

            public bool GetBottunDown()
            {
                return (State && !preState);
            }
        }

        public class AxisRaw
        {
            public int Value { get; private set; }
            private int preValue;

            public AxisRaw(InputVive parent, Func<int> update)
            {
                parent.updateCallback += () =>
                {
                    preValue = Value;
                    Value = update();
                };
            }

            public bool ChangePositive()
            {
                return (Value == 1 && preValue != 1);
            }

            public bool ChangeNegative()
            {
                return (Value == -1 && preValue != -1);
            }
        }

        public class Position
        {
            public Vector2 Value { get; private set; }
            private Vector2 preValue;

            public Position(InputVive parent, Func<Vector2> update)
            {
                parent.updateCallback += () =>
                {
                    preValue = Value;
                    Value = update();
                };
            }
        }
    }
}
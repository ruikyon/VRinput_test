using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputVive : MonoBehaviour
{
    public Controller controller;

    

    public class Controller
    {
        
        public class Bottun
        {
            public bool State { get; private set; }
            private bool preState;
            private Func<bool> stateUpdate;

            public Bottun(Func<bool> update)
            {
                stateUpdate = update;
            }

            public void Update()
            {
                preState = State;
                State = stateUpdate();
            }
        }
    }


}
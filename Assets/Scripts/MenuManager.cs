using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DefaultExecutionOrder(500)]
public class MenuManager : MonoBehaviour
{
    private Menu now;
    [SerializeField]
    private Menu top;
    [SerializeField]
    private ConfirmPanel cm;

    private void Awake()
    {
        foreach (Transform child in transform)
            if (child.GetComponent<Menu>() != null)
                child.GetComponent<Menu>().Init(this);
    }

    private void OnEnable()
    {
        PanelChange(top, true);
    }

    public void PanelChange(Menu to, bool reset)
    {
        if (now != null) now.gameObject.SetActive(false);
        now = to;
        to.gameObject.SetActive(true);
        if(reset) to.ResetData();
    }

    public void Confirm(string message, Action action)
    {
        cm.gameObject.SetActive(true);
        cm.Init(message, action, now);
        PanelChange(cm, false);
    }
}

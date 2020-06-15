using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Menu : MonoBehaviour
{

    [SerializeField]
    protected bool udFlag, lrFlag;
    private int selectLine = 0;
    protected MenuManager manager;
    protected readonly float height = 1.17f;
    [SerializeField]
    protected RectTransform selecter;
    [SerializeField]
    protected int lineSize;
    [SerializeField]
    protected TextMeshPro title, text;
    [SerializeField]
    protected Menu prePanel;
    [SerializeField]
    protected Menu[] nextPanels;
    protected Vector3 basePosi;

    public void Init(MenuManager _manager)
    {

        if (manager) return;

        basePosi = selecter.localPosition;
        manager = _manager;
        lineSize--;
        if (!udFlag && !lrFlag) selecter.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputVive.right.trackpad.Value)
        {
            Decide(selectLine);
        }
        else if (InputVive.right.vertical.ChangeNZ())
        {
            selecter.localPosition += new Vector3(0, 0, height * InputVive.right.vertical.Value);
            selectLine -= InputVive.right.vertical.Value;
        }
        else if (InputVive.right.horizontal.ChangeNZ())
        {
            ChangeValue(InputVive.right.horizontal.Value);
        }
        else if (InputVive.right.trigger.Value)
        {
            if (prePanel != null) manager.PanelChange(prePanel, false);
        }
    }

    protected virtual void Decide(int index) { }//決定時の操作

    protected virtual void ChangeValue(int dir) { }//値の変更

    public void ResetData()
    {
        if (selecter.gameObject.activeSelf)
        {
            selecter.localPosition = basePosi;
            selectLine = 0;
        }
        SetData();
    }

    protected virtual void SetData() { }//データを更新
}
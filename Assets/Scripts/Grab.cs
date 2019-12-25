using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grab : MonoBehaviour
{
    private readonly SteamVR_Action_Boolean trigger = SteamVR_Actions._default.InteractUI;

    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(flag+","+ trigger.GetState(SteamVR_Input_Sources.RightHand));
        if(flag && trigger.GetState(SteamVR_Input_Sources.RightHand))
        {
            TaskManager.Instance.NextTask();
            flag = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "TargetObject")
        {
            flag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TargetObject>() != null)
        {
            flag = false;
        }
    }
}

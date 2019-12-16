using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(flag && InputVive.right.trigger.GetBottunDown())
        {
            TaskManager.Instance.NextTask();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TargetObject>()!=null)
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

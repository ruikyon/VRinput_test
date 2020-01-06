using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private TargetObject[] targets;
    private int taskCount;
    private float start, end;
    public static TaskManager Instance { get; private set; }
    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(System.Environment.CurrentDirectory);
        //File.AppendAllText("sample.txt", "test");

        Instance = this;
        taskCount = 0;
        //Scheduler.AddEvent(StartTask, 5);
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!flag && Input.GetKeyDown(KeyCode.S))
        {
            StartTask();
            flag = true;
        }
    }

    private void StartTask()
    {
        Debug.Log("-task start-");
        start = Time.time;
        targets[0].Activate();
    }

    private void EndTask()
    {
        end = Time.time;
        Debug.Log("result time: " + (end - start));
        Debug.Log("-task end-");
        File.AppendAllText("result.txt", DateTime.Now.ToString() + ": " + (end - start) + Environment.NewLine);
    }

    public void NextTask()
    {
        Debug.Log("-next task-");
        targets[taskCount].gameObject.SetActive(false);
        taskCount++;
        if (taskCount < targets.Length)
        {
            targets[taskCount].Activate();
        }
        else
        {
            EndTask();
        }
    }
}

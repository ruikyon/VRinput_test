using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private TargetObject[] targets;
    private int taskCount;
    private float start, end;
    public static TaskManager Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        taskCount = 0;
        Scheduler.AddEvent(StartTask, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
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

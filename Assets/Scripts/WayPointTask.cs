using UnityEngine;
using System.Collections;
using System;

public class WayPointTask : MonoBehaviour
{
    public bool isFinished = false;

    public enum TaskType
    {
        WaitForSeconds,
        DropDoll,
    }

    [Serializable]
    public class SubTask
    {
        public TaskType type;
        public string value;
        public bool isFinished;
    }

    public SubTask[] subTasks;
    public int currentTask = -1;

    void Awake()
    {
        isFinished = false;
        this.enabled = false;
    }

    // Use this for initialization
    IEnumerator Start()
    {
        if (null == subTasks || subTasks.Length == 0)
        {
            isFinished = true;
        }
        else
        {
            currentTask = 0;
        }

        while (!isFinished)
        {
            SubTask subTask = subTasks[currentTask];

            switch (subTask.type)
            {
                case TaskType.WaitForSeconds:
                    {
                        float seconds = float.Parse(subTask.value);
                        yield return new WaitForSeconds(seconds);
                        currentTask++;
                    }
                    break;

                case TaskType.DropDoll:
                    {
                        GameObject NPCManag = GameObject.Find("NPCManager");
                        NPCManag.GetComponent<NPCManager>().currentDoll.GetComponent<ItemProperty>().Drop(NPCManag.GetComponent<NPCManager>().currentLittleGirl);
                        currentTask++;
                    }
                    break;
            }
            
            if (currentTask >= subTasks.Length)
            {
                isFinished = true;
            }
            yield return null;
        }
    }
}

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
        WaitForBoysReady,
        StartToPlayBaseball,
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

                case TaskType.WaitForBoysReady:
                    {
                        bool ready=false;
                        do
                        {
                            GameObject b1 = GameObject.Find("NPCManager").GetComponent<NPCManager>().currentBaseballBoy;
                            GameObject b2 = GameObject.Find("NPCManager").GetComponent<NPCManager>().currentAnotherBaseballBoy;
                            ready = b1.GetComponent<WayPointsWalker>().currentWayPoint == 0 && b2.GetComponent<WayPointsWalker>().currentWayPoint == 0 && b1.GetComponent<PathFinder>().CurrentState != PathFinder.State.InPath && b2.GetComponent<PathFinder>().CurrentState != PathFinder.State.InPath;
                            yield return null;
                        } while (!ready);
                        currentTask++;
                    }
                    break;
                case TaskType.StartToPlayBaseball:
                    {
                        GameObject b1 = GameObject.Find("NPCManager").GetComponent<NPCManager>().currentBaseballBoy;
                        GameObject b2 = GameObject.Find("NPCManager").GetComponent<NPCManager>().currentAnotherBaseballBoy;
                        b1.GetComponent<ThrowBaseball>().Arrived();
                        b2.GetComponent<ThrowBaseball>().Arrived();
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

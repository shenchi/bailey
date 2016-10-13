using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class TimeTrigger
{
    public int day;
    public float hour;
    public GameObject messageReceiver;
    public string message;
    public bool inUse;
}

public class TimeManager : MonoBehaviour
{
    public Transform clockPointer;
    public DayLight dayLightEffect;
    public float timeForDayTime = 90.0f;

    public Color dawnColor;
    public Color noonColor;

    public TimeTrigger[] timeTriggers;


    /// <summary>
    /// 0 for 8:00 am
    /// </summary>
    public float VirtualTimeInHour { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public int Day
    {
        get
        {
            return dayPassed + 1;
        }
    }

    private List<TimeTrigger> triggers;

    private enum State
    {
        None,
        Day,
        Night,
    }

    private State state = State.None;
    private float timeWhenStateBegin;
    private int dayPassed = 0;
    
    void Awake()
    {
        triggers = new List<TimeTrigger>(timeTriggers);
    }

    // Use this for initialization
    void Start()
    {
        StartDay();
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - timeWhenStateBegin;

        switch (state)
        {
            case State.Day:
                {
                    float progress = elapsedTime / timeForDayTime;
                    VirtualTimeInHour = progress * 12 + 8;

                    for (int i = triggers.Count - 1; i >= 0; i--)
                    {
                        var trigger = triggers[i];
                        if (!trigger.inUse)
                        {
                            triggers.RemoveAt(i);
                            continue;
                        }

                        if (Day != trigger.day && trigger.day != -1)
                            continue;

                        if (VirtualTimeInHour > trigger.hour)
                        {
                            trigger.messageReceiver.SendMessage(trigger.message);

                            if (trigger.day != -1)
                            {
                                triggers.RemoveAt(i);
                            }
                        }

                    }

                    if (null != clockPointer)
                    {
                        float angle = progress * 360.0f - 120.0f;
                        clockPointer.localRotation = Quaternion.Euler(0, 0, -angle);
                    }

                    if (null != dayLightEffect)
                    {
                        float x = Mathf.Pow(progress, 0.7f);// progress * 0.8f + 0.2f;
                        float t = Mathf.Clamp01(Mathf.Pow((Mathf.Cos(x * 2.0f * Mathf.PI) + 1) * 0.5f, 2));
                        dayLightEffect.tintColor = Color.Lerp(noonColor, dawnColor, t);
                    }

                    if (progress >= 1.0f)
                    {
                        EndDay();
                    }
                }
                break;
            case State.Night:
                {
                    float progress = elapsedTime / 10.0f;

                    float x = progress * 1.2f;

                    if (null != dayLightEffect && x <= 1.0f)
                    {
                        float t = Mathf.Clamp01(Mathf.Pow(Mathf.Sin(x * Mathf.PI), 0.5f));
                        dayLightEffect.tintColor = Color.Lerp(dawnColor, Color.black, t);
                    }

                    if (progress >= 1.0f)
                    {

                        foreach (GameObject NPC in GameObject.FindGameObjectsWithTag("NPC"))
                        {
                            Destroy(NPC);
                        }

                        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
                        {
                            if (item.GetComponent<ItemProperty>().isHome == false && item.GetComponent<ItemProperty>().owner == null)
                            {
                                Destroy(item);
                            }
                        }

                        StartDay();
                    }
                }
                break;
            default:
                break;
        }
    }

    void StartDay()
    {
        if (state == State.Day)
            return;

        clockPointer.parent.gameObject.SetActive(true);

        timeWhenStateBegin = Time.time;
        state = State.Day;
    }

    void EndDay()
    {
        if (state != State.Day)
            return;


        clockPointer.parent.gameObject.SetActive(false);

        dayPassed++;
        timeWhenStateBegin = Time.time;
        state = State.Night;
    }
}

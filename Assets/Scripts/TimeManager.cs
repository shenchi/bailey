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

    public int Day
    {
        get
        {
            return dayPassed + 1;
        }
    }

    public List<TimeTrigger> triggers;

    private bool paused = true;
    private float timeWhenDayBegin;
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
        if (paused)
            return;

        float elapsedTime = Time.time - timeWhenDayBegin;
        float progress = elapsedTime / timeForDayTime;
        VirtualTimeInHour = progress * 12;

        for (int i = triggers.Count - 1; i >= 0; i--)
        {
            var trigger = triggers[i];

            if (Day != trigger.day && trigger.day != -1)
                continue;

            if (VirtualTimeInHour > trigger.hour)
            {
                trigger.messageReceiver.SendMessage(trigger.message);
            }

            if (trigger.day != -1)
            {
                triggers.RemoveAt(i);
            }
        }

        if (null != clockPointer)
        {
            float angle = progress * 360.0f - 120.0f;
            clockPointer.localRotation = Quaternion.Euler(0, 0, -angle);
        }

        if (null != dayLightEffect)
        {
            float x = progress * 0.8f + 0.2f;
            float t = Mathf.Clamp01(Mathf.Pow((Mathf.Cos(x * 2.0f * Mathf.PI) + 1) * 0.5f, 3) );
            dayLightEffect.tintColor = Color.Lerp(noonColor, dawnColor, t);
        }
    }

    void StartDay()
    {
        if (!paused)
            return;
        
        timeWhenDayBegin = Time.time;
        paused = false;
    }

    void EndDay()
    {
        if (paused)
            return;

        paused = true;
        dayPassed++;
    }
}

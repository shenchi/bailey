using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    public Transform clockPointer;
    public DayLight dayLightEffect;
    public float timeForDayTime = 90.0f;

    public Color dawnColor;
    public Color noonColor;

    private float timeWhenDayBegin;
    
    /// <summary>
    /// 0 for 8:00 am
    /// </summary>
    public float VirtualTimeInHour { get; private set; }

    // Use this for initialization
    void Start()
    {
        timeWhenDayBegin = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - timeWhenDayBegin;
        float progress = elapsedTime / timeForDayTime;
        VirtualTimeInHour = progress * 12;

        if (null != clockPointer)
        {
            float angle = progress * 360.0f - 120.0f;
            clockPointer.localRotation = Quaternion.Euler(0, 0, -angle);
        }

        if (null != dayLightEffect)
        {
            float t = Mathf.Clamp01(Mathf.Pow((Mathf.Cos(progress * 2.0f * Mathf.PI) + 1) * 0.5f, 7) );
            dayLightEffect.tintColor = Color.Lerp(noonColor, dawnColor, t);
        }
    }
}

using UnityEngine;
using System.Collections;

public class LinearMovement : MonoBehaviour
{

    public float distanceThreshold = 1.0f;
    public float speed = 1.0f;
    
    public GameObject messageReceiver;
    public string messageOnArrival;

    private bool moving = false;
    private float timeSpeed = 0.0f;
    private float t = 0;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if ((transform.position - targetPosition).magnitude < distanceThreshold)
            {
                moving = false;
                //if (null != messageReceiver && string.IsNullOrEmpty(messageOnArrival))
                //{
                //    messageReceiver.SendMessage(messageOnArrival, this.gameObject);
                //}
                if (null != messageReceiver) {
                    messageReceiver.GetComponent<ThrowBaseball>().OnBallCatched(gameObject);
                }
                
            }
            else
            {
                t += Time.deltaTime * timeSpeed;
                transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            }
        }
    }

    public void MoveTo(GameObject go)
    {
        MoveTo(go.transform.position);
    }

    public void MoveTo(Transform t)
    {
        MoveTo(t.position);
    }

    public void MoveTo(Vector3 v)
    {
        targetPosition = v;
        startPosition = transform.position;
        targetPosition.z = startPosition.z;
        timeSpeed = speed/(targetPosition - startPosition).magnitude;
        t = 0.0f;
        moving = true;
    }
}

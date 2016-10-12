using UnityEngine;
using System.Collections;

public class GameEventManager : MonoBehaviour
{
    public GameObject doll;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartEvent(int EventID, int StepID, GameObject go)
    {
        switch (EventID)
        {
            case 0://little girl's story
                {
                    switch (StepID)
                    {
                        case 0:
                            //walk to dog, drop doll, walk away ,cry, return happy
                            go.GetComponent<PathFinder>().FindPath(938, -755);//dog house
                            break;
                        case 1:
                            Instantiate(doll, go.transform.position, Quaternion.identity);
                            break;
                        case 2:
                            go.GetComponent<PathFinder>().FindPath(1288, -267);//go away
                            break;
                        case 3:
                            go.GetComponent<NPCProperty>().SetMood("bad");
                            break;
                        case 4:
                            go.GetComponent<NPCProperty>().SetMood("good");
                            break;
                        default:
                            break;
                    }
                    break;
                }
        }

    }

}

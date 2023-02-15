using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float time;
    int maxTime;
    
    int day = 0;
    public int dayLength = 60;
    // Start is called before the first frame update
    void Start()
    {
        time = dayLength;
    }

    // Update is called once per frame
    void Update()
    {
        maxTime = (int)Time.realtimeSinceStartup;

        DayTimer(time);
        //print((int)time);
    }
    void DayTimer(float currentTime)
    {
        if (currentTime > 0)
        {
            time -= 1 * Time.deltaTime;
            //print((int)currentTime);

        }
        else
        {
            currentTime += dayLength;            
            //print("a day has passed \n day:" + day);
            DayTimer(currentTime);
        }
    }

    /*void DayTracker()
    {
        if (time % dayLength == 0)
        {
            day ++;
            print("day: "+ day);
            
        }
    }*/
}

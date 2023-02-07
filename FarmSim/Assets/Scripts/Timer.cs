using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    int time;
    int day = 0;
    public int dayLength = 60;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = (int)Time.realtimeSinceStartup;
        print(time);
    }
    private void FixedUpdate()
    {
        DayTracker();
    }

    void DayTracker()
    {
        if (time % dayLength == 0)
        {
            day ++;
            print("day: "+ day);
            
        }
    }
}

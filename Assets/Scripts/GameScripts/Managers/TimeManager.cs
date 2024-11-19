using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>    
{
    public float timeDifficulty {  get; private set; }
    public bool isTimeActive { get; private set; }

    private void Update()
    {
        TimeUpdate();
    }

    private void TimeUpdate()
    {
        if (!isTimeActive)
            return;

        timeDifficulty += Time.deltaTime;
    }

    public void StopTime()
    {
        isTimeActive = false;
    }

    public void ActivateTime()
    {
        isTimeActive = true;
    }

}

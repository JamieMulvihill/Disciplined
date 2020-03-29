using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarp : MonoBehaviour
{
    [Header("Bools")]

    public bool triggerSlowMo;
    private bool slowMotion;
    private bool zoom;

    [Space]

    [Header("Floats")]

    private float sloMoFactor = 0.05f;
    private float sloMoDuration = 1f;

    void Update()
    {
        if (slowMotion) //slowly speed up time until it is back to normal
        {
            Time.timeScale += (1f / sloMoDuration) * Time.unscaledDeltaTime;
            //dont let time speed up anymore than 1x speed
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            if (Time.timeScale == 1f)
                slowMotion = false;
        }

        if (triggerSlowMo)
        {
            triggerSlowMo = false;
            SlowMo();
        }
    }

    void SlowMo()
    {
        //set time to slow
        Time.timeScale = sloMoFactor;
        //smoothing
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        slowMotion = true;
    }
}

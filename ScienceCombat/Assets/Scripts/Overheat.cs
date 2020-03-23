using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overheat
{
    public float chillThreshold = 100f;     // the value "heat" must decrease to when "heat" surpasses "maxHeat" before GetOverheated() will return false.

    public bool shouldClamp = false;        // if true, heat will never excede maxHeat
    public float timeToChill = 0f;          // the amount of time after Broil() before Chill() will have any affect

    protected float maxHeat = 100f;
    protected float heat = 0f;
    protected float lastBroil = 0f;         // the time when Broil was last used
    protected bool burned = false;          // true when "heat" surpasses "maxHeat", becomes false when "heat" becomes less than or equal to "chillThreshold"
    public float GetHeat()
    {
        return heat;
    }
    public float GetHeatFraction()
    {
        return heat / maxHeat;
    }
    public bool GetOverheated()
    {
        if (!burned)
        {
            return heat >= maxHeat;
        }
        else
        {
            return true;
        }
    }
    public void Broil(float addedHeat)
    {
        heat += Mathf.Abs(addedHeat);
        if (heat > maxHeat)
        {
            burned = true;
            if (shouldClamp)
            {
                heat = maxHeat;
            }
        }
        lastBroil = Time.time;
    }
    public void Chill(float removedHeat)
    {
        if (timeToChill + lastBroil >= Time.time)
        {
            return;
        }
        heat -= Mathf.Abs(removedHeat);
        if (heat < 0f)
        {
            heat = 0;
        }
        if (heat <= chillThreshold)
        {
            burned = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBashing : MonoBehaviour
{
    private bool last_x;
    private bool last_y;
    private bool last_a;
    private bool last_b;
    private bool last_lt;
    private bool last_rt;
    private bool last_lb;
    private bool last_rb;

    private bool x;
    private bool y;
    private bool a;
    private bool b;
    private bool lt;
    private bool rt;
    private bool lb;
    private bool rb;

    public int count;
    public int breakFree;
    private Scientist scientist;
    private string ID;

    void Start()
    {
        count = 0;
        scientist = GetComponent<Scientist>();
        ID = scientist.controllerIndex.ToString();
    }//

    void Update()
    {
        // If player is using keyboard, don't let them button bash.
        if (ID == "0")
            return;



        last_x = x;
        last_y = y;
        last_a = a;
        last_b = b;
        last_lt = lt;
        last_rt = rt;
        last_lb = lb;
        last_rb = rb;

        x = buttonPressed("X" + ID);
        y = buttonPressed("Y" + ID);
        a = buttonPressed("A" + ID);
        b = buttonPressed("B" + ID);
        lt = buttonPressed("LT" + ID);
        rt = buttonPressed("RT" + ID);
        lb = buttonPressed("LB" + ID);
        rb = buttonPressed("RB" + ID);

        if (scientist.isCaptured)
        {
            if ((x && !last_x) ||
               (y && !last_y) ||
               (a && !last_a) ||
               (b && !last_b) ||
               (lt && !last_lt) ||
               (rt && !last_rt) ||
               (lb && !last_lb) ||
               (rb && !last_rb))
            {
                if (++count == breakFree)
                {
                    scientist.isCaptured = false;
                    count = 0;
                }
            }
        }
    }

    bool buttonPressed(string button)
    {
        return (Mathf.Abs(Input.GetAxis(button)) > 0.01f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBashing : MonoBehaviour
{
    public string X;
    public string Y;
    public string A;
    public string B;
    public string LT;
    public string RT;
    public string LB;
    public string RB;

    private int count;
    public int breakFree;
    private Scientist scientist;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        scientist = GetComponent<Scientist>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scientist.isCaptured)
        {
            if (buttonPressed(X) ||
               buttonPressed(Y) ||
               buttonPressed(A) ||
               buttonPressed(B) ||
               buttonPressed(LT) ||
               buttonPressed(RT) ||
               buttonPressed(LB) ||
               buttonPressed(RB))
            {
                if (count++ > breakFree)
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

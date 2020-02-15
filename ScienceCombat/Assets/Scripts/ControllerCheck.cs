using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCheck : MonoBehaviour
{
    public string[] names;

    private void Start()
    {
        names = Input.GetJoystickNames();
    }
    // Update is called once per frame
    void Update()
    {
        if (names != Input.GetJoystickNames()) {
            names = Input.GetJoystickNames();
        }
        int i = 0;
        // if(Input.GetKeyDown(input))
        foreach (var item in names)
        {
            i++;
            if (Input.GetKeyDown($"joystick {i} button 0")) {
                print($"joystick {i} button 0");
            }
        }
    }
}

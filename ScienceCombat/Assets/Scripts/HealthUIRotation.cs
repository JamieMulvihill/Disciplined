using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIRotation : MonoBehaviour
{
    public bool useFixedRotation = true;
    private Quaternion fixedRotation;
    void Start()
    {
        //Get Rotation of the Canvas
        fixedRotation = transform.parent.localRotation;
    }
    // Update is called once per frame
    void Update()
    {
        // bool used to determine if rotation should be fixed
        if (useFixedRotation) {
            // setting rotation
            transform.rotation = fixedRotation;
        }
    }
}

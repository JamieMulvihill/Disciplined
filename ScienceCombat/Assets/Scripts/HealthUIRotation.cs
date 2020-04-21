using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIRotation : MonoBehaviour
{
    public bool useFixedRotation = true;
    private Quaternion fixedRotation;
    // Start is called before the first frame update
    void Start()
    {
        fixedRotation = transform.parent.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (useFixedRotation) {
            transform.rotation = fixedRotation;
        }
    }
}

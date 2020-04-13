using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallForce : MonoBehaviour
{
    public float xAxisForce;
    public float zAxisForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Scientist>()) {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(xAxisForce, 0, zAxisForce), ForceMode.Impulse);
        }
    }
}

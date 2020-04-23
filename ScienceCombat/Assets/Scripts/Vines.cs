using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : MonoBehaviour{

    private Scientist scientist;

    private void Start() {
        StartCoroutine(deleteSelf());
    }


    IEnumerator deleteSelf() {

       yield return new WaitForSeconds(6);
        if (scientist == null)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the Vines have collided with something that is not the biologist or the ground
        // get the Scientist component of the collided object
        if (other.gameObject.tag != "Biologist" && other.gameObject.tag != "Ground" && scientist == null)
        {
            scientist = other.gameObject.GetComponent<Scientist>();
            if (scientist)
            {
                // Set the Scientist to being captured
                // Start thr capture Co-Routine
                // Set the Spawn positon for the Vines gameObject
                // Set the Vines GAmeObject to active
                scientist.isCaptured = true;
                StartCoroutine(Capture(scientist));
                transform.GetChild(0).position = other.transform.position;
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    // function to handle the Capture behavior
   IEnumerator Capture(Scientist capturedScientist) {
        // Set the captured Scientist Kinematic to true so it connot move
        // wait for 6 seconds
        capturedScientist.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(6);
        // Free the Scientist
        // set the Captured Scientist to not captured
        // set the Scienitst back to not kinemtatic
        // Destroy the Vines gameobject
        capturedScientist.isCaptured = false;
        scientist = null;
        capturedScientist.GetComponent<Rigidbody>().isKinematic = false;
        Destroy(gameObject);
   }

    private void LateUpdate()
    {
        if(scientist)
        {
            if(!scientist.isCaptured)
            {
                scientist.GetComponent<Rigidbody>().isKinematic = false;
                scientist = null;
                Destroy(gameObject);
            }
        }
    }

}

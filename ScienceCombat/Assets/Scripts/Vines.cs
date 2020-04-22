using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : MonoBehaviour{

    private Scientist scientist;
    private float originalSpeed;

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
        if (other.gameObject.tag != "Biologist" && other.gameObject.tag != "Ground" && scientist == null)
        {
            scientist = other.gameObject.GetComponent<Scientist>();
            if (scientist)
            {
                scientist.isCaptured = true;
                StartCoroutine(Capture(scientist));
                transform.GetChild(0).position = other.transform.position;
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

   IEnumerator Capture(Scientist capturedScientist) {
        capturedScientist.GetComponent<Rigidbody>().isKinematic = true;
       
        yield return new WaitForSeconds(6);
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

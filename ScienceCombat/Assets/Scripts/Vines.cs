using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : MonoBehaviour{

    private Scientist scientist;
    private float originalSpeed;

    private void Start()
    {
        //height = transform.GetChild(0).GetChild(0).GetComponent<MeshFilter>().mesh.bounds.max.y*2f;
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
                //originalSpeed = scientist.speed;
                StartCoroutine(Capture(scientist));
                transform.GetChild(0).position = other.transform.position;
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

   IEnumerator Capture(Scientist capturedScientist) {
       // capturedScientist.speed = 0;
        capturedScientist.GetComponent<Rigidbody>().isKinematic = true;
        //capturedScientist.gameObject.transform.position = transform.position;
        //capturedScientist.gameObject.transform.position = Vector3.MoveTowards(capturedScientist.gameObject.transform.position, transform.position, .5f);
        yield return new WaitForSeconds(6);
        capturedScientist.isCaptured = false;
        scientist = null;
        capturedScientist.GetComponent<Rigidbody>().isKinematic = false;
       // capturedScientist.speed = originalSpeed;
        Destroy(gameObject);
   }


}

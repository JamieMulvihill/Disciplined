using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : MonoBehaviour{

    private Scientist scientist;
    private float originalSpeed;

    float height;


    private void Start()
    {
        height = transform.GetChild(0).GetChild(0).GetComponent<MeshFilter>().mesh.bounds.max.y*2f;
        StartCoroutine(deleteSelf());
    }

    private void Update()
    {
        transform.GetChild(0).localScale = Vector3.Lerp(transform.GetChild(0).localScale, Vector3.one, 0.04f);
    }

    IEnumerator deleteSelf() {

       yield return new WaitForSeconds(8);
        if (scientist == null)
        {
            Destroy(gameObject);
        }
    }



    private void OnCollisionEnter(Collision collision){

        if (collision.gameObject.tag != "Biologist" && collision.gameObject.tag != "Ground" && scientist == null ) {

            scientist = collision.gameObject.GetComponent<Scientist>();
            originalSpeed = scientist.speed;
            StartCoroutine(Capture(scientist));
        }
    }
    IEnumerator Capture(Scientist capturedScientist) {
        capturedScientist.speed = 0;
        capturedScientist.GetComponent<Rigidbody>().isKinematic = true;
        capturedScientist.gameObject.transform.position = transform.position + transform.up * height;
        yield return new WaitForSeconds(10);
        capturedScientist.GetComponent<Rigidbody>().isKinematic = false;
        capturedScientist.speed = originalSpeed;
        Destroy(gameObject);
    }


}

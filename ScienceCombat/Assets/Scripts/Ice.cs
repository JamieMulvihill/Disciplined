using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    [SerializeField] private GameObject particleEffectPrefab;
    [SerializeField] private float radius;
    [SerializeField] private float duration;
    private GameObject particleEffect;
    private Vector3 initialVelocity;
    private GameObject slidingScientist;

    // Start is called before the first frame update
    void Start()
    {
        particleEffect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        StartCoroutine(deleteSelf());

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Scientist>() != null)
        {
            slidingScientist = other.gameObject;
            //initialVelocity = other.gameObject.transform.forward * 10;
            other.gameObject.GetComponent<Scientist>().slippingVelocity = other.gameObject.transform.forward * 10;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Scientist>() != null)
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = other.gameObject.GetComponent<Scientist>().slippingVelocity;
            other.gameObject.GetComponent<Scientist>().isSliding = true;
        }



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Scientist>() != null)
        {
            other.gameObject.GetComponent<Scientist>().isSliding = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator deleteSelf()
    {
        yield return new WaitForSeconds(duration);
        slidingScientist.gameObject.GetComponent<Scientist>().isSliding = false;
        Destroy(particleEffect);
        Destroy(gameObject);
    }
}

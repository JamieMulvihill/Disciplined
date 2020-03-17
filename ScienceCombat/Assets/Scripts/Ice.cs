using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    [SerializeField] private GameObject particleEffectPrefab;
    [SerializeField] private float radius;
    [SerializeField] private float duration;
    private GameObject particleEffect;
    private List<GameObject> slidingScientists;

    // Start is called before the first frame update
    void Start()
    {
        slidingScientists = new List<GameObject>();
        particleEffect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        StartCoroutine(deleteSelf());

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Scientist>() != null && other.gameObject.tag != "Chemist")
        {
            //slidingScientist = other.gameObject;
            other.gameObject.GetComponent<Scientist>().slippingVelocity = other.gameObject.transform.forward * 10;
            other.gameObject.GetComponent<Scientist>().isSliding = true;
            slidingScientists.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Scientist>() != null && other.gameObject.tag != "Chemist")
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = other.gameObject.GetComponent<Scientist>().slippingVelocity;
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
        //if (slidingScientist != null)
        //{
        //    slidingScientist.gameObject.GetComponent<Scientist>().isSliding = false;
        //    slidingScientist = null;
        //}
        foreach(var slidingScientist in slidingScientists)
        {
            slidingScientist.gameObject.GetComponent<Scientist>().isSliding = false;
        }
        slidingScientists.Clear();
        Destroy(particleEffect);
        Destroy(gameObject);
    }
}

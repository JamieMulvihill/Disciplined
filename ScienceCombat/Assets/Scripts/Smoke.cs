using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float duration;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        //spawnTime = Time.time;
        StartCoroutine(DEATH());

    }

    // Update is called once per frame
    void Update()
    {
        //if (Time.time > spawnTime + duration)
        //{
            
        //    GetComponentInParent<SmokeAttack>().smokeExists = false;

        //    Destroy(gameObject);
        //}

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator DEATH()
    {
        
        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }
}

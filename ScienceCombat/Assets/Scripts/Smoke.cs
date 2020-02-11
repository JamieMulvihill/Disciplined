using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField] private GameObject particleEffectPrefab;
    [SerializeField] private float radius;
    [SerializeField] private float duration;
    private float spawnTime;
    private GameObject particleEffect;

    // Start is called before the first frame update
    void Start()
    {
        //spawnTime = Time.time;

        particleEffect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        StartCoroutine(deleteSelf());

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator deleteSelf()
    {
        yield return new WaitForSeconds(duration);
        Destroy(particleEffect);
        Destroy(gameObject);
    }
}

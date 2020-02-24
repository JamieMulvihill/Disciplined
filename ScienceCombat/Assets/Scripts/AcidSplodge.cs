using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSplodge : MonoBehaviour
{
    [SerializeField] private GameObject particleEffectPrefab;
    [SerializeField] private float duration;
    [SerializeField] private float radius;
    [SerializeField] private float damageInterval;
    [SerializeField] private float damage;
    private float lastTime;
    private GameObject particleEffect;
    private Collider[] hitObjecets;

    void Start()
    {
        lastTime = Time.time;
        gameObject.GetComponent<SphereCollider>().radius = radius;
        particleEffect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        StartCoroutine(deleteSelf());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        
        hitObjecets = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in hitObjecets)
        {

            if ((hit.gameObject.tag == "Biologist") ||
            (hit.gameObject.tag == "Physicist") ||
            (hit.gameObject.tag == "Engineer"))
            {
                // calling getcomponent way too much here...
                Health playerHealth = hit.GetComponent<Health>();
                if (Time.time > damageInterval + playerHealth.lastAcidDamageTime)
                {
                    playerHealth.TakeDamage(damage);
                    playerHealth.lastAcidDamageTime = Time.time;
                }
            }
        }
        //// Comparing strings is expensive...
        //if ((collision.gameObject.tag == "Biologist") ||
        //    (collision.gameObject.tag == "Physicist") ||
        //    (collision.gameObject.tag == "Engineer"))
        //{
        //    if (Time.time > damageInterval + lastTime)
        //    {
        //        // Getting component every frame...
        //        Health playerHealth = collision.GetComponent<Health>();
        //        playerHealth.TakeDamage(damage);
        //        lastTime = Time.time;
        //    }
        //}
    }

    IEnumerator deleteSelf()
    {
        yield return new WaitForSeconds(duration);
        Destroy(particleEffect);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

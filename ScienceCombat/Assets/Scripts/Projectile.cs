using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Collider[] hitObjecets;
    [SerializeField] private float damage;
    [SerializeField] private float damageRadius;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void AreaOfEffect(GameObject hitPlayer)
    {
        Health playerHealth = hitPlayer.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        rigidbody.velocity = Vector3.zero;
        hitObjecets = Physics.OverlapSphere(transform.position, damageRadius);


        if (collision.gameObject.tag != "Chemist")
        {
            AreaOfEffect(collision.gameObject);
            Destroy(gameObject);
        }

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }


}

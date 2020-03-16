using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float duration;
    public float damage;
    public float radius;
    public float deltaRadius;

    [SerializeField] private GameObject particleEffectPrefab;

    void Start()
    {
        gameObject.GetComponent<SphereCollider>().radius = radius;
        Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        StartCoroutine(deleteSelf());
    }

    private void OnTriggerEnter(Collider hitPlayer)
    {
        Health playerHealth = hitPlayer.GetComponent<Health>();

        if (playerHealth != null)
        {
            float distance = (hitPlayer.gameObject.transform.position - gameObject.transform.position).magnitude;
            Debug.Log(distance);

            // plus 2 to account for the size of the scientist.
            playerHealth.TakeDamage(damage * (radius + 2 - distance));
        }
    }

    IEnumerator deleteSelf()
    {
        radius += deltaRadius;
        gameObject.GetComponent<SphereCollider>().radius = radius;
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Acid : Projectile
{

    [SerializeField] private AcidSplodge acidSplodgePrefab;

    protected override void AreaOfEffect(GameObject hitPlayer)
    {
        //Health playerHealth = hitPlayer.GetComponent<Health>();
        //if (playerHealth != null)
        //{
        //    Debug.Log("Child class AreaOfEffect function.");
        //    playerHealth.TakeDamage(damage);
        //}

        Instantiate(acidSplodgePrefab, transform.position, Quaternion.identity);

    }

    private void OnCollisionEnter(Collision collision)
    {
        
                AreaOfEffect(collision.gameObject);
                Destroy(gameObject);
        

    }

}
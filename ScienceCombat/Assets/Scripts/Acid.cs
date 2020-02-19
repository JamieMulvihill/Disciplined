
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Acid : Projectile
{

    [SerializeField] private AcidSplodge acidSplodgePrefab;

    protected override void AreaOfEffect(GameObject hitPlayer)
    {
        if (hitPlayer.tag != "Virus" && hitPlayer.tag != "Vines" && hitPlayer.tag != "Acid" && hitPlayer.tag != "Fireball")
        {
            Instantiate(acidSplodgePrefab, transform.position, Quaternion.identity);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        
         AreaOfEffect(collision.gameObject);
         Destroy(gameObject);
        

    }

}
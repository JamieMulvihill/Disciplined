
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Acid : Projectile
{

    protected override void AreaOfEffect(GameObject hitPlayer)
    {
        Health playerHealth = hitPlayer.GetComponent<Health>();
        if (playerHealth != null)
        {
            Debug.Log("Child class AreaOfEffect function.");
            playerHealth.TakeDamage(damage);
        }
    }

   

}
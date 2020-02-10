using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Virus : Projectile
{
   
   protected override void AreaOfEffect(GameObject hitPlayer) {
        //Apply poisin to the health of hitPlayer;
        Health playerHealth = hitPlayer.GetComponent<Health>();
        if (playerHealth != null) {
            playerHealth.isPoisioned = true;
            playerHealth.PoisionDamage();
        }
    }

}

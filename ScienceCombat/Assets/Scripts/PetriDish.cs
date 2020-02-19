using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetriDish : Projectile
{
    [SerializeField] private GameObject virus;

    protected override void AreaOfEffect(GameObject hitPlayer)
    {
        if (hitPlayer.tag != "Virus" && hitPlayer.tag != "Vines")
        {
            Instantiate(virus, transform.position, Quaternion.identity);
        }
    }
   
}


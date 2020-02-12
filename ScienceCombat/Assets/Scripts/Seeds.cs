using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeds : Projectile
{
    [SerializeField] private GameObject vines;
    // Start is called before the first frame update
   protected override void AreaOfEffect(GameObject hitPlayer)
    {
      
        Instantiate(vines, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

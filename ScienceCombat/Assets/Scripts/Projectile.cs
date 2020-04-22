using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float damageRadius;

    protected virtual void AreaOfEffect(GameObject hitPlayer)
    {
        //This is a virtual function to be orverridden by the Child classes
    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag != gameObject.tag) {
            AreaOfEffect(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        
        if (collision.gameObject.tag != gameObject.tag)
        {
            AreaOfEffect(collision.gameObject);
        }

    }
}

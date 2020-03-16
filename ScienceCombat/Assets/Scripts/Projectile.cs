﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Collider[] hitObjecets;
    public float damage;
    public float damageRadius;

    // Start is called before the first frame update
    void Start()
    {

    }

    protected virtual void AreaOfEffect(GameObject hitPlayer)
    {
        //Debug.Log("Base class AreaOfEffect function.");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //hitObjecets = Physics.OverlapSphere(transform.position, damageRadius);
        //foreach (Collider hit in hitObjecets)
        //{

            if (collision.gameObject.tag != gameObject.tag) //(hit.tag != gameObject.tag)
            {
                AreaOfEffect(collision.gameObject);
                Destroy(gameObject);
            }
        //}

    }

    private void OnTriggerStay(Collider collision)
    {
        
        if (collision.gameObject.tag != gameObject.tag)
        {
            AreaOfEffect(collision.gameObject);
        }

    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, damageRadius);
    }


}

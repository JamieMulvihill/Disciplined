﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Virus : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float radius;
   
    void Start()
    {
        gameObject.GetComponent<SphereCollider>().radius = radius;
        StartCoroutine(deleteSelf());
    }

    private void OnTriggerEnter(Collider hitPlayer)
    {
        if (hitPlayer.gameObject.tag != "Biologist")
        {
            //Apply poisin to the health of hitPlayer;
            Health playerHealth = hitPlayer.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.isPoisioned = true;
                playerHealth.PoisionDamage();
            }
        }
    }

    IEnumerator deleteSelf()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

}

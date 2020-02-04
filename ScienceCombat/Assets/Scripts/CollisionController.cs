using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public HealthBarController healthbar;

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Enemy") {
            if (healthbar) {
                healthbar.onTakeDamage(10);
            }
        }
    }
}
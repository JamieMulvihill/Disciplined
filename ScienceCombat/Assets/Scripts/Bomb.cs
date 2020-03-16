using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float duration;



    protected override void AreaOfEffect(GameObject hitPlayer)
    {
        if (hitPlayer.tag != "Virus" && hitPlayer.tag != "Vines" && hitPlayer.tag != "Acid" && hitPlayer.tag != "Fireball" && hitPlayer.tag != "Punch Hitbox")
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Explosion explosionScript = explosion.GetComponent<Explosion>();
            explosionScript.damage = damage;
            explosionScript.radius = damageRadius;
            explosionScript.duration = duration;
        }
    }
}
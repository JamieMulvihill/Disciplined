using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeds : Projectile
{
    [SerializeField] private GameObject vines;
    [SerializeField] private GameObject[] vineList;
    private AudioSource sound;

   protected override void AreaOfEffect(GameObject hitPlayer){
        if (hitPlayer.tag == "Ground")
        {
            vineList = GameObject.FindGameObjectsWithTag("Vines");
            if (vineList.Length < 1) {
                Instantiate(vines, transform.position, Quaternion.identity);
                sound = GetComponent<AudioSource>();
                sound.Play();
                print(sound);
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Firehot : Projectile
{
    [SerializeField]private float maxDamage = 10f;
    [SerializeField]private float finalSize = 10f;
    [SerializeField] private float falloff = 10f;
    [SerializeField] private float minSize = 0f;

    //public void Initialise(float inputDamage, float inputFinalSize, float inputFalloff)
    //{
    //    maxDamage = inputDamage;
    //    finalSize = inputFinalSize;
    //    falloff = inputFalloff;
    //}
    protected override void AreaOfEffect(GameObject hitPlayer)
    {
        Health playerHealth = hitPlayer.gameObject.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage * Time.deltaTime);
        }
    }
    void Resize() //object scale approaches finalSize as current damage approaches 0
    {
        //merge test
        float resizeAmount = Mathf.Min(minSize + ((maxDamage - damage) / maxDamage), finalSize);
        gameObject.transform.localScale = Vector3.one * finalSize * resizeAmount;
    }
    void Awake()
    {
        damage = maxDamage;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // decrease damage by falloff * seconds
        // resize fireball
        // if damage is negative, destroy fireball
        damage -= falloff * Time.deltaTime;
        Resize();
        if (damage < 0f)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}

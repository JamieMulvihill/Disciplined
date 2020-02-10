using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Firehot : Projectile
{
    public float maxDamage = 10f;
    public float finalSize = 10f;
    public float falloff = 10f;

    public void Initialise(float inputDamage, float inputFinalSize, float inputFalloff)
    {
        maxDamage = inputDamage;
        finalSize = inputFinalSize;
        falloff = inputFalloff;
    }
    void Resize() //object scale approaches finalSize as current damage approaches 0
    {
        //merge test
        float resizeAmount = (maxDamage - damage) / maxDamage;
        gameObject.transform.localScale = Vector3.one * finalSize * resizeAmount;
    }
    void Awake()
    {
        damage = maxDamage;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

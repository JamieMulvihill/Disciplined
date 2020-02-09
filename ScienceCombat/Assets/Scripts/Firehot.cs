using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Firehot : MonoBehaviour
{
    private float damage = 10f;
    private float finalSize = 10f;
    private float falloff = 10f;
    private float currentDamage;
    public void Initialise(float inputDamage, float inputFinalSize, float inputFalloff)
    {
        damage = inputDamage;
        finalSize = inputFinalSize;
        falloff = inputFalloff;
    }
    void Resize() //object scale approaches finalSize as current damage approaches 0
    {
        //merge test
        float resizeAmount = (damage - currentDamage) / damage;
        gameObject.transform.localScale = Vector3.one * finalSize * resizeAmount;
    }
    void Awake()
    {
        currentDamage = damage;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentDamage -= falloff * Time.deltaTime;
        Resize();
        if (currentDamage < 0f)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}

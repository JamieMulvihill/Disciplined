using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Firehot : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float finalSize;
    [SerializeField] private float falloff;
    private float currentDamage;
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

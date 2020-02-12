using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float health = 100;
    public float damageGuiValue = 0;
    public float damageConversion;
    public float healthConversion;
    public float healthGuiValue;
    private float totalPoisionDmg = 30;
    private float currentPoisionDmg = 0;
    public bool isPoisioned;
    private bool runningCoroutine = false;

    // Start is called before the first frame update
    void Start()
    {
        isPoisioned = false;
        healthGuiValue = 255;
        healthConversion = (healthGuiValue / health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PoisionDamage() {

        if (!runningCoroutine && isPoisioned)  {
            runningCoroutine = true;  
            StartCoroutine(UpdatePoison());  
        }

    }

    public void TakeDamage(float damage)
    {
        if (damageGuiValue < 255)
        {
            damageGuiValue += healthConversion;
        }
        else { healthGuiValue -= healthConversion; }

        health -= damage;
    }

    IEnumerator UpdatePoison() {
        while (isPoisioned && totalPoisionDmg >= 1)    {
            health -= 1;
            totalPoisionDmg -= 1;
            if (damageGuiValue < 255)
            {
                damageGuiValue += healthConversion;
            }
            else { healthGuiValue -= healthConversion; }

            yield return new WaitForSeconds(.5f); 
        }

        isPoisioned = false;
        runningCoroutine = false;
        totalPoisionDmg = 30;
        yield break;
    }
}

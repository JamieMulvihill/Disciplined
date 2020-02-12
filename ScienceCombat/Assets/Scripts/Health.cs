using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] public float maxHealth = 100;
    public float redValue = 0;
    public float healthSegment;
    public float greenGuiValue;
    private float totalPoisionDmg = 30;
    private float currentPoisionDmg = 0;
    public bool isPoisioned;
    private bool runningCoroutine = false;

    // Start is called before the first frame update
    void Start()
    {
        isPoisioned = false;
        health = maxHealth;
        greenGuiValue = 255;
        healthSegment = (greenGuiValue / health);
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
        health -= damage;
        if (health > 50f)
        {
            redValue = healthSegment * (maxHealth - health ) * 2;
        }
        else { greenGuiValue = healthSegment * (health) * 2; }

    }

    IEnumerator UpdatePoison() {
        while (isPoisioned && totalPoisionDmg >= 1)    {
            health -= 1;
            totalPoisionDmg -= 1;
            if (health > 50f)
            {
                redValue = healthSegment * (maxHealth - health) * 2; 
            }
            else { greenGuiValue = healthSegment * (health) * 2; }
            yield return new WaitForSeconds(.5f); 
        }

        isPoisioned = false;
        runningCoroutine = false;
        totalPoisionDmg = 30;
        yield break;
    }
}

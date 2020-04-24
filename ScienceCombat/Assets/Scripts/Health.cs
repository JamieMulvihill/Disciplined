using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Variable Values
    [SerializeField] public float health;
    [SerializeField] public float maxHealth = 100;
    private float totalPoisionDmg = 30;
    public bool isPoisioned;
    public Image healthBar;
    private bool runningCoroutine = false;
    public float lastAcidDamageTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Values
        isPoisioned = false;
        health = maxHealth;
        healthBar.fillAmount = health / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the HealthBar UI
        healthBar.fillAmount = health / maxHealth;
    }

    // Function to start the Process of dealing poison damage
    public void PoisionDamage() {

        if (!runningCoroutine && isPoisioned)  {
            runningCoroutine = true;  
            StartCoroutine(UpdatePoison());  
        }
    }

    // Public function for removing health
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
   
    // Co-Routine to handle Poison functionality
    IEnumerator UpdatePoison() {
        // Check that the Player is poisoned and that there is still
        // poison damage to be done
        // repeat every half a second
        while (isPoisioned && totalPoisionDmg >= 1)    {
            health -= 5;
            totalPoisionDmg -= 5;
            yield return new WaitForSeconds(.5f); 
        }

        // set to no longer Poisoned and reset the total Poison damage
        isPoisioned = false;
        runningCoroutine = false;
        totalPoisionDmg = 30;
        yield break;
    }
}

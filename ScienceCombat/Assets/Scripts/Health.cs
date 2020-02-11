using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float health;
    private float totalPoisionDmg = 30;
    private float currentPoisionDmg = 0;
    public bool isPoisioned;
    private bool runningCoroutine = false;

    // Start is called before the first frame update
    void Start()
    {
        isPoisioned = false;
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
    }

    IEnumerator UpdatePoison() {
        while (isPoisioned && totalPoisionDmg >= 1)    {
            health -= 1;
            totalPoisionDmg -= 1;    

            yield return new WaitForSeconds(.5f); 
        }

        isPoisioned = false;
        runningCoroutine = false;
        totalPoisionDmg = 30;
        yield break;
    }
}

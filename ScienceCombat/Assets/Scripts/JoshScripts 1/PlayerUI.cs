﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private GameObject manager;
    [SerializeField] private GameObject healthUI;
    [SerializeField] private GameObject cd1UI;
    [SerializeField] private GameObject cd2UI;

    private UIManager UIMan;
    private Image healthbar;
    private Image cd1;
    private Image cd2;

    public float health;

    private float cooldownSpeed;

    private bool cd1ready;
    private bool cd2ready;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        UIMan = manager.GetComponent<UIManager>();
        cd1 = cd1UI.GetComponent<Image>();
        cd2 = cd2UI.GetComponent<Image>();
        cooldownSpeed = 5;
        cd1ready = true;
        cd2ready = true;
        health = 1;
    }


    void Update()
    {
        //getHealth();
        //setHealth();

        if(cd1ready == false)
        {
            cd1.fillAmount += 1 / cooldownSpeed * Time.deltaTime;

            if (cd1.fillAmount >= 1)
            {
                cd1ready = true;
            }
        }

        if (cd2ready == false)
        {
            cd2.fillAmount += 1 / cooldownSpeed * Time.deltaTime;

            if (cd2.fillAmount >= 1)
            {
                cd2ready = true;
            }
        }

        if(UIMan.cooldown1)
        {
            UIMan.cooldown1 = false;
            if (cd1ready)
            {
                cd1.fillAmount = 0;
                cd1ready = false;
            }
        }

        if (UIMan.cooldown2)
        {
            UIMan.cooldown2 = false;
            if (cd2ready)
            {
                cd2.fillAmount = 0;
                cd2ready = false;
            }
        }
    }

    void getHealth()
    {
        // health = assigned scientists health;
    }

    void setHealth()
    {
        healthbar.fillAmount = health;
    }
}

/*float health, float cooldowns 1/2, bool cooldowns 1/2 ready
 * 
 * when cooldown is used, set the fill amount to 0
 */
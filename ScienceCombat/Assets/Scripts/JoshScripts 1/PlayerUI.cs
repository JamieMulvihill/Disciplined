﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private GameObject manager;
    public GameObject connectedPlayer;
    [SerializeField] private GameObject healthUI;
    [SerializeField] private GameObject cd1UI;
    [SerializeField] private GameObject cd2UI;

    private UIManager UIMan;
    private Image healthbar;
    private Image cd1;
    private Image cd2;
    private Health health;
    private Flammenwerfer engineerAttack;
    private Laser physicistAttack;

    private float healthValue;

    //private float cooldownSpeed;

    private bool cd1ready;
    private bool cd2ready;
    private bool engineer;
    private bool physicist;
    private bool attackOnCooldown;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        UIMan = manager.GetComponent<UIManager>();
        health = connectedPlayer.GetComponent<Health>();
        cd1 = cd1UI.GetComponent<Image>();
        cd2 = cd2UI.GetComponent<Image>();
        healthbar = healthUI.GetComponent<Image>();
        //cooldownSpeed = 5;
        cd1ready = true;
        cd2ready = true;
        healthValue = 1;
        attackOnCooldown = false;
        engineer = false;
        physicist = false;
        Setup();
    }

    void Setup()
    {
        if(connectedPlayer.tag == "Engineer")
        {
            engineerAttack = connectedPlayer.GetComponent<Flammenwerfer>();
            engineerAttack.overheat = new Overheat();
            engineer = true;
        }

        if (connectedPlayer.tag == "Physicist")
        {
            physicistAttack = connectedPlayer.GetComponent<Laser>();
            physicistAttack.overheat = new Overheat();
            physicist = true;
        }
    }


    void Update()
    {
        SetHealth();

        if (engineer == true)
        {
            if (!attackOnCooldown)
            {
                cd1.fillAmount = engineerAttack.overheat.GetHeatFraction();
            }
        }

        if (physicist == true)
        {
            if (!attackOnCooldown)
            {
                cd1.fillAmount = physicistAttack.overheat.GetHeatFraction();
            }
        }
    }

    public void UIAbilityCooldown1(float _duration)
    {
        cd1.fillAmount = 0;
        while (cd1ready == false)
        {
            cd1.fillAmount += 1 / _duration * Time.deltaTime;

            if (cd1.fillAmount >= 1)
            {
                cd1ready = true;
            }
        }
    }

    public void UIAbilityCooldown2(float _duration)
    {
        cd2.fillAmount = 0;
        while (cd2ready == false)
        {
            cd2.fillAmount += 1 / _duration * Time.deltaTime;

            if (cd2.fillAmount >= 1)
            {
                cd2ready = true;
            }
        }
    }

    void SetHealth()
    {
        healthbar.fillAmount = health.health / 100;
    }

    //public void ConnectPlayer()
    //{
    //    health = connectedPlayer.GetComponent<Health>();
    //}
}


//if (cd1ready == false)
//{
//    cd1.fillAmount += 1 / cooldownSpeed * Time.deltaTime;

//    if (cd1.fillAmount >= 1)
//    {
//        cd1ready = true;
//    }
//}

//if (cd2ready == false)
//{
//    cd2.fillAmount += 1 / cooldownSpeed * Time.deltaTime;

//    if (cd2.fillAmount >= 1)
//    {
//        cd2ready = true;
//    }
//}

//if(UIMan.cooldown1)
//{
//    UIMan.cooldown1 = false;
//    if (cd1ready)
//    {
//        cd1.fillAmount = 0;
//        cd1ready = false;
//    }
//}

//if (UIMan.cooldown2)
//{
//    UIMan.cooldown2 = false;
//    if (cd2ready)
//    {
//        cd2.fillAmount = 0;
//        cd2ready = false;
//    }
//}
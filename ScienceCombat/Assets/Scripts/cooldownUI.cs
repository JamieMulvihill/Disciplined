using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldownUI : MonoBehaviour
{
    public Slider slider;

    public int playerIndex;
    public int primarySecondary;

    private bool firstUpdate = true;
    GameObject scientist;

    private void InitialiseScientist()
    {
        Scientist[] scientists;
        scientists = FindObjectsOfType<Scientist>();
        for (int i = 0; i < scientists.Length; i++)
        {
            if (scientists[i].gameObject.tag == "Chemist" && playerIndex == 0)
            {
                scientist = scientists[i].gameObject;
                break;
            }
            else if (scientists[i].gameObject.tag == "Engineer" && playerIndex == 1)
            {
                scientist = scientists[i].gameObject;
                break;
            }
            else if (scientists[i].gameObject.tag == "Biologist" && playerIndex == 2)
            {
                scientist = scientists[i].gameObject;
                break;
            }
            else if (scientists[i].gameObject.tag == "Physicist" && playerIndex == 3)
            {
                scientist = scientists[i].gameObject;
                break;
            }
        }
        firstUpdate = false;
    }
    
    private void UpdateScientist()
    {
        if (scientist.tag == "Chemist")
        {
            if (primarySecondary == 0)
            {
                slider.maxValue = scientist.GetComponent<ProjectileLauncher>().fireRate;
                slider.value = slider.maxValue - (Time.time - scientist.GetComponent<ProjectileLauncher>().lastShotTime);
            }
            if (primarySecondary == 1)
            {
                slider.maxValue = 1;
                slider.value = scientist.GetComponent<IceAttack>().overheat.GetHeatFraction();
            }
            return;
        }
        else if (scientist.tag == "Engineer")
        {
            if (primarySecondary == 0)
            {
                slider.maxValue = 1;
                slider.value = scientist.GetComponent<Flammenwerfer>().overheat.GetHeatFraction();
            }
            if (primarySecondary == 1)
            {
                slider.maxValue = 1;
                slider.value = scientist.GetComponent<RoboticArm>().overheat.GetHeatFraction();
            }
            return;
        }
        else if (scientist.tag == "Biologist")
        {
            if (primarySecondary == 0)
            {
                slider.maxValue = scientist.GetComponent<ProjectileLauncher>().fireRate;
                slider.value = slider.maxValue - (Time.time - scientist.GetComponent<ProjectileLauncher>().lastShotTime);
            }
            if (primarySecondary == 1)
            {
                slider.maxValue = scientist.GetComponent<ProjectileLauncher>().fireRate;
                slider.value = slider.maxValue - (Time.time - scientist.GetComponent<ProjectileLauncher>().lastShotTime);
            }
            Debug.Log(slider.maxValue);
            return;
        }
        else if (scientist.tag == "Physicist")
        {
            if (primarySecondary == 0)
            {
                slider.maxValue = 1;
                slider.value = scientist.GetComponent<Laser>().overheat.GetHeatFraction();
            }
            if (primarySecondary == 1)
            {
                slider.maxValue = 1;
                slider.value = scientist.GetComponent<Dash>().overheat.GetHeatFraction();
            }
            return;
        }
    }

    private void Start()
    {
        slider.value = 0;
    }



    private void Update()
    {
        if (!firstUpdate)
        {
            UpdateScientist();
        }
    }

    private void LateUpdate()
    {
        if (firstUpdate) // after scientists are spawned
        {
            InitialiseScientist();
        }
    }



}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class cooldownUI : MonoBehaviour
//{
//    public int playerIndex;

//    PlayerContainer playerContainer;

//    Scientist scientist = null;

//    private void Start()
//    {

//    }

//    private void Update()
//    {
//        var scientists = FindObjectsOfType<Scientist>();



//        //playerContainer = FindObjectOfType<PlayerContainer>();

//        //Character character = playerContainer.chosenPlayers[playerIndex];
//        //scientist = character.Scientist.GetComponent<Scientist>();
//        //Debug.Log("scientist0:" + scientists[0].gameObject.GetInstanceID());
//        //Debug.Log("scientist1:" + scientists[1].gameObject.GetInstanceID());
//        //Debug.Log("scientist2:" + scientists[2].gameObject.GetInstanceID());
//        //Debug.Log("scientist3:" + scientists[3].gameObject.GetInstanceID());
//        //Debug.Log("scientist4:" + scientists[4].gameObject.GetInstanceID());

//        //Debug.Log("playerContainer0:" + playerContainer.chosenPlayers[playerIndex].Scientist.GetInstanceID());

//        //scientist = scientists[]

//        Debug.Log("test: " + scientists[0].test);

//    }



//}

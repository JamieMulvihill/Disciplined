using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldownUI : MonoBehaviour
{
    public Slider slider;

    public int playerIndex;

    private bool firstUpdate = true;

    Scientist [] scientists;

    private void Start()
    {
        var scientists = FindObjectsOfType<Scientist>();


        
        
        slider.value = 0;


        //scientist.GetComponent<Flammenwerfer>().overheat = new Overheat();
    }



    private void Update()
    {
        if (!firstUpdate)
        {
            slider.value = Time.time - scientists[0].GetComponent<ProjectileLauncher>().lastShotTime;
            Debug.Log(slider.value);
        }
    }

    private void LateUpdate()
    {
        if (firstUpdate)
        {
            scientists = FindObjectsOfType<Scientist>();
            slider.maxValue = scientists[0].gameObject.GetComponent<ProjectileLauncher>().fireRate;
            firstUpdate = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class grantUI : MonoBehaviour
{
    [SerializeField] private Text grantText;

    public int playerIndex;

    public bool firstUpdate = true;
    GameObject scientist;

    private void InitialiseScientist()
    {
        Scientist[] scientists;
        scientists = FindObjectsOfType<Scientist>();
        for (int i = 0; i < scientists.Length - 4; i++)
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
        string asdf = scientist.GetComponent<Scientist>().grantEarnings.ToString();
        grantText.text = ((int)scientist.GetComponent<Scientist>().grantEarnings).ToString() + "K";
        Debug.Log(grantText.text);
    }

    private void Start()
    {
        
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

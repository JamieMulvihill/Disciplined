using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
   public Character[] chosenPlayers;
   void Awake()
   {

       GameObject[] objs = GameObject.FindGameObjectsWithTag("Spawner");

       if (objs.Length > 1)
       {
           Destroy(gameObject);
       }

       DontDestroyOnLoad(gameObject);
   }
   
}

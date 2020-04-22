using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldownUI : MonoBehaviour
{
    public int playerIndex;

    PlayerContainer playerContainer;

    Scientist scientist = null;

    private void Start()
    {
        playerContainer = FindObjectOfType<PlayerContainer>();

        Character character = playerContainer.chosenPlayers[playerIndex];
        scientist = character.Scientist.GetComponent<Scientist>();
    }

    private void Update()
    {
        Debug.Log(scientist.test);

    }



}

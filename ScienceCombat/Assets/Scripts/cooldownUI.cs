using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldownUI : MonoBehaviour
{
    public Slider slider;

    public int playerIndex;

    PlayerContainer playerContainer;

    private void Start()
    {
        slider.value = slider.maxValue;
        playerContainer = FindObjectOfType<PlayerContainer>();

        Character character = playerContainer.GetComponent<PlayerContainer>().chosenPlayers[playerIndex];
        GameObject scientist = character.Scientist;
        scientist.GetComponent<Flammenwerfer>().overheat = new Overheat();
    }

    private void Update()
    {
        if (playerContainer.GetComponent<PlayerContainer>().chosenPlayers.Length >= playerIndex)
        {
            Character character = playerContainer.GetComponent<PlayerContainer>().chosenPlayers[playerIndex];
            GameObject scientist = character.Scientist;
            if (scientist.GetComponent<Flammenwerfer>().overheat != null)
            {
                slider.value = scientist.GetComponent<Flammenwerfer>().overheat.GetHeatFraction();
                Debug.Log(scientist.GetComponent<Flammenwerfer>().overheat.GetHeat());
            }
           
        }
    }



}

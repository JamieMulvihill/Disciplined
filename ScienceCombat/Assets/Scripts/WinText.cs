using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinText : MonoBehaviour
{
    public Text[] placeLabels;
    private Scoreboard scoreboard;
    private List<string> playerTags;
    private List<float> playerScores;
    int counter = 0;
    private void Start()
    {
       // placeLabels = new Text[4];
        playerTags = new List<string>();
        playerScores = new List<float>();

        scoreboard = FindObjectOfType<Scoreboard>();
        foreach (var score in scoreboard.scores)
        {
            string tag = score.Key;
            float result = score.Value;
            playerScores.Add(result);
        }

        playerScores.Sort((x, y) => x.CompareTo(y));

        while (counter <= 3)
        {
            foreach (var score in scoreboard.scores)
            {
                if (playerScores[counter] == score.Value)
                {
                    playerTags.Add(score.Key);
                    counter++;
                }
            }
        }
        for (int i = 0; i < playerTags.Count; i++)
        {
            placeLabels[i].text = playerTags[i];
        }
        //scoreboard.spawner.chosenPlayers.Length(); 
    }
    private void FuntionName() { 
        
    }
}

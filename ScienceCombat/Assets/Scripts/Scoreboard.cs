using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    private PlayerContainer spawner;
    public List<Scientist> scientists;
    public List<Scientist> deadScientists;
    public List<float> currentScores;
    public Dictionary<string, float> scores;
    private void Start()
    {
        
        currentScores = new List<float>();
        deadScientists = new List<Scientist>();
        spawner = FindObjectOfType<PlayerContainer>();
        scores = new Dictionary<string, float>();
        //for (int i = 0; i < spawner.chosenPlayers.Length; i++)
        //{
        //    scientists.Add(spawner.chosenPlayers[i].Scientist.GetComponent<Scientist>());
        //}
        for (int i = 0; i < scientists.Count; i++) {
            scores.Add(scientists[i].gameObject.tag, scientists[i].grantEarnings);        
        }

        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        scientists.Sort((Scientist a, Scientist b) => a.grantEarnings.CompareTo(b.grantEarnings));

        //scientists.Sort((Scientist a, Scientist b) =>
        //{
        //    return a.grantEarnings > b.grantEarnings ? -1 : 1;
        //});
    }

    public void KilledPlayer(GameObject newScientist) {
        // store score, remove from list, set score to stored score and add to lsit
        
    }
}

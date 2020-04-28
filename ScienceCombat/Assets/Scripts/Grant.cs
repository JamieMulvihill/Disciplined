using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Grant : MonoBehaviour
{
    public float grantValue = 50f;
    public bool isPossessed = false;
    public Scientist scientist;
    public NavMeshAgent navAgent;
    public float range = 5.0f;
    Vector3 point;
    public bool reachedDestination = true;
    public Canvas winCan;
    [SerializeField] private GameObject WinChar;
    public GameObject winner = null;

    // Update is called once per frame
    void Update() {

        MaxedOutGrant();

        if (isPossessed && grantValue > 0) {
            grantValue -= 1 * Time.deltaTime;
            scientist.EarningGrant(1 * Time.deltaTime);
            transform.position = new Vector3(scientist.transform.position.x, 4, scientist.transform.position.z);
        }
        if (!isPossessed && reachedDestination)
        {
           
            if (RandomPoint(transform.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            }
            navAgent.SetDestination(point);
            reachedDestination = false;
        }
        if (Mathf.Abs(transform.position.magnitude - point.magnitude) < .5f) {
            reachedDestination = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPossessed)
        {
            scientist = other.gameObject.GetComponent<Scientist>();
            if (scientist)
            {
                isPossessed = true;
                scientist.grant = this;
                Debug.Log(scientist.gameObject.tag);
            }
        }
    }
    
    private void MaxedOutGrant() {
        if (grantValue <= 0) {
            // Get all the Scientists in the game
            // iterate through them and find which has best score
            // store the winning scor and postion in array
            Scientist[] scientistis = FindObjectsOfType<Scientist>();
            float winnerScore = 0;
            int winnerIndex = 0;
            for (int i = 0; i < scientistis.Length; i++) {
                scientistis[i].isCaptured = true;
                float playerScore = scientistis[i].grantEarnings;
                if (playerScore > winnerScore)
                {
                    winnerScore = playerScore;
                    winnerIndex = i;
                }
            }

            winner = scientistis[winnerIndex].gameObject;
            winCan.enabled = true;
            winner.GetComponent<Animator>().Play("dance");
            scientist.hasGrant = false;
            //isPossessed = false;
            scientist = null;
            Destroy(gameObject);

            //End the round and load next scene, this will be scoreboard of rounds or win screen if end game
            //SceneManager.LoadScene("WinState");

            

        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            // Get a point within a UnitSphere mutiplied by range variable frome the centre variable.
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            // Check that the point is a point on the NavMesh
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                // Store the result
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}

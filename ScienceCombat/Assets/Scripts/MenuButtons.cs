using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public Canvas canvas;
    [SerializeField] private GameObject audio;

    // Start is called before the first frame update
    void Start()
    {
        print("Start");
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Play()
    {
        SceneManager.LoadScene(1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //print("Play");
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    private IEnumerator Wait()
    {
        print("waiting");
        yield return new WaitForSeconds(30.75f);
        print("waited");
        audio.SetActive(true);
        canvas.GetComponent<Canvas>().enabled = true;
        
    }


}

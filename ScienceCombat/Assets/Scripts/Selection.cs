using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Selection : MonoBehaviour
{
    [SerializeField] private GameObject characterSelector;
    [SerializeField] private Image characterSprite;
    [SerializeField] private PlayerContainer playerContainer;
    [SerializeField] private GameObject MenuChar;
    public GameObject CharPosition;
    private int indexPosition = 0;
    private CharacterSelect selector;
    float inputTime = 0.0f;
    float lastInput = 0.0f;
    bool selectedPlayer = false;
    public int playerNum;
    [SerializeField] private string selectButton;   // Button to select scientist.
    [SerializeField] private string startButton;    // Button to start the game.

    // Start is called before the first frame update
    void Start()
    {

        selector = characterSelector.GetComponent<CharacterSelect>();
        characterSprite.sprite = selector.playabeCharacters[indexPosition].ScientistSprite;
        //MenuChar = Instantiate(selector.playabeCharacters[indexPosition].Scientist);
        //MenuChar.transform.localScale = new Vector3(100,100,100);
        //MenuChar.GetComponent<SkinnedMeshRenderer>().sharedMesh = selector.playabeCharacters[indexPosition].MenuCharacter.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        //MenuChar.GetComponent<SkinnedMeshRenderer>().bones = selector.playabeCharacters[indexPosition].MenuCharacter.GetComponent<SkinnedMeshRenderer>().bones;

        MenuChar = Instantiate(selector.playabeCharacters[indexPosition].MenuCharacter);
        MenuChar.transform.position = CharPosition.transform.position;
        MenuChar.transform.localScale = new Vector3(500, 500, 500);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!selectedPlayer)
            characterSprite.sprite = selector.playabeCharacters[ChoosingCharacter()].ScientistSprite;


        SelectingChoice();
    }
    int ChoosingCharacter()
    {

        if (!selectedPlayer)
        {
            if (Input.GetAxis("Horizontal" + playerNum.ToString()) > .5)
            {
                inputTime = Time.time;
                if (inputTime - lastInput > .3)
                {

                    if (indexPosition < selector.playabeCharacters.Count - 1)
                    {
                        indexPosition++;
                        lastInput = Time.time;


                        Destroy(MenuChar);
                        MenuChar = Instantiate(selector.playabeCharacters[indexPosition].MenuCharacter);
                        MenuChar.transform.position = CharPosition.transform.position;
                        MenuChar.transform.localScale = new Vector3(500, 500, 500);

                    }

                    else
                    {
                        indexPosition = 0;
                        lastInput = Time.time;


                        Destroy(MenuChar);
                        MenuChar = Instantiate(selector.playabeCharacters[indexPosition].MenuCharacter);
                        MenuChar.transform.position = CharPosition.transform.position;
                        MenuChar.transform.localScale = new Vector3(500, 500, 500);
                    }
                }
            }

            if (Input.GetAxis("Horizontal" + playerNum.ToString()) < -.5)
            {

                inputTime = Time.time;
                if (inputTime - lastInput > .3)
                {
                    
                    if (indexPosition > 0)
                    {
                        indexPosition--;
                        lastInput = Time.time;

                        Destroy(MenuChar);
                        MenuChar = Instantiate(selector.playabeCharacters[indexPosition].MenuCharacter);
                        MenuChar.transform.position = CharPosition.transform.position;
                        MenuChar.transform.localScale = new Vector3(500, 500, 500);
                    }

                    else
                    {
                        indexPosition = selector.playabeCharacters.Count - 1;
                        lastInput = Time.time;


                        Destroy(MenuChar);
                        MenuChar = Instantiate(selector.playabeCharacters[indexPosition].MenuCharacter);
                        MenuChar.transform.position = CharPosition.transform.position;
                        MenuChar.transform.localScale = new Vector3(500, 500, 500);
                    }

                }
            }
        }
        return indexPosition;
    }
    void SelectingChoice()
    {

        if (!selectedPlayer)
        {
            //Mesh = Instantiate(selector.playabeCharacters[indexPosition].Scientist);
            //Mesh.transform.localScale = new Vector3(100, 100, 100);
            //Mesh.GetComponent<SkinnedMeshRenderer>().sharedMesh = selector.playabeCharacters[indexPosition].Scientist.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            //Mesh.GetComponent<SkinnedMeshRenderer>().bones = selector.playabeCharacters[indexPosition].Scientist.GetComponent<SkinnedMeshRenderer>().bones;

            //if (Input.GetKeyDown($"joystick {playerNum} button 0"))
            if(Input.GetButtonDown(selectButton + playerNum.ToString()))
            {
                selectedPlayer = true;
                playerContainer.chosenPlayers[playerNum] = selector.playabeCharacters[ChoosingCharacter()];
                selector.playabeCharacters.RemoveAt(ChoosingCharacter());
                characterSprite.sprite = playerContainer.chosenPlayers[playerNum].ScientistSprite;
            }
        }
        if (selectedPlayer)
        {
            //if (Input.GetKeyDown($"joystick {playerNum} button 1"))
            if (Input.GetButtonDown(startButton + playerNum.ToString()))
            {
                //SceneManager.LoadScene("Jamie");
                //SceneManager.LoadScene("Josh's");
                SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
            }
        }
    }
}


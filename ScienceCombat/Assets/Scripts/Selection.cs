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
    public GameObject characterPosition;
    private int indexPosition = 0;
    private CharacterSelect selector;
    float inputTime = 0.0f;
    float lastInput = 0.0f;
    bool selectedPlayer = false;
    public int playerNum;
    [SerializeField] private string selectButton;   // Button to select scientist.
    [SerializeField] private string startButton;    // Button to start the game.
    private Animator Anim;

    // Start is called before the first frame update
    void Start()
    {

        selector = characterSelector.GetComponent<CharacterSelect>();
        characterSprite.sprite = selector.playabeCharacters[indexPosition].ScientistSprite;
        MenuChar = Instantiate(selector.playabeCharacters[indexPosition].MenuCharacter);
        MenuChar.transform.position = characterPosition.transform.position;
        MenuChar.transform.localScale = new Vector3(500, 500, 500);
    }

   
    void LateUpdate()
    {
        if (!selectedPlayer)
            characterSprite.sprite = selector.playabeCharacters[ChoosingCharacter()].ScientistSprite;
        Anim = MenuChar.GetComponent<Animator>();

        SelectingChoice();
    }

    // Function to determine which element of the List of Characters is to be displayed On screen
    int ChoosingCharacter()
    {
        // CHeck if a player is selected
        if (!selectedPlayer)
        {
            //  Check if the input from the controller is in the right direction 
            if (Input.GetAxis("Horizontal" + playerNum.ToString()) > .5)
            {
                // check the time since last input is greater than the delay time
                inputTime = Time.time;
                if (inputTime - lastInput > .3)
                {
                    // check which element of the list is being displayed,
                    // increase the indexPosition, store the time and destroy the GameObject currently Displayed
                    // Instantiate the next GameObject based on the new IndexPosition, set its postion and Scale.
                    if (indexPosition < selector.playabeCharacters.Count - 1)
                    {
                        indexPosition++;
                        lastInput = Time.time;
                        Destroy(MenuChar);
                        MenuChar = Instantiate(selector.playabeCharacters[indexPosition].MenuCharacter);
                        MenuChar.transform.position = characterPosition.transform.position;
                        MenuChar.transform.localScale = new Vector3(500, 500, 500);

                    }

                    // set index back to 0 to start again, store the time
                    // destroy the GameObject currently Displayed
                    // Instantiate the next GameObject based on the new IndexPosition, set its postion and Scale.
                    else
                    {
                        indexPosition = 0;
                        lastInput = Time.time;
                        Destroy(MenuChar);
                        MenuChar = Instantiate(selector.playabeCharacters[indexPosition].MenuCharacter);
                        MenuChar.transform.position = characterPosition.transform.position;
                        MenuChar.transform.localScale = new Vector3(500, 500, 500);
                    }
                }
            }

            // check if the input from the controller is in the left direction
            if (Input.GetAxis("Horizontal" + playerNum.ToString()) < -.5)
            {
                // check the time since last input is greater than the delay time
                inputTime = Time.time;
                if (inputTime - lastInput > .3)
                {
                    // check which element of the list is being displayed,
                    // decrease the indexPosition, store the time and destroy the GameObject currently Displayed
                    // Instantiate the next GameObject based on the new IndexPosition, set its postion and Scale.
                    if (indexPosition > 0)
                    {
                        indexPosition--;
                        lastInput = Time.time;
                        Destroy(MenuChar);
                        MenuChar = Instantiate(selector.playabeCharacters[indexPosition].MenuCharacter);
                        MenuChar.transform.position = characterPosition.transform.position;
                        MenuChar.transform.localScale = new Vector3(500, 500, 500);
                    }

                    else
                    {
                        indexPosition = selector.playabeCharacters.Count - 1;
                        lastInput = Time.time;
                        Destroy(MenuChar);
                        MenuChar = Instantiate(selector.playabeCharacters[indexPosition].MenuCharacter);
                        MenuChar.transform.position = characterPosition.transform.position;
                        MenuChar.transform.localScale = new Vector3(500, 500, 500);
                    }

                }
            }
        }
        //return the value of index postion to control which element of the list of character to display
        return indexPosition;
    }
    void SelectingChoice()
    {

        if (!selectedPlayer)
        {
            if(Input.GetButtonDown(selectButton + playerNum.ToString()))
            {
                selectedPlayer = true;
                playerContainer.chosenPlayers[playerNum] = selector.playabeCharacters[ChoosingCharacter()];
                selector.playabeCharacters.RemoveAt(ChoosingCharacter());
                characterSprite.sprite = playerContainer.chosenPlayers[playerNum].ScientistSprite;

                Anim.Play("Selected");
            }
        }
        if (selectedPlayer)
        {
            if (Input.GetButtonDown(startButton + playerNum.ToString()))
            {
                //SceneManager.LoadScene("Jamie");
                //SceneManager.LoadScene("Josh's");
                SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
            }
        }
    }
}


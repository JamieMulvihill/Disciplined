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
    private int indexPosition = 0;
    private CharacterSelect selector;
    float inputTime = 0.0f;
    float lastInput = 0.0f;
    bool selectedPlayer = false;
    public int playerNum;

    public GameObject Mesh;

    // Start is called before the first frame update
    void Start()
    {

        selector = characterSelector.GetComponent<CharacterSelect>();
        characterSprite.sprite = selector.playabeCharacters[indexPosition].ScientistSprite;
        
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
                    }

                    else
                    {
                        indexPosition = 0;
                        lastInput = Time.time;
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
                    }

                    else
                    {
                        indexPosition = selector.playabeCharacters.Count - 1;
                        lastInput = Time.time;
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
            //Mesh.GetComponent<SkinnedMeshRenderer>().sharedMesh = selector.playabeCharacters[indexPosition].Scientist.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            //Mesh.GetComponent<MeshRenderer>().sharedMaterial = selector.playabeCharacters[indexPosition].Scientist.GetComponent<MeshRenderer>().sharedMaterial;

            if (Input.GetKeyDown($"joystick {playerNum} button 0"))
            {
                selectedPlayer = true;
                playerContainer.chosenPlayers[playerNum - 1] = selector.playabeCharacters[ChoosingCharacter()];
                selector.playabeCharacters.RemoveAt(ChoosingCharacter());
                characterSprite.sprite = playerContainer.chosenPlayers[playerNum - 1].ScientistSprite;
            }
        }
        if (selectedPlayer)
        {
            if (Input.GetKeyDown($"joystick {playerNum} button 1"))
            {
                SceneManager.LoadScene("Map V3");
            }
        }
    }
}


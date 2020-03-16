using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Selection : MonoBehaviour
{
    [SerializeField] private GameObject characterSelector;
    [SerializeField] private Image characterSprite;
    [SerializeField] private GameObject characterMesh;
    [SerializeField] private PlayerContainer playerContainer;
    private int indexPosition = 0;
    private CharacterSelect selector;
    float inputTime = 0.0f;
    float lastInput = 0.0f;
    bool selectedPlayer = false;
    public int playerNum;

    

    // Start is called before the first frame update
    void Start()
    {

        selector = characterSelector.GetComponent<CharacterSelect>();
        characterSprite.sprite = selector.playabeCharacters[indexPosition].ScientistSprite;

        selector.playabeCharacters[indexPosition].Scientist.transform.position = characterSprite.transform.position;
        selector.playabeCharacters[indexPosition].Scientist.transform.localScale = new Vector3(100, 100, 100);
        selector.playabeCharacters[indexPosition].Scientist.GetComponent<Rigidbody>().isKinematic = true;
        selector.playabeCharacters[indexPosition].Scientist.GetComponent<Rigidbody>().useGravity = false;

        characterMesh = Instantiate(selector.playabeCharacters[indexPosition].Scientist);


        //characterMesh.GetComponent<SkinnedMeshRenderer>().sharedMesh = selector.playabeCharacters[indexPosition].Scientist.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        //characterMesh.GetComponent<SkinnedMeshRenderer>().bones = selector.playabeCharacters[indexPosition].Scientist.GetComponent<SkinnedMeshRenderer>().bones;
        //characterMesh.GetComponent<SkinnedMeshRenderer>().sharedMesh = new CharacterSelect().playabeCharacters[indexPosition].Scientist.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        //characterMesh.GetComponent<SkinnedMeshRenderer>().bones = new CharacterSelect().playabeCharacters[indexPosition].Scientist.GetComponent<SkinnedMeshRenderer>().bones;



        // Mesh.GetComponent<SkinnedMeshRenderer>().gameObject.transform.position = characterSprite.transform.position;

        //Mesh = selector.playabeCharacters[indexPosition].Scientist;

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
                Destroy(characterMesh);

                selector.playabeCharacters[indexPosition].Scientist.transform.position = characterSprite.transform.position;
                selector.playabeCharacters[indexPosition].Scientist.transform.localScale = new Vector3(100, 100, 100);
                selector.playabeCharacters[indexPosition].Scientist.GetComponent<Rigidbody>().isKinematic = true;
                selector.playabeCharacters[indexPosition].Scientist.GetComponent<Rigidbody>().useGravity = false;

                characterMesh = Instantiate(selector.playabeCharacters[indexPosition].Scientist);

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

                Destroy(characterMesh);

                selector.playabeCharacters[indexPosition].Scientist.transform.position = characterSprite.transform.position;
                selector.playabeCharacters[indexPosition].Scientist.transform.localScale = new Vector3(100, 100, 100);
                selector.playabeCharacters[indexPosition].Scientist.GetComponent<Rigidbody>().isKinematic = true;
                selector.playabeCharacters[indexPosition].Scientist.GetComponent<Rigidbody>().useGravity = false;

                characterMesh = Instantiate(selector.playabeCharacters[indexPosition].Scientist);

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
            //Mesh = Instantiate(selector.playabeCharacters[indexPosition].Scientist);
            //Mesh.transform.localScale = new Vector3(100, 100, 100);

            //characterMesh.GetComponent<SkinnedMeshRenderer>().sharedMesh = selector.playabeCharacters[indexPosition].Scientist.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            //characterMesh.GetComponent<SkinnedMeshRenderer>().bones = selector.playabeCharacters[indexPosition].Scientist.GetComponent<SkinnedMeshRenderer>().bones;

            //characterMesh.GetComponent<SkinnedMeshRenderer>().sharedMesh = new CharacterSelect().playabeCharacters[indexPosition].Scientist.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            //characterMesh.GetComponent<SkinnedMeshRenderer>().bones = new CharacterSelect().playabeCharacters[indexPosition].Scientist.GetComponent<SkinnedMeshRenderer>().bones;

            //Mesh = selector.playabeCharacters[indexPosition].Scientist;

            if (Input.GetKeyDown($"joystick {playerNum} button 0"))
            {
                selectedPlayer = true;
                playerContainer.chosenPlayers[playerNum - 1] = selector.playabeCharacters[ChoosingCharacter()];
                selector.playabeCharacters[indexPosition].Scientist.transform.position = Vector3.zero;
                selector.playabeCharacters.RemoveAt(ChoosingCharacter());
                characterSprite.sprite = playerContainer.chosenPlayers[playerNum - 1].ScientistSprite;

            }
        }
        if (selectedPlayer)
        {
            if (Input.GetKeyDown($"joystick {playerNum} button 1"))
            {
                SceneManager.LoadScene("Jamie");
            }
        }
       
    }
}


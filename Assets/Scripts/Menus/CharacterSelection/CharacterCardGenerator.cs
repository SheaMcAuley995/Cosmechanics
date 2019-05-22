using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct CharacterData
{
    // Display fields on the character card
    public Text nameField;

    // Constructor for creating new character cards
    public CharacterData(Text _nameField)
    {
        nameField = _nameField;
    }
}

public class CharacterCardGenerator : MonoBehaviour
{
    public enum CharacterStatus { SELECTING, READY };
    public CharacterStatus characterStatus;

    [Header("Constructor Data")]
    public CharacterData displayFields;
    CharacterData newCharacter;
    CameraMultiTarget cameraMultiTarget;

    public GameObject characterToSpawn;

    [Header("Selection Pool")]
    public GameObject[] heads;
    [Space]
    public Material[] materials;
    List<string> namesList = new List<string>();
    int nameIndex, headIndex, materialIndex;

    [Space]
    public Text joinText;
    public Image joinImage;
    public Image readyStatusBar;
    public Sprite[] statusSprites;
    Image[] locatorDots;

    [HideInInspector] public Vector3 spawnPos;
    Vector3 spawnPos1 = new Vector3(-450f, 0.1725311f, 75.67999f);
    Vector3 spawnPos2 = new Vector3(-445f, 0.1725311f, 75.67999f);
    Vector3 spawnPos3 = new Vector3(-440f, 0.1725311f, 75.67999f);
    Vector3 spawnPos4 = new Vector3(-435f, 0.1725311f, 75.67999f);

    GameObject newPlayer;
    GameObject newHead;
    Vector3 headPos;
    Renderer[] childRenderers;
    Animator animator;

    PlayerController controller;
    [HideInInspector] public int currentPlayerId = 0;

    [HideInInspector] public bool selecting;

    // Use this for initialization
    void Awake()
    {
        newCharacter = new CharacterData(displayFields.nameField);

        // 91
        #region Names
        namesList.AddMany("Wroelk", "Gaohq", "Eisk", "Brozzyhm", "Struets",
            "Kruazek", "Strouerq", "Couz'lp", "Pheoblepsua", "Effyls",
            "Klecl", "Globbens", "Marohne", "Poesoth", "Treoffmm", "Knoziet",
            "Wruecrg", "Krougauhn", "Vreik'aelpheo", "Flybbuyhx", "Preodhxe",
            "Uaud", "Kmeib", "Qlyvp", "Zyohz", "Trahr", "Knuodromt", "Grizrq",
            "Rajts", "Veochy", "Suensul", "Vlaemsea", "Sloecals", "Caek",
            "Qlalm", "Briephuylloe", "Cleadyno", "Slachqo", "Tipseln",
            "Goupsiercua", "Kmuohnikx", "Sralfuerqa", "Giot", "Tuottihm",
            "Jiqulp", "Zrauwrets", "Pruyth", "Zrioth", "Uaxxiz", "Brykalz",
            "Vluh'", "Chype", "Heaphyll", "Alsokh", "Afyssae", "Kleamsuph",
            "Vluopuohxeo", "Klovvaelph", "Jeirfeomt", "Gruyphues", "Saurfauloe",
            "Zreaporc", "Bruypruyhq", "Strex", "Maurice the Space Cowboy", "Wuajm",
            "Vrauxxt", "Em", "Kriwrk", "Yk'lp", "Duttesk", "Wauqrhm", "Qleir'lt",
            "Qlats", "Plaem", "Uaclohs", "Ecle", "Nuama", "Faep", "Aloulmie",
            "Gnohnartue", "Prypihl", "Zythylph", "Buodhael", "Sukmilz", "Buylsyd",
            "Mith'ob Omega Supreme", "Corwaldron", "Zoriel", "EchoZoolu", "MimsyWinters");
        #endregion

        cameraMultiTarget = Camera.main.GetComponent<CameraMultiTarget>();
        readyStatusBar.sprite = statusSprites[0];
    }

    // Generates a full new prisoner card (done the first time to give players a default character)
    public void GenerateFullCard(int playerId)
    {
        // Sets random values for each card parameter
        nameIndex = Random.Range(0, namesList.Count);
        headIndex = Random.Range(0, heads.Length);
        materialIndex = Random.Range(0, materials.Length);

        // Assigns each card parameter the above corresponding value
        newCharacter.nameField.text = namesList[nameIndex];

        // Creates the new player and assigns necessary components
        newPlayer = Instantiate(characterToSpawn, spawnPos, Quaternion.Euler(0f, -180f, 0f));
        newPlayer.AddComponent<SelectedPlayer>();
        animator = newPlayer.GetComponent<Animator>();

        AssignHead();
        AssignColour();

        // Assigns newly created characters a playerId for ReWired, and assigns the camera
        controller = newPlayer.GetComponent<PlayerController>();
        currentPlayerId = playerId;
        controller.playerId = currentPlayerId;
        currentPlayerId++;
        controller.cameraTrans = Camera.main.transform;

        // This prevents characters from moving around when players are selecting characters
        controller.enabled = false;
    }

    public void NextHead()
    {
        // Cycles through the array of heads
        if (headIndex >= heads.Length - 1)
        {
            headIndex = 0;
        }
        else
        {
            headIndex++;
        }

        AssignHead();
    }

    public void PreviousHead()
    {
        // Cycles through the array of heads
        if (headIndex <= 0)
        {
            headIndex = heads.Length - 1;
        }
        else
        {
            headIndex--;
        }

        // Assigns the selected head to the character
        AssignHead();
    }

    public void NextColour()
    {
        // Cycles through the array of materials (colours)
        if (materialIndex >= materials.Length - 1)
        {
            materialIndex = 0;
        }
        else
        {
            materialIndex++;
        }

        // Assigns the selected colour to the character
        AssignColour();
    }

    public void PreviousColour()
    {
        // Cycles through the array of materials (colours)
        if (materialIndex <= 0)
        {
            materialIndex = materials.Length - 1;
        }
        else
        {
            materialIndex--;
        }

        // Assigns the selected colour to the character
        AssignColour();
    }

    void AssignHead()
    {
        // Identifies the active head and destroys it
        newHead = newPlayer.GetComponentInChildren<Head>().gameObject;
        headPos = newHead.transform.position;
        Destroy(newHead.gameObject);

        // Spawns the newly selected head and resets animations for it & the body
        newHead = Instantiate(heads[headIndex], headPos, Quaternion.Inverse(newPlayer.transform.rotation), newPlayer.transform);
        animator.SetBool("CharSelect", true);
        animator.Play("CharSelect Idle", -1, 0);
        newHead.GetComponent<Animator>().SetBool("CharSelect", true);
        newHead.GetComponent<Animator>().Play("CharSelect Idle", -1, 0);
    }

    void AssignColour()
    {
        // Gets all of the character's renderers
        childRenderers = newPlayer.GetComponentsInChildren<Renderer>();
        foreach (Renderer child in childRenderers)
        {
            // Sets each renderer's material (except for the head) to the corresponding material (colour)
            if (child.gameObject.layer != 16)
            {
                child.material = materials[materialIndex];
            }
        }

        // Sets the character's locator circle to the above colour
        locatorDots = newPlayer.GetComponentsInChildren<Image>();
        foreach (Image locator in locatorDots)
        {
            Color emissColor = materials[materialIndex].GetColor("_EmissionColor");
            locator.color = emissColor;
        }
    }

    public void RemovePlayer(int playerId)
    {
        //Destroy(controller[playerId].gameObject);
    }

    // Used in AssignPlayers to prevent accidential selection spamming
    public IEnumerator SelectionDelay()
    {
        yield return new WaitForSeconds(0.2f);
        selecting = false;
        yield return null;
    }

    // Gets rid of the Join Game UI when player joins the game
    public void DeactivateJoinIcons()
    {
        joinImage.enabled = false;
        joinText.enabled = false;
    }

    public void ActivateJoinIcons()
    {
        joinImage.enabled = true;
        joinText.enabled = true;
    }
}

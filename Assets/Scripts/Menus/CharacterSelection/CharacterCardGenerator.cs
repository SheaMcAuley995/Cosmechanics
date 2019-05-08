using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct CharacterData
{
    // Display fields on the character card
    public Text nameField, crimeField, sentenceField;

    // Constructor for creating new character cards
    public CharacterData(Text _nameField, Text _crimeField, Text _sentenceField)
    {
        nameField = _nameField;
        crimeField = _crimeField;
        sentenceField = _sentenceField;
    }
}

public class CharacterCardGenerator : MonoBehaviour
{
    public enum CharacterStatus { SELECTING, READY };
    public CharacterStatus characterStatus;

    [Header("Constructor Data")]
    public CharacterData displayFields;
    CharacterData newCharacter;
    CharacterData lastCard;
    CameraMultiTarget cameraMultiTarget;

    public GameObject characterToSpawn;

    [Header("Selection Pool")]
    public RenderTexture[] videoFeeds;
    [Space]
    public GameObject[] heads;
    [Space]
    public Material[] materials;
    [Space]
    public Image[] locatorDots;

    [Space]
    public Image readyStatusBar;
    public Sprite[] statusSprites;

    // Lists
    List<RenderTexture> videoFeedList = new List<RenderTexture>();
    List<string> namesList = new List<string>();
    List<string> crimesList = new List<string>();
    List<string> sentencesList = new List<string>();
    List<GameObject> headsList = new List<GameObject>();
    List<Material> materialList = new List<Material>();

    List<string> questionables = new List<string>();

    List<int> previousHeads = new List<int>();
    List<int> previousNames = new List<int>();
    List<int> previousCrimes = new List<int>();
    List<int> previousSentences = new List<int>();
    List<int> previousMaterials = new List<int>();

    GameObject lastSelected;
    [HideInInspector] public Vector3 spawnPos;
    Vector3 spawnPos1 = new Vector3(-450f, 0.1725311f, 75.67999f);
    Vector3 spawnPos2 = new Vector3(-445f, 0.1725311f, 75.67999f);
    Vector3 spawnPos3 = new Vector3(-440f, 0.1725311f, 75.67999f);
    Vector3 spawnPos4 = new Vector3(-435f, 0.1725311f, 75.67999f);

    Head[] childHeads;
    GameObject newPlayer;
    Renderer[] children;
    Animator animator;

    public Text joinText;
    public Image joinImage;

    // Random selection variables
    int nameIndex, crimeIndex, sentenceIndex, headIndex, materialIndex;

    [HideInInspector] public int currentPlayerId = 0;

    int lastMat = 0;
    int lastHead = 0;
    int timesGoneBack = 0;
    int timesGoneBackHead, timesGoneBackColour, timesGoneBackCrime;

    [HideInInspector] public bool selecting;

    // Use this for initialization
    void Awake()
    {
        newCharacter = new CharacterData(displayFields.nameField, displayFields.crimeField, displayFields.sentenceField);

        #region Generated List Content

        // 4
        #region Video Feeds
        for (int i = 0; i < videoFeeds.Length; i++)
        {
            videoFeedList.AddMany(videoFeeds[i]);
        }
        #endregion

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

        // 52
        #region Crimes
        crimesList.AddMany("Astrolarsony", "Space littering", "Serial cereal eating", 
            "Accelerating the big crunch", "Public dabbing", "Overpaying crewmembers", 
            "Lollygagging", "Owning too many cats", "Owning too few cats", "Sneezing loudly", 
            "Leaving the water running", "Spilling the florp", "Drawing on the table",             
            "Unlicensed terraforming", "Unsafe hyperlane change", "Drinking the florp", 
            "Panicking at the disco", "Couldn't handle the truth", "Star arson", 
            "Crashing the LAN party", "Removing the watermark", "Coloring outside the lines",
            "Leaving someone on 'read'", "Eating all of the cookies", "Novel reading",            
            "Feebleness of intellect", "Video game addiction", "Poor taste in art", 
            "Untidy computer desktop", "Timeline destabilization", "Illicit space-whale watching", 
            "Misplacing warheads", "Unauthorized cat hoarding", "Inappropriate florp handling",
            "Destroyed sacred meteor", "Piloting without a license", "Didn't return library book",
            "Failed to return pen", "Ran a red light", "Scream-sneezed", "Unsanctioned docking",
            "Speeding in the hangar", "Asked for the manager", "Brought baby to the movies",
            "Forgot valentine's day", "Forgot their anniversary", "Tattled", "Pantsed a co-worker",
            "Pirated movies", "Used water on an oil fire", "Put tin foil in the microwave",
            "Ate soup with a fork");
        #endregion

        // 14
        #region Sentences
        sentencesList.AddMany("Community service", "Rehabilitation classes", 
            "Extra math homework", "Fined 25 million spacecoin", "Handcuffed to their clone",
            "THE CLAW", "Has to wear wet socks", "No wifi", "Non-powered steering", 
            "Tax rate doubled", "Doubled commute time", "100 push ups", "50 zero-g pull-ups",
            "Music taken away");
        #endregion

        // 4
        #region Materials
        for (int l = 0; l < materials.Length; l++)
        {
            materialList.AddMany(materials[l]);
        }
        #endregion

        #region Questionable Or Too Long Crimes & Sentences
        //questionables.AddMany("Public inflorpication", "Boring when inflorpicated", "Piloting under the influence of spice",
        //    "Possession of florp with intent to redistribute", "Minus 50 DKP", "To be used as a test subject", "Involuntary organ donation",
        //    "Disassembly by nanobots", "Brain to be removed from body and inserted into a manufacturing robot", 
        //    "Body to be donated to science prior to death", "1e+08 quantum galactic cycles cleaning the Aether",
        //    "500 years in the spice mines of Druffel", "Infinite existence in a cyclical time loop",
        //    "Doesn't have to go home, but they can't stay here", "To be exposed to a vacuum for approximately 3.4111111176 seconds",
        //    "Has to do the Druffel run in less than 11 parsecs", "Must memorize the SpaceTunes terms and conditions",
        //    "Internet access restricted to 56k modem", "Has to program an operating system entirely in binary",
        //    "10 years of hard labor as a trash compactor inspector", "To be marooned on a desert planet with two suns and two troublesome droids",
        //    "To be used to plug a hole the size of a dinner plate in the airlock", "Must run a barefoot marathon on a path strewn with legos",
        //    "Atmospheric reentry without a heat shield", "To be replaced by a clone and forced to watch it live out it's life",
        //    "To have 50% of all good memories removed", "Banished to a wretched hive of scum and villainy",
        //    "To be given laxatives and pushed into space so that their last act will be to boldly go where no one has gone before",
        //    "To be stranded on an ocean planet near the event horizon of a black hole", "Seeing someone sneeze... and not saying 'bless you'",
        //    "Traumatizing a Class 0 Civilization with a fake alien invasion", "Public indecency and indecent publicity",
        //    "Speeding - going warp 80 in a warp 65 zone", "Florp-juggling without a license", "Hitting all of the buttons on a space elevator",
        //    "Unsanctioned alteration of a moon's orbit", "Desynchronizing the phase cycles of a quantum entanglement tunneler",
        //    "Noise ordinance violation: playing music too loudly in space", "Use of imperial units instead of metric", "Using only 10% of the brain",
        //    "Failing a Turing test while biological", "Sneezing without covering their mouth", "Fighting for their right to party",
        //    "Failing to construct additional pylons", "Failing to provide more cowbell", "Failing to study history... and then repeating it",
        //    "Calling yourself a space pirate", "Unsanctioned rapid unscheduled disassembly", "Incorrectly using the word 'blockchain'",
        //    "Jigglegaffing within 30 parsecs of the big bang", "Failure to properly shuffle cards", "Selling florponade without a permit",
        //    "Wearing the wrong shoe size", "Opening an airlock in the wrong situation", "Not completing a dyson sphere", "Space-whale poaching",
        //    "Non-consensual evacuation of planets", "Something really, really bad", "Food for the broodmother");
        #endregion

        #endregion

        cameraMultiTarget = Camera.main.GetComponent<CameraMultiTarget>();

        readyStatusBar.sprite = statusSprites[0];
    }

    // Generates a full new prisoner card (done the first time to give players a default character)
    public void GenerateFullCard(int playerId)
    {
        // Sets random values for each card parameter
        int headIndex = Random.Range(0, 3);
        int nameIndex = Random.Range(0, namesList.Count);
        int crimeIndex = Random.Range(0, crimesList.Count);
        int sentenceIndex = Random.Range(0, sentencesList.Count);
        int materialIndex = Random.Range(0, materials.Length);

        // Assigns each card parameter the above corresponding value
        newCharacter.nameField.text = namesList[nameIndex];
        newCharacter.crimeField.text = crimesList[crimeIndex];
        newCharacter.sentenceField.text = sentencesList[sentenceIndex];

        // Stores card data for selecting previous cards
        previousHeads.Add(headIndex);
        previousNames.Add(nameIndex);
        previousCrimes.Add(crimeIndex);
        previousSentences.Add(sentenceIndex);
        previousMaterials.Add(materialIndex);

        // Creates the new player
        newPlayer = Instantiate(characterToSpawn, spawnPos, Quaternion.Euler(0f, -180f, 0f));
        newPlayer.AddComponent<SelectedPlayer>();
        childHeads = newPlayer.GetComponentsInChildren<Head>();
        animator = newPlayer.GetComponent<Animator>();

        // Adds all of the player's head options to a list
        for (int i = 0; i < childHeads.Length; i++)
        {
            headsList.Add(childHeads[i].gameObject);
        }
        // Displays the first randomly selected head
        for (int i = 0; i < headsList.Count; i++)
        {
            // Sets all heads as inactive first just to make sure it nevers lets two heads be active
            headsList[i].SetActive(false);
        }
        headsList[headIndex].SetActive(true);

        // Gets all of the character's renderers
        children = newPlayer.GetComponentsInChildren<Renderer>();
        foreach (Renderer child in children)
        {
            // Sets each renderer's material (except for the head) to the corresponding material (colour)
            if (child.gameObject.layer != 16)
            {
                child.material = materialList[materialIndex];
            }
        }

        // Sets the character's locator circle to the above colour
        locatorDots = newPlayer.GetComponentsInChildren<Image>();
        foreach (Image locator in locatorDots)
        {
            Color emissColor = materialList[materialIndex].GetColor("_EmissionColor");
            locator.color = emissColor;
        }

        lastSelected = newPlayer.gameObject;

        // Assigns newly created characters a playerId for ReWired
        PlayerController controller = newPlayer.GetComponent<PlayerController>();
        currentPlayerId = playerId;
        controller.playerId = currentPlayerId;
        currentPlayerId++;

        // This prevents characters from moving around when players are selecting characters
        controller.cameraTrans = Camera.main.transform;
        controller.walkSpeed = 0.0f;
        controller.runSpeed = 0.0f;
        controller.turnSmoothTime = 100f;
    }

    public void GenerateModel(int playerId)
    {
        // Sets values for each card parameter
        headIndex = lastHead;
        nameIndex = Random.Range(0, namesList.Count);

        // Assigns each card parameter the above corresponding value
        newCharacter.nameField.text = namesList[nameIndex];

        previousHeads.Add(headIndex);
        previousNames.Add(nameIndex);

        // Assigns the new head object
        for (int i = 0; i < headsList.Count; i++)
        {
            headsList[i].SetActive(false);
        }
        headsList[headIndex].SetActive(true);
        animator.Play("Idle", -1, 0);

        lastSelected = headsList[headIndex].gameObject;

        // Cycles through the heads instead of generating them randomly
        lastHead++;
        if (lastHead >= headsList.Count)
        {
            lastHead = 0;
        }

        // Assigns newly created characters a playerId for ReWired
        PlayerController controller = newPlayer.GetComponent<PlayerController>();
        currentPlayerId = playerId;
        controller.playerId = currentPlayerId;
        currentPlayerId++;

        // This prevents characters from moving around when players are selecting characters
        controller.cameraTrans = Camera.main.transform;
        controller.walkSpeed = 0.0f;
        controller.runSpeed = 0.0f;
        controller.turnSmoothTime = 100f;
    }

    public void GeneratePreviousModel(int playerId)
    {
        if (timesGoneBackHead >= previousNames.Count)
        {
            timesGoneBackHead = 0;
        }

        // Sets values for each card parameter
        int headIndex = previousHeads[previousHeads.Count - 2 - timesGoneBackHead];
        int nameIndex = previousNames[previousNames.Count - 2 - timesGoneBackHead];

        // Assigns each card parameter the above corresponding value
        newCharacter.nameField.text = namesList[nameIndex];

        // Assigns the new head
        for (int i = 0; i < headsList.Count; i++)
        {
            headsList[i].SetActive(false);
        }
        headsList[headIndex].SetActive(true);
        animator.Play("Idle", -1, 0);

        timesGoneBackHead++;

        // Assigns newly created characters a playerId for ReWired
        PlayerController controller = newPlayer.GetComponent<PlayerController>();
        currentPlayerId = playerId;
        controller.playerId = currentPlayerId;
        currentPlayerId++;

        // This prevents characters from moving around when players are selecting characters
        controller.cameraTrans = Camera.main.transform;
        controller.walkSpeed = 0.0f;
        controller.runSpeed = 0.0f;
        controller.turnSmoothTime = 100f;
    }

    public void GenerateColour()
    {
        // Sets value for the card's colour parameter
        materialIndex = lastMat;

        // Gets all of the character's renderers
        foreach (Renderer child in children)
        {
            // Sets each renderer's material (except for the head) to the corresponding material (colour)
            if (child.gameObject.layer != 16)
            {
                child.material = materialList[materialIndex];
            }
        }

        // Sets the character's locator circle to the above colour
        foreach (Image locator in locatorDots)
        {
            Color emissColor = materialList[materialIndex].GetColor("_EmissionColor");
            locator.color = emissColor;
        }

        previousMaterials.Add(materialIndex);

        // Cycles through the materials instead of generating them randomly
        lastMat++;
        if (lastMat >= materialList.Count)
        {
            lastMat = 0;
        }
    }

    public void GeneratePreviousColour()
    {
        if (timesGoneBackColour == previousNames.Count)
        {
            timesGoneBackColour = 0;
        }

        // Sets value for the card's colour parameter
        int materialIndex = previousMaterials[previousMaterials.Count - 2 - timesGoneBackColour];

        // Gets all of the character's renderers
        foreach (Renderer child in children)
        {
            // Sets each renderer's material (except for the head) to the corresponding material (colour)
            if (child.gameObject.layer != 16)
            {
                child.material = materialList[materialIndex];
            }
        }

        // Sets the character's locator circle to the above colour
        foreach (Image locator in locatorDots)
        {
            locator.color = materialList[materialIndex].color;
        }

        timesGoneBackColour++;
    }

    // Used in AssignPlayers to prevent accidential selection spamming
    public IEnumerator SelectionDelay()
    {
        yield return new WaitForSeconds(0.2f);
        selecting = false;
        yield return null;
    }

    public void DeactivateJoinIcons()
    {
        joinImage.enabled = false;
        joinText.enabled = false;
    }
}

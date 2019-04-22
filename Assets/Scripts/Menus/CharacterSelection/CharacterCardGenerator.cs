using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct CharacterData
{
    /// Display fields on the character card
    public TextMeshProUGUI nameField, crimeField, sentenceField;

    /// Constructor for creating new character cards
    public CharacterData(TextMeshProUGUI _nameField, TextMeshProUGUI _crimeField, TextMeshProUGUI _sentenceField)
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

    [Header("Selection Pool")]
    public RenderTexture[] videoFeeds;
    [Space]
    public GameObject[] characters;
    [Space]
    public Material[] materials;
    [Space]
    public Image[] locatorDots;

    [Space]
    public Image readyStatusBar;
    public Sprite[] statusSprites;

    /// Lists
    List<RenderTexture> videoFeedList = new List<RenderTexture>();
    List<string> namesList = new List<string>();
    List<string> crimesList = new List<string>();
    List<string> sentencesList = new List<string>();
    List<GameObject> prefabsList = new List<GameObject>();
    List<Material> materialList = new List<Material>();

    List<int> previousPrefabs = new List<int>();
    List<int> previousNames = new List<int>();
    List<int> previousCrimes = new List<int>();
    List<int> previousSentences = new List<int>();
    List<int> previousMaterials = new List<int>();

    GameObject lastSelectedPlayerModel;
    [HideInInspector] public Vector3 spawnPos;
    Vector3 spawnPos1 = new Vector3(-450f, 0.1725311f, 75.67999f);
    Vector3 spawnPos2 = new Vector3(-445f, 0.1725311f, 75.67999f);
    Vector3 spawnPos3 = new Vector3(-440f, 0.1725311f, 75.67999f);
    Vector3 spawnPos4 = new Vector3(-435f, 0.1725311f, 75.67999f);

    /// Random selection variables
    int nameIndex, crimeIndex, sentenceIndex, prefabIndex, materialIndex;

    int currentPlayerId = 0;

    int lastMat = 0;
    int lastHead = 0;
    int timesGoneBack = 0;

    [HideInInspector] public bool selecting;

    // Use this for initialization
    void Awake()
    {
        newCharacter = new CharacterData(displayFields.nameField, displayFields.crimeField, displayFields.sentenceField);

        #region Generated List Content
        #region Video Feeds
        /// 4 video feeds added to the list using the ListExtension class
        for (int i = 0; i < videoFeeds.Length; i++)
        {
            videoFeedList.AddMany(videoFeeds[i]);
        }
        #endregion

        #region Names
        /// 90 names added to the list using the ListExtension class
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
            "Mith'ob Omega Supreme", "Corwaldron", "Zoriel", "EchoZoolu");
        #endregion

        #region Ages
        ///// Ages 1 - 1,000 added to the list using the ListExtension class
        //int maxAge = 1000;
        //for (int age = 1; age < maxAge; age++)
        //{
        //    agesList.AddMany(age);
        //}
        #endregion

        #region Genders
        ///// 4 gender icons added to the list (can add more) using the ListExtension class
        //for (int j = 0; j < genders.Length; j++)
        //{
        //    gendersList.AddMany(genders[j]);
        //}
        #endregion

        #region Crimes
        /// 65 crimes added to the list using the ListExtension class
        crimesList.AddMany("Astrolarsony", "Public inflorpication", "Space-whale poaching",
            "Not completing a dyson sphere", "Non-consensual evacuation of planets",
            "Space littering", "Something about opening an airlock in the wrong situation",
            "Serial cereal eating", "Accelerating the big crunch", "Public dabbing", 
            "Overpaying crewmembers", "Wearing the wrong shoe size", "Lollygagging",
            "Selling florponade without a permit", "Owning too many cats",
            "Owning too few cats", "Sneezing loudly", "Leaving the water running",
            "Spilling the florp", "Failure to properly shuffle cards", "Drawing on the table",
            "Jigglegaffing within 30 parsecs of the big bang", "Incorrectly using the word 'blockchain'", 
            "Unsanctioned rapid unscheduled disassembly", "Unlicensed terraforming", "Unsafe hyperlane change", 
            "Calling yourself a space pirate", "Drinking the florp", "Panicking at the disco",
            "Couldn't handle the truth", "Failing to study history... and then repeating it",
            "Launching a spacecraft out of the solar system with a map to the home planet on it",
            "Failing to provide more cowbell", "Failing to construct additional pylons",
            "Fighting for their right to party", "Star arson", "Crashing the LAN party",
            "Removing the watermark", "Coloring outside the lines", "Sneezing without covering their mouth",
            "Leaving someone on 'read'", "Eating all of the cookies", "Novel reading",
            "Boring when inflorpicated", "Obsession with anime", "Failing a Turing test while biological",
            "Feebleness of intellect", "Video game addiction", "Using only 10% of the brain",
            "Use of imperial units instead of metric", "Piloting under the influence of spice",
            "Noise ordinance violation: playing music too loudly in space", "Poor taste in art",
            "Untidy computer desktop", "Timeline destabilization",
            "Desynchronizing the phase cycles of a quantum entanglement tunneler",
            "Unsanctioned alteration of a moon's orbit", "Possession of florp with intent to redistribute",
            "Hitting all of the buttons on a space elevator",
            "Speeding - going warp 80 in a warp 65 zone", "Public indecency and indecent publicity",
            "Traumatizing a Class 0 Civilization with a fake alien invasion",
            "Seeing someone sneeze... and not saying 'bless you'", "Florp-juggling without a license",
            "Article 13 Violation: Posting Memes");
        #endregion

        #region Sentences
        /// 35 sentences added to the list using the ListExtension class
        sentencesList.AddMany("1e+08 quantum galactic cycles cleaning the Aether",
            "500 years in the spice mines of Druffel", "Food for the broodmother",
            "Community service", "Rehabilitation classes", "Extra math homework",
            "Fined 25 million aliencoin", "Infinite suffering in a cyclical time loop",
            "Minus 50 DKP", "Doesn't have to go home, but they can't stay here",
            "Handcuffed to their clone",
            "To be exposed to a vacuum for approximately 20.4111111176 seconds",
            "THE CLAW", "Something really, really bad", "Has to wear wet socks",
            "To be used as a test subject", "No wifi",
            "Has to do the Druffel run in less than 11 parsecs",
            "Must memorize the SpaceTunes terms and conditions",
            "Internet access restricted to 56k modem",
            "Has to program an operating system entirely in binary",
            "10 years of hard labor as a trash compactor inspector",
            "To be marooned on a desert planet with two suns and two idiotic droids",
            "To be used to plug a hole the size of a dinner plate in the airlock",
            "Must run a barefoot marathon on a path strewn with legos",
            "Disassembly by nanobots", "Involuntary organ donation",
            "Brain to be removed from body and inserted into a manufacturing robot",
            "Body to be donated to science prior to death",
            "Atmospheric reentry without a heat shield",
            "To be replaced by a clone and forced to watch it live out it's life",
            "To have 50% of all good memories removed",
            "Banished to a wretched hive of scum and villainy",
            "To be given laxatives and pushed into space so that their last act will be to boldly go where no one has gone before",
            "To be stranded on an ocean planet near the event horizon of a black hole");
        #endregion

        #region Prefabs
        /// 4 character prefabs added to the list using the ListExtension class
        for (int k = 0; k < characters.Length; k++)
        {
            prefabsList.AddMany(characters[k]);
        }
        #endregion

        #region Materials
        for (int l = 0; l < materials.Length; l++)
        {
            materialList.AddMany(materials[l]);
        }
        #endregion
        #endregion

        cameraMultiTarget = Camera.main.GetComponent<CameraMultiTarget>();

        readyStatusBar.sprite = statusSprites[0];
    }

    // Generates a full new prisoner card
    public void GenerateFullCard(int playerId)
    {
        Destroy(lastSelectedPlayerModel);

        // Sets random values for each card parameter
        int prefabIndex = Random.Range(0, characters.Length);
        int nameIndex = Random.Range(0, namesList.Count);
        int crimeIndex = Random.Range(0, crimesList.Count);
        int sentenceIndex = Random.Range(0, sentencesList.Count);
        int materialIndex = Random.Range(0, materials.Length);

        // Assigns each card parameter the above corresponding value
        newCharacter.nameField.text = namesList[nameIndex];
        newCharacter.crimeField.text = crimesList[crimeIndex];
        newCharacter.sentenceField.text = sentencesList[sentenceIndex];

        // Stores card data for selecting previous cards
        previousPrefabs.Add(prefabIndex);
        previousNames.Add(nameIndex);
        previousCrimes.Add(crimeIndex);
        previousSentences.Add(sentenceIndex);
        previousMaterials.Add(materialIndex);

        // Creates the new player
        GameObject newPlayer = Instantiate(prefabsList[prefabIndex], spawnPos, Quaternion.Euler(0f, -180f, 0f));

        // Gets all of the character's renderers
        Renderer[] children = newPlayer.GetComponentsInChildren<Renderer>();
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
            locator.color = materialList[materialIndex].color;
        }

        lastSelectedPlayerModel = newPlayer.gameObject;

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

    // Generates previous character cards
    public void GeneratePreviousCard(int playerId)
    {
        Destroy(lastSelectedPlayerModel);

        if (timesGoneBack == previousNames.Count - 1)
        {
            timesGoneBack = 0;
        }

        // Sets values for each card parameter
        int prefabIndex = previousPrefabs[previousPrefabs.Count - 2 - timesGoneBack];
        int nameIndex = previousNames[previousNames.Count - 2 - timesGoneBack];
        int crimeIndex = previousCrimes[previousCrimes.Count - 2 - timesGoneBack];
        int sentenceIndex = previousSentences[previousSentences.Count - 2 - timesGoneBack];
        int materialIndex = previousMaterials[previousMaterials.Count - 2 - timesGoneBack];

        // Applies above values to card
        newCharacter.nameField.text = namesList[nameIndex];
        newCharacter.crimeField.text = crimesList[crimeIndex];
        newCharacter.sentenceField.text = sentencesList[sentenceIndex];

        // Creates the new player
        GameObject newPlayer = Instantiate(prefabsList[prefabIndex], spawnPos, Quaternion.Euler(0f, -180f, 0f));

        // Gets all of the character's renderers
        Renderer[] children = newPlayer.GetComponentsInChildren<Renderer>();
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
            locator.color = materialList[materialIndex].color;
        }

        lastSelectedPlayerModel = newPlayer.gameObject;
        timesGoneBack++;

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

    #region Methods For: Customize Character - Multiple Buttons
    public void GenerateModel(int playerId)
    {
        Destroy(lastSelectedPlayerModel);

        // Sets values for each card parameter
        prefabIndex = lastHead;
        nameIndex = Random.Range(0, namesList.Count);

        // Assigns each card parameter the above corresponding value
        newCharacter.nameField.text = namesList[nameIndex];

        // Creates the new player
        GameObject newPlayer = Instantiate(prefabsList[prefabIndex], spawnPos, Quaternion.Euler(0f, -180f, 0f));

        lastSelectedPlayerModel = newPlayer.gameObject;

        // Cycles through the heads instead of generating them randomly
        lastHead++;
        if (lastHead >= prefabsList.Count)
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
        Destroy(lastSelectedPlayerModel);

        if (timesGoneBack == previousNames.Count - 1)
        {
            timesGoneBack = 0;
        }

        // Sets values for each card parameter
        prefabIndex = lastHead;
        nameIndex = previousNames[previousNames.Count - 2 - timesGoneBack];

        // Assigns each card parameter the above corresponding value
        newCharacter.nameField.text = namesList[nameIndex];

        // Creates the new player
        GameObject newPlayer = Instantiate(prefabsList[prefabIndex], spawnPos, Quaternion.Euler(0f, -180f, 0f));
        lastSelectedPlayerModel = newPlayer.gameObject;

        // Cycles through the heads instead of generating them randomly
        lastHead--;
        if (lastHead <= 0)
        {
            lastHead = prefabsList.Count;
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

    public void GenerateColour()
    {
        // Sets value for the card's colour parameter
        materialIndex = lastMat;

        // Gets all of the character's renderers
        Renderer[] children = lastSelectedPlayerModel.GetComponentsInChildren<Renderer>();
        foreach (Renderer child in children)
        {
            // Sets each renderer's material (except for the head) to the corresponding material (colour)
            if (child.gameObject.layer != 16)
            {
                child.material = materialList[materialIndex];
            }
        }

        // Sets the character's locator circle to the above colour
        locatorDots = lastSelectedPlayerModel.GetComponentsInChildren<Image>();
        foreach (Image locator in locatorDots)
        {
            locator.color = materialList[materialIndex].color;
        }

        // Cycles through the materials instead of generating them randomly
        lastMat++;
        if (lastMat >= materialList.Count - 1)
        {
            lastMat = 0;
        }
    }

    public void GeneratePreviousColour()
    {
        // Sets value for the card's colour parameter
        materialIndex = lastMat;

        // Gets all of the character's renderers
        Renderer[] children = lastSelectedPlayerModel.GetComponentsInChildren<Renderer>();
        foreach (Renderer child in children)
        {
            // Sets each renderer's material (except for the head) to the corresponding material (colour)
            if (child.gameObject.layer != 16)
            {
                child.material = materialList[materialIndex];
            }
        }

        // Sets the character's locator circle to the above colour
        locatorDots = lastSelectedPlayerModel.GetComponentsInChildren<Image>();
        foreach (Image locator in locatorDots)
        {
            locator.color = materialList[materialIndex].color;
        }

        // Cycles through the materials instead of generating them randomly
        lastMat--;
        if (lastMat <= 0)
        {
            lastMat = materialList.Count - 1;
        }
    }

    public void GenerateCrime()
    {
        // Sets values for each card parameter
        crimeIndex = Random.Range(0, crimesList.Count);
        sentenceIndex = Random.Range(0, sentencesList.Count);

        // Assigns each card parameter the above corresponding value
        newCharacter.crimeField.text = crimesList[crimeIndex];
        newCharacter.sentenceField.text = sentencesList[sentenceIndex];
    }

    public void GeneratePreviousCrime()
    {
        // Sets values for each card parameter
        crimeIndex = previousCrimes[previousCrimes.Count - 2 - timesGoneBack];
        sentenceIndex = previousSentences[previousSentences.Count - 2 - timesGoneBack];

        // Assigns each card parameter the above corresponding value
        newCharacter.crimeField.text = crimesList[crimeIndex];
        newCharacter.sentenceField.text = sentencesList[sentenceIndex];
    }
    #endregion

    // Used in AssignPlayers to prevent accidential selection spamming
    public IEnumerator SelectionDelay()
    {
        yield return new WaitForSeconds(0.2f);
        selecting = false;
        yield return null;
    }
}

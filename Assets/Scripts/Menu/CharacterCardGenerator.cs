using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCardGenerator : MonoBehaviour
{
    #region UI Data
    // Text displays
    public TextMeshProUGUI nameField, ageField, crimeField, sentenceField;

    // Picture displays
    public Image portraitField;
    public Sprite portrait1, portrait2, portrait3, portrait4, portrait5;
    public Image genderField;
    public Sprite gender1, gender2, gender3, gender4;
    #endregion

    // Lists
    List<Sprite> portraitsList = new List<Sprite>();
    List<string> namesList = new List<string>();
    List<int> agesList = new List<int>();
    List<Sprite> gendersList = new List<Sprite>();
    List<string> crimesList = new List<string>();
    List<string> sentencesList = new List<string>();

    // Random selection variables
    int portraitIndex, nameIndex, ageIndex, genderIndex, crimeIndex, sentenceIndex;

	// Use this for initialization
	void Start ()
    {
        #region Portraits
        /// 5 portraits added to the list using the ListExtension class
        portraitsList.AddMany(portrait1, portrait2, portrait3, portrait4, portrait5);
        #endregion

        #region Names
        /// 86 names added to the list using the ListExtension class
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
            "Gnohnartue", "Prypihl", "Zythylph", "Buodhael", "Sukmilz", "Buylsyd");
        #endregion

        #region Ages
        /// Ages 1 - 1,000 added to the list using the ListExtension class
        int maxAge = 1000;
        for (int age = 1; age < maxAge; age++)
        {
            agesList.AddMany(age);
        }
        #endregion

        #region Genders
        /// 4 gender icons added to the list (can add more) using the ListExtension class
        gendersList.AddMany(gender1, gender2, gender3, gender4);
        #endregion

        #region Crimes
        /// 68 crimes added to the list using the ListExtension class
        crimesList.AddMany("Astrolarsony", "Public inflorpication", "Space-whale poaching",
            "Not completing a dyson sphere", "Non-consensual evacuation of planets",
            "Space littering", "Something about opening an airlock in the wrong situation",
            "Serial cereal eating", "Accelerating the big crunch", "Preventing crunch",
            "Switching to HDR pipeline", "Releasing a buggy game", "Public dabbing",
            "Overpaying crewmembers", "Wearing the wrong shoe size", "Lollygagging",
            "Selling florponade without a license", "Owning too many space-cats",
            "Owning too few space-cats", "Sneezing loudly", "Leaving the water running",
            "Spilling the florp", "Failiure to properly shuffle cards", "Drawing on the table",
            "Jigglegaffing within 30 parsecs of the big bang", "Leaving the galacticean union",
            "Incorrectly using the word 'blockchain'", "All of them", "Causing rapid unscheduled disassembly", 
            "Unlicensed terraforming", "Unsafe hyperlane change", "Calling yourself a space pirate", 
            "Drinking the florp", "Releasing an early alpha", "Panicking at the disco", 
            "Couldn't handle the truth", "Failing to study history... and then repeating it",
            "Launching a spacecraft out of the solar system with a map to the home planet on it",
            "Failed to provide more cowbell", "Failed to construct additional pylons",
            "Fought for their right to party", "Star arson", "Crashing the LAN party",
            "Removed the watermark", "Coloring outside the lines", "Sneezed without covering their mouth",
            "Left someone on 'read'", "Eating all of the cookies", "Novel reading",
            "Boring when inflorpicated", "Obsession with anime", "Failing a Turing test while biological",
            "Feebleness of intellect", "Video game addiction", "Using only 10% of the brain",
            "Use of imperial units instead of metric", "Piloting under the influence of spice",
            "Noise ordinance violation: playing music too loudly in space", "Poor taste in art",
            "Untidy computer desktop", "Timeline destabilization", 
            "Desynchronizing the phase cycles of a quantum entanglement tunneler", 
            "Unsanctioned alteration of a moon's orbit", 
            "Hitting all of the buttons on a space elevator at once", 
            "Speeding - going warp 80 in a warp 65 zone", "Public indecency and indecent publicity", 
            "Traumatizing a Class 0 Civilization with a fake alien invasion", 
            "Saw someone sneeze... and didn't say 'bless you'");
        #endregion

        #region Sentences
        /// 35 sentences added to the list using the ListExtension class
        sentencesList.AddMany("1e+08 quantum galactic cycles cleaning the Aether",
            "500 years in the spice mines of Druffel", "Food for the broodmother",
            "Community service", "Rehabilitation classes", "Extra math homework",
            "Fined 25 million aliencoin", "Infinite suffering in a cyclical time loop",
            "Minus 50 DKP", "Doesn't have to go home, but they can't stay here",
            "Handcuffed to a clone of themself",
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
            "Dissassembly by nanobots", "Involuntary organ donation", 
            "Brain to be removed from body and inserted into a manufacturing robot",
            "Body to be donated to science prior to death", 
            "Atmospheric reentry without a heat shield", 
            "To be replaced by a clone and forced to watch it live out it's life",
            "To have 50% of all good memories removed", 
            "Banished to a wretched hive of scum and villainy", 
            "To be given laxatives and pushed into space so that their last act will be to boldly go where no one has gone before",
            "To be stranded on an ocean planet on the edge of a black hole's event horizon");
        #endregion
    }

    /// Generates a new prisoner card
    public void GenerateCard()
    {
        #region Random Selections
        /// NOTE: Cache these later for some of that sweet juicy #efficiency
        portraitIndex = Random.Range(0, 5);
        nameIndex = Random.Range(0, 86);
        DetermineAgeBias();
        genderIndex = Random.Range(0, 4);
        crimeIndex = Random.Range(0, 68);
        sentenceIndex = Random.Range(0, 35);
        #endregion

        #region Display Fields
        portraitField.sprite = portraitsList[portraitIndex];
        nameField.text = namesList[nameIndex];
        ageField.text = agesList[ageIndex].ToString();
        genderField.sprite = gendersList[genderIndex];
        crimeField.text = crimesList[crimeIndex];
        sentenceField.text = sentencesList[sentenceIndex];
        #endregion
    }

    void DetermineAgeBias()
    {
        int ageBias = Random.Range(1, 10);
        if (ageBias < 9)
        {
            ageIndex = Random.Range(2, 100);
        }
        else if (ageBias >= 9)
        {
            ageIndex = Random.Range(101, 1000);
        }
    }

    /// Temporary activator -- will replace with buttons
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateCard();
        }
    }
}

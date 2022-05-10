using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChEd_Race : MonoBehaviour
{
    public static ChEd_Race Instance;

    private BasicPC basicPc;

    [SerializeField] private GameObject raceItem;
    [SerializeField] private GameObject raceItemMenuParent;

    private RaceItem previousButton;

    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Dropdown dropdown;

    [SerializeField] private GameObject traitPrefab;
    [SerializeField] private GameObject traitMenuParent;
    private List<GameObject> traitsRace;
    private List<GameObject> traitsSubrace;

    [SerializeField] private GameObject electionPrefab;
    [SerializeField] private GameObject electionMenuParent;
    private List<GameObject> electionRace;
    private List<GameObject> electionSubrace;
    [SerializeField] private GameObject dropdownPrefab;

    private Dictionary<string,GameObject> raceButtons = new Dictionary<string,GameObject>();

    private void Awake()
    {
        // Instance creation
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    // MENU OF RACES
    public void Init(BasicPC basicPC)
    {
        string firstRace = null;
        // Set the base pc
        this.basicPc = basicPC;
        // Reset and place race icons
        ResetItems();
        foreach (DB.Race race in DB.Instance.dictionary_Race.Values)
        {
            if (firstRace == null) firstRace = race.index;
            GameObject go = Instantiate(raceItem, raceItemMenuParent.transform);

            go.transform.SetParent(raceItemMenuParent.transform, false);
            go.name = race.index;

            go.GetComponentInChildren<RaceItem>().ItemInit(race.index);
            go.GetComponent<Button>().onClick.AddListener(() => { FocusRace(go); });

            raceButtons.Add(race.index, go);
        }

        //Init lists
        electionRace = new List<GameObject>();
        electionSubrace = new List<GameObject>();
        traitsSubrace = new List<GameObject>();
        traitsRace = new List<GameObject>();

        // If info stored, regenerate it
        if (basicPc != null && basicPc.Race != null)
        {
            ButtonSelected(raceButtons[basicPC.Race].GetComponentInChildren<RaceItem>());
            FocusRace(raceButtons[basicPc.Race]);
            //FocusSubrace();
        }
        else
        {
            ButtonSelected(raceButtons[firstRace].GetComponentInChildren<RaceItem>());
            FocusRace(raceButtons[firstRace]);
            //FocusSubrace();
        }
        
    }

    private void ResetItems()
    {
        foreach (Transform child in raceItemMenuParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ButtonSelected(RaceItem button)
    {
        if (previousButton != null) previousButton.ButtonUnselected();
        previousButton = button;
    }


    // SPECIFIC RACE
    private void FocusRace(GameObject go)
    {
        DB.Race race = DB.Instance.GetRace(go.name);
        // Save race
        CharacterEditorMenu.Instance.basicPc.Race = race.index;

        title.text = race.name;

        // Subclases
        DB.Subrace subrace = null;
        if (race.subraces.Count == 0)
        {
            Destroy(dropdown);
        }
        else
        {
            List<string> list = new List<string>();
            foreach (string value in race.subraces)
            {
                list.Add(DB.Instance.GetSubrace(value).name);
            }

            dropdown.ClearOptions();
            dropdown.AddOptions(list);
            subrace = DB.Instance.GetSubrace(dropdown.options[dropdown.value].text);
        }

        foreach (Transform child in traitMenuParent.transform)
        {
            Destroy(child.gameObject);
        }
        traitsRace.Clear();
        traitsSubrace.Clear();
        
        // Age
        CreateTrait("Age", "", race.age, traitsRace);
        // Size
        CreateTrait("Size", race.size, race.size_description, traitsRace);
        // Speed
        CreateTrait("Speed", race.speed.ToString(), "", traitsRace);
        // Alignment
        CreateTrait("Alignment", "", race.alignment, traitsRace);
        // Traits
        foreach(string value in race.traits)
        {
            DB.Trait trait = DB.Instance.GetTrait(value);
            string auxiliar = "";
            foreach(string desc in trait.desc)
            {
                auxiliar += desc + "\n";
            }
            CreateTrait(trait.name, "", auxiliar, traitsRace);
        }

        foreach (Transform child in electionMenuParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        electionRace.Clear();
        electionSubrace.Clear();
        
        // Ability scores
        CreateElectionAbility("Ability Scores", race, subrace, electionRace);
        // Starting profs
        CreateElectionProf("Profeciencies", race, subrace, electionRace);
        // Languages
        CreateElectionLanguage("Languages", race, subrace, electionRace);


        if (subrace != null)
        {
            FocusSubrace();
        }
    }

    public void FocusSubrace()
    {
        DB.Subrace subrace = DB.Instance.GetSubrace(dropdown.options[dropdown.value].text);
        // Save subraces
        CharacterEditorMenu.Instance.basicPc.Subrace = subrace.index;

        // Description

        foreach (GameObject gameObject in traitsSubrace)
        {
            Destroy(gameObject);
        }
        traitsSubrace.Clear();

        // Traits
        foreach (string value in subrace.racial_traits)
        {
            DB.Trait trait = DB.Instance.GetTrait(value);
            string auxiliar = "";
            foreach (string desc in trait.desc)
            {
                auxiliar += desc + "\n";
            }
            CreateTrait(trait.name, "", auxiliar, traitsSubrace);
        }

        foreach (GameObject gameobject in electionSubrace)
        {
            Destroy(gameobject);
        }
        electionSubrace.Clear();
    }

    private void CreateTrait(string title, string subtitle, string text, List<GameObject> list2Save)
    {
        GameObject go = Instantiate(traitPrefab, traitMenuParent.transform);
        list2Save.Add(go);
        go.name = title;
        go.transform.GetChild(0).GetComponent<TMP_Text>().text = title;
        go.transform.GetChild(1).GetComponent<TMP_Text>().text = subtitle;
        go.transform.GetChild(2).GetComponent<TMP_Text>().text = text;
    }

    private void CreateElectionProf(string title, DB.Race race, DB.Subrace subrace, List<GameObject> list2Save)
    {
        // Return cases
        if (race.starting_proficiencies.Count == 0 && race.starting_proficiency_options.from.Count == 0 &&
            (subrace == null || (subrace.starting_proficiencies.Count == 0 && subrace.starting_proficiency_options.from.Count == 0)))
        {
            return;
        }

        // Specific of Proficiencies
        GameObject go = Instantiate(electionPrefab, electionMenuParent.transform);
        list2Save.Add(go);
        go.name = title;

        // Title
        go.transform.GetChild(0).GetComponent<TMP_Text>().text = title;

        // Build the text for starting proficiencies
        string auxiliar = "";
        foreach (string str in race.starting_proficiencies)
        {
            auxiliar += DB.Instance.GetProficiency(str).name + "\n";
        }
        if (subrace != null)
        {
            foreach (string str in subrace.starting_proficiencies)
            {
                auxiliar += DB.Instance.GetProficiency(str).name + "\n";
            }
        }
        go.transform.GetChild(1).GetComponent<TMP_Text>().text = auxiliar;

        // Options dropdown
        if (race.starting_proficiency_options.from.Count == 0 && (subrace == null || subrace.starting_proficiency_options.from.Count == 0))
        {
            Destroy(go.transform.GetChild(2).gameObject);
        }
        else
        {
            // Clean dropdowns
            foreach (Transform goTransform in go.transform.GetChild(2).transform)
            {
                Destroy(goTransform.gameObject);
            }
            for (int i = 0; i < race.starting_proficiency_options.choose; i++)
            {
                // Create dropdown
                GameObject new_dropdown = Instantiate(dropdownPrefab, go.transform.GetChild(2).transform);
                List<string> list = new List<string>();
                new_dropdown.GetComponent<TMP_Dropdown>().ClearOptions();
                // Add options
                foreach (string proficiencyId in race.starting_proficiency_options.from)
                {
                    list.Add(DB.Instance.GetProficiency(proficiencyId).name);
                }
                new_dropdown.GetComponent<TMP_Dropdown>().AddOptions(list);
            }
            if (subrace != null)
            {
                for (int i = 0; i < subrace.starting_proficiency_options.choose; i++)
                {
                    // Create dropdown
                    GameObject new_dropdown = Instantiate(dropdownPrefab, go.transform.GetChild(2).transform);
                    List<string> list = new List<string>();
                    new_dropdown.GetComponent<TMP_Dropdown>().ClearOptions();
                    // Add options
                    foreach (string proficiencyId in subrace.starting_proficiency_options.from)
                    {
                        list.Add(DB.Instance.GetProficiency(proficiencyId).name);
                    }
                    new_dropdown.GetComponent<TMP_Dropdown>().AddOptions(list);
                }
            }
        }

        // Description: Nothing to add
        go.transform.GetChild(3).GetComponent<TMP_Text>().text = "";
    }

    private void CreateElectionAbility(string title, DB.Race race, DB.Subrace subrace, List<GameObject> list2Save)
    {
        // Return cases
        if (race.ability_bonuses.Count == 0 && race.ability_bonus_options.from.Count == 0 &&
            subrace.ability_bonuses.Count == 0 && subrace.ability_bonus_options.from.Count == 0)
        {
            return;
        }

        // Specific of Ability bonus
        GameObject go = Instantiate(electionPrefab, electionMenuParent.transform);
        list2Save.Add(go);
        go.name = title;

        // Title
        go.transform.GetChild(0).GetComponent<TMP_Text>().text = title;

        // Build the text for ability improvements
        string auxiliar = "";
        foreach (DB.AbilityBonus abilityBonus in race.ability_bonuses)
        {
            auxiliar += "+" + abilityBonus.bonus + " " + DB.Instance.GetAbilityScore(abilityBonus.ability_score).full_name + "\n";
        }
        if (subrace != null) {
            foreach (DB.AbilityBonus abilityBonus in subrace.ability_bonuses)
            {
                auxiliar += "+" + abilityBonus.bonus + " " + DB.Instance.GetAbilityScore(abilityBonus.ability_score).full_name + "\n";
            } 
        }
        go.transform.GetChild(1).GetComponent<TMP_Text>().text = auxiliar;
        
        // Options dropdown
        if (race.ability_bonus_options.from.Count == 0 && (subrace == null || subrace.ability_bonus_options.from.Count == 0))
        {
            Destroy(go.transform.GetChild(2).gameObject);
        }
        else
        {
            // Clean dropdowns
            foreach (Transform goTransform in go.transform.GetChild(2).transform)
            {
                Destroy(goTransform.gameObject);
            }
            for (int i = 0; i < race.ability_bonus_options.choose; i++)
            {
                // Create dropdown
                GameObject new_dropdown = Instantiate(dropdownPrefab, go.transform.GetChild(2).transform);
                List<string> list = new List<string>();
                new_dropdown.GetComponent<TMP_Dropdown>().ClearOptions();
                // Add options
                foreach (string abilityId in race.ability_bonus_options.from)
                {
                    list.Add("+" + abilityId.Split('-')[1] + " "
                        + DB.Instance.GetAbilityScore(abilityId.Split('-')[0]).full_name);
                }
                new_dropdown.GetComponent<TMP_Dropdown>().AddOptions(list);
            }
            if (subrace != null)
            {
                for (int i = 0; i < subrace.ability_bonus_options.choose; i++)
                {
                    // Create dropdown
                    GameObject new_dropdown = Instantiate(dropdownPrefab, go.transform.GetChild(2).transform);
                    List<string> list = new List<string>();
                    new_dropdown.GetComponent<TMP_Dropdown>().ClearOptions();
                    // Add options
                    foreach (string abilityId in subrace.ability_bonus_options.from)
                    {
                        list.Add("+" + abilityId.Split('-')[1] + " "
                            + DB.Instance.GetAbilityScore(abilityId.Split('-')[0]).full_name);
                    }
                    new_dropdown.GetComponent<TMP_Dropdown>().AddOptions(list);
                }
            }
        }

        // Description: Nothing to add
        go.transform.GetChild(3).GetComponent<TMP_Text>().text = "";
    }

    private void CreateElectionLanguage(string title, DB.Race race, DB.Subrace subrace, List<GameObject> list2Save)
    {
        // Return cases
        if (race.languages.Count == 0 && race.language_options.from.Count == 0 &&
            subrace.languages.Count == 0 && subrace.language_options.from.Count == 0)
        {
            return;
        }

        // Specific of Languages
        GameObject go = Instantiate(electionPrefab, electionMenuParent.transform);
        list2Save.Add(go);
        go.name = title;

        // Title and description
        go.transform.GetChild(0).GetComponent<TMP_Text>().text = title;
        go.transform.GetChild(1).GetComponent<TMP_Text>().text = race.language_desc;
        
        // Options dropdown
        if (race.language_options.from.Count == 0 && (subrace == null || subrace.language_options.from.Count == 0))
        {
            Destroy(go.transform.GetChild(2).gameObject);
        }
        else
        {
            // Clean dropdowns
            foreach (Transform goTransform in go.transform.GetChild(2).transform)
            {
                Destroy(goTransform.gameObject);
            }
            for (int i = 0; i < race.language_options.choose; i++)
            {
                // Create dropdown
                GameObject new_dropdown = Instantiate(dropdownPrefab, go.transform.GetChild(2).transform);
                List<string> list = new List<string>();
                new_dropdown.GetComponent<TMP_Dropdown>().ClearOptions();
                // Add options
                foreach (string languageId in race.language_options.from)
                {
                    list.Add(DB.Instance.GetLanguages(languageId).name);
                }
                new_dropdown.GetComponent<TMP_Dropdown>().AddOptions(list);
            }
            if (subrace != null)
            {
                for (int i = 0; i < subrace.language_options.choose; i++)
                {
                    // Create dropdown
                    GameObject new_dropdown = Instantiate(dropdownPrefab, go.transform.GetChild(2).transform);
                    
                    List<string> list = new List<string>();
                    new_dropdown.GetComponent<TMP_Dropdown>().ClearOptions();
                    // Add options
                    foreach (string languageId in subrace.language_options.from)
                    {
                        list.Add(DB.Instance.GetLanguages(languageId).name);
                    }
                    new_dropdown.GetComponent<TMP_Dropdown>().AddOptions(list);
                }
            }
        }

        // Description: Nothing to add
        go.transform.GetChild(3).GetComponent<TMP_Text>().text = "";
    }

    // Set in a scropt in the dropdown prefabs
    /*private void setLanguage()
    {
        CharacterEditorMenu.Instance.basicPc.RaceChoices = ("language" + gameObject.transform.GetSiblingIndex(), value);
    }*/
}

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

    [SerializeField] private GameObject electionPrefab;
    [SerializeField] private GameObject electionMenuParent;

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

        this.basicPc = basicPC;
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

        if (basicPc != null && basicPc.Race != null)
        {
            ButtonSelected(raceButtons[basicPC.Race].GetComponentInChildren<RaceItem>());
            FocusRace(raceButtons[basicPc.Race]);
        }
        else
        {
            ButtonSelected(raceButtons[firstRace].GetComponentInChildren<RaceItem>());
            FocusRace(raceButtons[firstRace]);
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

        title.text = race.name;

        // Subclases
        dropdown.ClearOptions();
        dropdown.AddOptions(race.subraces);

        foreach (Transform child in traitMenuParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        // Age
        CreateTrait("Age", "", race.age);
        // Size
        CreateTrait("Size", race.size, race.size_description);
        // Speed
        CreateTrait("Speed", race.speed.ToString(), "");
        // Alignment
        CreateTrait("Alignment", "", race.alignment);
        // Traits
        foreach(string value in race.traits)
        {
            DB.Trait trait = DB.Instance.GetTrait(value);
            string auxiliar = "";
            foreach(string desc in trait.desc)
            {
                auxiliar += desc + "\n";
            }
            CreateTrait(trait.name, "", auxiliar);
        }

        foreach (Transform child in electionMenuParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        // Ability scores
        CreateElection("Ability Scores", race.ability_bonuses, race.ability_bonus_options);
        // Starting profs
        CreateElection("Profeciencies", race.starting_proficiencies, race.starting_proficiency_options);
        // Languages
        CreateElection("Languages", race.languages, race.language_options, race.language_desc);
    }

    private void CreateTrait(string title, string subtitle, string text)
    {
        GameObject go = Instantiate(traitPrefab, traitMenuParent.transform);
        go.name = title;
        go.transform.GetChild(0).GetComponent<TMP_Text>().text = title;
        go.transform.GetChild(1).GetComponent<TMP_Text>().text = subtitle;
        go.transform.GetChild(2).GetComponent<TMP_Text>().text = text;
    }

    private void CreateElection(string title, List<string> profs, DB.From prof_options)
    {
        if (profs.Count == 0 && prof_options.from.Count == 0)
        {
            return;
        }
        GameObject go = Instantiate(electionPrefab, electionMenuParent.transform);
        go.name = title;
        go.transform.GetChild(0).GetComponent<TMP_Text>().text = title;
        
        string auxiliar = "";
        foreach (string str in profs)
        {
            auxiliar += DB.Instance.GetProficiency(str).name + "\n";
        }
        go.transform.GetChild(1).GetComponent<TMP_Text>().text = auxiliar;

        if (prof_options.from.Count == 0)
        {
            Destroy(go.transform.GetChild(2).gameObject);
        } else
        {
            go.transform.GetChild(2).GetComponent<TMP_Dropdown>().ClearOptions();
            go.transform.GetChild(2).GetComponent<TMP_Dropdown>().AddOptions(prof_options.from);
        }
        go.transform.GetChild(3).GetComponent<TMP_Text>().text = "";
    }

    private void CreateElection(string title, List<DB.AbilityBonus> desc, DB.From ability_bonus_options)
    {
        if (desc.Count == 0 && ability_bonus_options.from.Count == 0)
        {
            return;
        }
        // Specific of Ability bonus
        GameObject go = Instantiate(electionPrefab, electionMenuParent.transform);
        go.name = title;
        go.transform.GetChild(0).GetComponent<TMP_Text>().text = title;

        string auxiliar = "";
        foreach (DB.AbilityBonus abilityBonus in desc)
        {
            auxiliar += abilityBonus.ability_score + "->" + abilityBonus.bonus + "\n";
        }
        go.transform.GetChild(1).GetComponent<TMP_Text>().text = auxiliar;
        if (ability_bonus_options.from.Count == 0)
        {
            Destroy(go.transform.GetChild(2).gameObject);
        }
        else
        {
            go.transform.GetChild(2).GetComponent<TMP_Dropdown>().ClearOptions();
            go.transform.GetChild(2).GetComponent<TMP_Dropdown>().AddOptions(ability_bonus_options.from);
        }
        go.transform.GetChild(3).GetComponent<TMP_Text>().text = "";
    }

    private void CreateElection(string title, List<string> languages, DB.From language_options, string language_desc)
    {
        if (languages.Count == 0 && language_options.from.Count == 0)
        {
            return;
        }
        // Specific of Languages
        GameObject go = Instantiate(electionPrefab, electionMenuParent.transform);
        go.name = title;
        go.transform.GetChild(0).GetComponent<TMP_Text>().text = title;
        go.transform.GetChild(1).GetComponent<TMP_Text>().text = language_desc;
        if (language_options.from.Count == 0)
        {
            Destroy(go.transform.GetChild(2).gameObject);
        }
        else
        {
            go.transform.GetChild(2).GetComponent<TMP_Dropdown>().ClearOptions();
            go.transform.GetChild(2).GetComponent<TMP_Dropdown>().AddOptions(language_options.from);
        }
    }
}

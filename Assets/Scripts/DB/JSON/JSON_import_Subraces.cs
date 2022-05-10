using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Subraces : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_Race
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_AbilityScore
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_AbilityBonus
    {
        public JS_AbilityScore ability_score;
        public int bonus;
    }

    [System.Serializable]
    public class JS_StartingProficiency
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_RacialTrait
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_From
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_LanguageOptions
    {
        public int choose;
        public List<JS_From> from;
        public string type;
    }

    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public string name;
        public JS_Race race;
        public string desc;
        public List<JS_AbilityBonus> ability_bonuses;
        public List<JS_StartingProficiency> starting_proficiencies;
        public List<string> languages;
        public List<JS_RacialTrait> racial_traits;
        public string url;
        public JS_LanguageOptions language_options;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Subraces.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Subrace[] subraces = new DB.Subrace[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Subrace aux = new DB.Subrace();

            //public string index;
            aux.index = jsonObject[i].index;
            //public string name;
            aux.name = jsonObject[i].name;
            //public JS_Race race;
            aux.parent_race = jsonObject[i].race.index;
            //public string desc;
            aux.desc = jsonObject[i].desc;
            //public List<JS_AbilityBonus> ability_bonuses;
            aux.ability_bonuses = new List<DB.AbilityBonus>();
            foreach (JS_AbilityBonus abilityBonus in jsonObject[i].ability_bonuses)
            {
                DB.AbilityBonus auxiliar = new DB.AbilityBonus();
                auxiliar.ability_score = abilityBonus.ability_score.index;
                auxiliar.bonus = abilityBonus.bonus;
                aux.ability_bonuses.Add(auxiliar);
            }
            //public List<JS_StartingProficiency> starting_proficiencies;
            aux.starting_proficiencies = new List<string>();
            foreach (JS_StartingProficiency startingProficiency in jsonObject[i].starting_proficiencies)
            {
                aux.starting_proficiencies.Add(startingProficiency.index);
            }
            //public List<object> languages;
            aux.languages = jsonObject[i].languages;
            //public List<JS_RacialTrait> racial_traits;
            aux.racial_traits = new List<string>();
            foreach (JS_RacialTrait racialTrait in jsonObject[i].racial_traits)
            {
                aux.racial_traits.Add(racialTrait.index);
            }
            //public JS_LanguageOptions language_options;
            aux.language_options = new DB.From();
            aux.language_options.choose = jsonObject[i].language_options.choose;
            aux.language_options.type = jsonObject[i].language_options.type;
            if (jsonObject[i].language_options.from != null)
            {
                aux.language_options.from = new List<string>();
                foreach (JS_From from in jsonObject[i].language_options.from)
                {
                    aux.language_options.from.Add(from.index);
                }
            }
            aux.starting_proficiency_options = new DB.From();
            aux.ability_bonus_options = new DB.From();

            subraces[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(subraces);
        string outputPath = Application.dataPath + "/Files/Definitive/Subraces.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

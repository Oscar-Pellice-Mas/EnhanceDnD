using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Races : MonoBehaviour
{
    // Temporal
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
    public class JS_From
    {
        public string index;
        public string name;
        public string url;
        public JS_AbilityScore ability_score;
        public int bonus;
    }

    [System.Serializable]
    public class JS_StartingProficiencyOptions
    {
        public int choose;
        public string type;
        public List<JS_From> from;
    }

    [System.Serializable]
    public class JS_Language
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Trait
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Subrace
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_LanguageOptions
    {
        public int choose;
        public string type;
        public List<JS_From> from;
    }

    [System.Serializable]
    public class JS_AbilityBonusOptions
    {
        public int choose;
        public string type;
        public List<JS_From> from;
    }

    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public string name;
        public int speed;
        public List<JS_AbilityBonus> ability_bonuses;
        public string alignment;
        public string age;
        public string size;
        public string size_description;
        public List<JS_StartingProficiency> starting_proficiencies;
        public JS_StartingProficiencyOptions starting_proficiency_options;
        public List<JS_Language> languages;
        public string language_desc;
        public List<JS_Trait> traits;
        public List<JS_Subrace> subraces;
        public string url;
        public JS_LanguageOptions language_options;
        public JS_AbilityBonusOptions ability_bonus_options;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Races.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Race[] races = new DB.Race[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Race aux = new DB.Race();

            //public string index;
            aux.index = jsonObject[i].index;
            //public string name;
            aux.name = jsonObject[i].name;
            //public int speed;
            aux.speed = jsonObject[i].speed;
            //public List<JS_AbilityBonus> ability_bonuses;
            aux.ability_bonuses = new List<DB.AbilityBonus>();
            foreach (JS_AbilityBonus abilityBonus in jsonObject[i].ability_bonuses)
            {
                DB.AbilityBonus auxiliar = new DB.AbilityBonus();
                auxiliar.ability_score = abilityBonus.ability_score.index;
                auxiliar.bonus = abilityBonus.bonus;
                aux.ability_bonuses.Add(auxiliar);
            }
            //public string alignment;
            aux.alignment = jsonObject[i].alignment;
            //public string age;
            aux.age = jsonObject[i].age;
            //public string size;
            aux.size = jsonObject[i].size;
            //public string size_description;
            aux.size_description = jsonObject[i].size_description;
            //public List<JS_StartingProficiency> starting_proficiencies;
            aux.starting_proficiencies = new List<string>();
            foreach (JS_StartingProficiency startingProficiency in jsonObject[i].starting_proficiencies)
            {
                aux.starting_proficiencies.Add(startingProficiency.index);
            }
            //public JS_StartingProficiencyOptions starting_proficiency_options;
            aux.starting_proficiency_options = new DB.From();
            aux.starting_proficiency_options.choose = jsonObject[i].starting_proficiency_options.choose;
            aux.starting_proficiency_options.type = jsonObject[i].starting_proficiency_options.type;
            if (jsonObject[i].starting_proficiency_options.from != null)
            {
                aux.starting_proficiency_options.from = new List<string>();
                foreach (JS_From from in jsonObject[i].starting_proficiency_options.from)
                {
                    aux.starting_proficiency_options.from.Add(from.index);
                }
            }
            
            //public List<JS_Language> languages;
            aux.languages = new List<string>();
            foreach (JS_Language language in jsonObject[i].languages)
            {
                aux.languages.Add(language.index);
            }
            //public string language_desc;
            aux.language_desc = jsonObject[i].language_desc;
            //public List<JS_Trait> traits;
            aux.traits = new List<string>();
            foreach (JS_Trait trait in jsonObject[i].traits)
            {
                aux.traits.Add(trait.index);
            }
            //public List<JS_Subrace> subraces;
            aux.subraces = new List<string>();
            foreach (JS_Subrace subrace in jsonObject[i].subraces)
            {
                aux.subraces.Add(subrace.index);
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
            
            //public JS_AbilityBonusOptions ability_bonus_options;
            aux.ability_bonus_options = new DB.From();
            aux.ability_bonus_options.choose = jsonObject[i].ability_bonus_options.choose;
            aux.ability_bonus_options.type = jsonObject[i].ability_bonus_options.type;
            if (jsonObject[i].ability_bonus_options.from != null)
            {
                aux.ability_bonus_options.from = new List<string>();
                foreach (JS_From from in jsonObject[i].ability_bonus_options.from)
                {
                    string auxiliar = from.ability_score.index + "-" + from.bonus.ToString();
                    aux.ability_bonus_options.from.Add(auxiliar);
                }
            }
            

            races[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(races);
        string outputPath = Application.dataPath + "/Files/Definitive/Races.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

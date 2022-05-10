using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Background : MonoBehaviour
{
    // Temporal
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
        public JS_EquipmentCategory equipment_category;
        public string desc;
        public List<JS_Alignment> alignments;
    }

    [System.Serializable]
    public class JS_LanguageOptions
    {
        public int choose;
        public string type;
        public List<JS_From> from;
    }

    [System.Serializable]
    public class JS_Equipment
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_StartingEquipment
    {
        public JS_Equipment equipment;
        public int quantity;
    }

    [System.Serializable]
    public class JS_EquipmentCategory
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_StartingEquipmentOption
    {
        public int choose;
        public string type;
        public JS_From from;
    }

    [System.Serializable]
    public class JS_Feature
    {
        public string name;
        public List<string> desc;
    }

    [System.Serializable]
    public class JS_PersonalityTraits
    {
        public int choose;
        public string type;
        public List<string> from;
    }

    [System.Serializable]
    public class JS_Alignment
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Ideals
    {
        public int choose;
        public string type;
        public List<JS_From> from;
    }

    [System.Serializable]
    public class JS_Bonds
    {
        public int choose;
        public string type;
        public List<string> from;
    }

    [System.Serializable]
    public class JS_Flaws
    {
        public int choose;
        public string type;
        public List<string> from;
    }

    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public string name;
        public List<JS_StartingProficiency> starting_proficiencies;
        public JS_LanguageOptions language_options;
        public List<JS_StartingEquipment> starting_equipment;
        public List<JS_StartingEquipmentOption> starting_equipment_options;
        public JS_Feature feature;
        public JS_PersonalityTraits personality_traits;
        public JS_Ideals ideals;
        public JS_Bonds bonds;
        public JS_Flaws flaws;
        public string url;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Backgrounds.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Background[] background = new DB.Background[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Background aux = new DB.Background();

            aux.index = jsonObject[i].index;
            aux.name = jsonObject[i].name;

            aux.starting_proficiencies = new List<string>();
            foreach (JS_StartingProficiency startingProficiency in jsonObject[i].starting_proficiencies)
            {
                string proficiency = startingProficiency.index;
                aux.starting_proficiencies.Add(proficiency);
            }

            aux.language_options = new DB.From();
            aux.language_options.choose = jsonObject[i].language_options.choose;
            aux.language_options.type = jsonObject[i].language_options.type;
            aux.language_options.from = new List<string>();
            foreach (JS_From obj in jsonObject[i].language_options.from)
            {
                aux.language_options.from.Add(obj.index);
            }

            aux.starting_equipment = new List<DB.StartingEquipment>();
            foreach (JS_StartingEquipment obj in jsonObject[i].starting_equipment)
            {
                DB.StartingEquipment startingEquipment = new DB.StartingEquipment();
                startingEquipment.equipment = obj.equipment.index;
                startingEquipment.quantity = obj.quantity;
                aux.starting_equipment.Add(startingEquipment);
            }

            aux.starting_equipment_options = new List<DB.StartingEquipmentOptionCategory>();
            foreach (JS_StartingEquipmentOption obj in jsonObject[i].starting_equipment_options)
            {
                DB.StartingEquipmentOptionCategory startingEquipmentOption = new DB.StartingEquipmentOptionCategory();
                startingEquipmentOption.choose = obj.choose;
                startingEquipmentOption.type = obj.type;
                startingEquipmentOption.fromCategory = obj.from.equipment_category.index;
                aux.starting_equipment_options.Add(startingEquipmentOption);
            }

            aux.feature = new DB.Info();
            aux.feature.name = jsonObject[i].feature.name;
            aux.feature.desc = new List<string>();
            foreach (string obj in jsonObject[i].feature.desc)
            {
                aux.feature.desc.Add(obj);
            }

            aux.personality_traits = new DB.From();
            aux.personality_traits.choose = jsonObject[i].personality_traits.choose;
            aux.personality_traits.type = jsonObject[i].personality_traits.type;
            aux.personality_traits.from = new List<string>();
            foreach (string obj in jsonObject[i].personality_traits.from)
            {
                aux.personality_traits.from.Add(obj);
            }

            aux.ideals = new DB.Ideals();
            aux.ideals.choose = jsonObject[i].ideals.choose;
            aux.ideals.type = jsonObject[i].ideals.type;
            aux.ideals.fromIdeals = new List<DB.FromIdeals>();
            foreach (JS_From obj in jsonObject[i].ideals.from)
            {
                DB.FromIdeals from = new DB.FromIdeals();
                from.desc = obj.desc;
                from.alignments = new List<string>();
                foreach (JS_Alignment obj2 in obj.alignments)
                {
                    from.alignments.Add(obj2.index);
                }
                aux.ideals.fromIdeals.Add(from);
            }

            aux.bonds = new DB.From();
            aux.bonds.choose = jsonObject[i].bonds.choose;
            aux.bonds.type = jsonObject[i].bonds.type;
            aux.bonds.from = new List<string>();
            foreach (string obj in jsonObject[i].bonds.from)
            {
                aux.bonds.from.Add(obj);
            }

            aux.flaws = new DB.From();
            aux.flaws.choose = jsonObject[i].flaws.choose;
            aux.flaws.type = jsonObject[i].flaws.type;
            aux.flaws.from = new List<string>();
            foreach (string obj in jsonObject[i].flaws.from)
            {
                aux.flaws.from.Add(obj);
            }

            background[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(background);
        string outputPath = Application.dataPath + "/Files/Definitive/Backgrounds.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

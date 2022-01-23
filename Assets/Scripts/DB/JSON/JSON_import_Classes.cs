using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Classes : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_From
    {
        public string index;
        public string name;
        public string url;
        public JS_AbilityScore ability_score;
        public int minimum_score;
    }

    [System.Serializable]
    public class JS_ProficiencyChoice
    {
        public int choose;
        public string type;
        public List<JS_From> from;
    }

    [System.Serializable]
    public class JS_Proficiency
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_SavingThrow
    {
        public string index;
        public string name;
        public string url;
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
    public class JS_StartingEquipmentOption
    {
        public int choose;
        public string type;
        public List<JS_StartingEquipment> from;
    }

    [System.Serializable]
    public class JS_AbilityScore
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Prerequisite
    {
        public JS_AbilityScore ability_score;
        public int minimum_score;
    }

    [System.Serializable]
    public class JS_PrerequisiteOptions
    {
        public string type;
        public int choose;
        public List<JS_From> from;
    }

    [System.Serializable]
    public class JS_MultiClassing
    {
        public List<JS_Prerequisite> prerequisites;
        public List<JS_Proficiency> proficiencies;
        public List<JS_ProficiencyChoice> proficiency_choices;
        public JS_PrerequisiteOptions prerequisite_options;
    }

    [System.Serializable]
    public class JS_Subclass
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_SpellcastingAbility
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Info
    {
        public string name;
        public List<string> desc;
    }

    [System.Serializable]
    public class JS_Spellcasting
    {
        public int level;
        public JS_SpellcastingAbility spellcasting_ability;
        public List<JS_Info> info;
    }

    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public string name;
        public int hit_die;
        public string desc;
        public List<JS_ProficiencyChoice> proficiency_choices;
        public List<JS_Proficiency> proficiencies;
        public List<JS_SavingThrow> saving_throws;
        public List<JS_StartingEquipment> starting_equipment;
        public List<JS_StartingEquipmentOption> starting_equipment_options;
        public string class_levels;
        public JS_MultiClassing multi_classing;
        public List<JS_Subclass> subclasses;
        public JS_Spellcasting spellcasting;
        public string spells;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Classes.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Class[] dnd_class = new DB.Class[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Class aux = new DB.Class();

            aux.index = jsonObject[i].index;
            aux.name = jsonObject[i].name;
            aux.hit_die = jsonObject[i].hit_die;
            aux.desc = jsonObject[i].desc;

            aux.proficiency_choices = new List<DB.ProficiencyChoice>();
            foreach (JS_ProficiencyChoice proficiencyChoice in jsonObject[i].proficiency_choices)
            {
                DB.ProficiencyChoice proficiencyAux = new DB.ProficiencyChoice();
                proficiencyAux.choose = proficiencyChoice.choose;
                proficiencyAux.type = proficiencyChoice.type;
                proficiencyAux.fromProficiencies = new List<string>();
                foreach (JS_From proficiency in proficiencyChoice.from)
                {
                    proficiencyAux.fromProficiencies.Add(proficiency.index);
                }
                aux.proficiency_choices.Add(proficiencyAux);
            }

            aux.proficiencies = new List<string>();
            foreach (JS_Proficiency proficiency in jsonObject[i].proficiencies)
            {
                aux.proficiencies.Add(proficiency.index);
            }

            aux.saving_throws = new List<string>();
            foreach (JS_SavingThrow savingThrow in jsonObject[i].saving_throws)
            {
                aux.saving_throws.Add(savingThrow.index);
            }

            aux.starting_equipment = new List<DB.StartingEquipment>();
            foreach (JS_StartingEquipment obj in jsonObject[i].starting_equipment)
            {
                DB.StartingEquipment startingEquipment = new DB.StartingEquipment();
                startingEquipment.equipment = obj.equipment.index;
                startingEquipment.quantity = obj.quantity;
                aux.starting_equipment.Add(startingEquipment);
            }

            aux.starting_equipment_options = new List<DB.From>();
            foreach (JS_StartingEquipmentOption obj in jsonObject[i].starting_equipment_options)
            {
                DB.StartingEquipmentOption startingEquipmentOption = new DB.StartingEquipmentOption();
                startingEquipmentOption.choose = obj.choose;
                startingEquipmentOption.type = obj.type;
                startingEquipmentOption.from = new List<DB.StartingEquipment>();
                foreach (JS_StartingEquipment equipment in obj.from)
                {
                    DB.StartingEquipment equip = new DB.StartingEquipment();
                    equip.equipment = equipment.equipment.index;
                    equip.quantity = equipment.quantity;
                    startingEquipmentOption.from.Add(equip);
                }
            }

            aux.multi_classing = new DB.MultiClassing();
            aux.multi_classing.prerequisites = new List<DB.Prerequisite>();
            foreach (JS_Prerequisite prerequisite in jsonObject[i].multi_classing.prerequisites)
            {
                DB.Prerequisite auxiliar = new DB.Prerequisite();
                auxiliar.ability_score = prerequisite.ability_score.index;
                auxiliar.minimum_score = prerequisite.minimum_score;
                aux.multi_classing.prerequisites.Add(auxiliar);
            }
            aux.multi_classing.proficiencies = new List<string>();
            foreach (JS_Proficiency proficiency in jsonObject[i].multi_classing.proficiencies)
            {
                aux.multi_classing.proficiencies.Add(proficiency.index);
            }

            if (jsonObject[i].multi_classing.prerequisite_options != null)
            {
                aux.multi_classing.prerequisite_options = new DB.PrerequisiteOptions();
                aux.multi_classing.prerequisite_options.type = jsonObject[i].multi_classing.prerequisite_options.type;
                aux.multi_classing.prerequisite_options.choose = jsonObject[i].multi_classing.prerequisite_options.choose;
                if (jsonObject[i].multi_classing.prerequisite_options.from != null)
                {
                    aux.multi_classing.prerequisite_options.fromPrerequisite = new List<DB.Prerequisite>();
                    foreach (JS_From from in jsonObject[i].multi_classing.prerequisite_options.from)
                    {
                        DB.Prerequisite auxiliar = new DB.Prerequisite();
                        auxiliar.ability_score = from.ability_score.index;
                        auxiliar.minimum_score = from.minimum_score;
                        aux.multi_classing.prerequisite_options.fromPrerequisite.Add(auxiliar);
                    }
                }
            }

            if (jsonObject[i].multi_classing.proficiency_choices != null)
            {
                aux.multi_classing.proficiency_choices = new List<DB.ProficiencyChoice>();
                foreach (JS_ProficiencyChoice proficiencyChoice in jsonObject[i].multi_classing.proficiency_choices)
                {
                    DB.ProficiencyChoice auxiliar = new DB.ProficiencyChoice();
                    auxiliar.choose = proficiencyChoice.choose;
                    auxiliar.type = proficiencyChoice.type;
                    auxiliar.fromProficiencies = new List<string>();
                    foreach (JS_From from in proficiencyChoice.from)
                    {
                        auxiliar.fromProficiencies.Add(from.index);
                    }
                    aux.multi_classing.proficiency_choices.Add(auxiliar);
                }
            }

            aux.subclasses = new List<string>();
            foreach (JS_Subclass subclass in jsonObject[i].subclasses)
            {
                aux.subclasses.Add(subclass.index);
            }

            if (jsonObject[i].spellcasting != null)
            {
                aux.spellcasting = new DB.Spellcasting();
                aux.spellcasting.level = jsonObject[i].spellcasting.level;

                if (jsonObject[i].spellcasting.spellcasting_ability != null)
                    aux.spellcasting.spellcasting_ability = jsonObject[i].spellcasting.spellcasting_ability.index;

                if (jsonObject[i].spellcasting.info != null)
                {
                    aux.spellcasting.info = new List<DB.Info>();
                    foreach (JS_Info info in jsonObject[i].spellcasting.info)
                    {
                        DB.Info auxiliar = new DB.Info();
                        auxiliar.desc = info.desc;
                        auxiliar.name = info.name;
                        aux.spellcasting.info.Add(auxiliar);
                    }
                }
            }

            dnd_class[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(dnd_class);
        string outputPath = Application.dataPath + "/Files/Definitive/Classes.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

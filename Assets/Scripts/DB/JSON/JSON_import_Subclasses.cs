using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Subclasses : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_Class
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Prerequisite
    {
        public string index;
        public string type;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Spell2
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Spell
    {
        public List<JS_Prerequisite> prerequisites;
        public JS_Spell2 spell;
    }

    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public JS_Class @class;
        public string name;
        public string subclass_flavor;
        public List<string> desc;
        public string subclass_levels;
        public string url;
        public List<JS_Spell> spells;
    }



    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Subclasses.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Subclass[] subclasses = new DB.Subclass[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Subclass aux = new DB.Subclass();

            //public string index;
            aux.index = jsonObject[i].index;
            //public string name;
            aux.name = jsonObject[i].name;
            //public List<string> desc;
            aux.desc = jsonObject[i].desc;
            //public JS_Class @class;
            aux.parent_class = jsonObject[i].@class.index;
            //public string subclass_flavor;
            aux.subclass_flavor = jsonObject[i].subclass_flavor;
            //public string subclass_levels;
            aux.subclass_levels = jsonObject[i].subclass_levels;
            //public List<JS_Spell> spells;
            aux.spells = new List<DB.SubclassSpell>();
            foreach (JS_Spell spell in jsonObject[i].spells)
            {
                DB.SubclassSpell auxiliar = new DB.SubclassSpell();
                auxiliar.spell = spell.spell.index;
                auxiliar.prerequisites = new List<DB.SpellPrerequisite>();
                foreach (JS_Prerequisite prerequisite in spell.prerequisites)
                {
                    DB.SpellPrerequisite spellPrerequisite = new DB.SpellPrerequisite();
                    auxiliar.prerequisites.Add(spellPrerequisite);
                }
                aux.spells.Add(auxiliar);
            }

            subclasses[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(subclasses);
        string outputPath = Application.dataPath + "/Files/Definitive/Subclasses.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

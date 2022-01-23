using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Proficiencies : MonoBehaviour
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
    public class JS_Race
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Reference
    {
        public string index;
        public string type;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public string type;
        public string name;
        public List<JS_Class> classes;
        public List<JS_Race> races;
        public string url;
        public List<JS_Reference> references;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Proficiencies.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Proficiency[] proficiencies = new DB.Proficiency[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Proficiency aux = new DB.Proficiency();

            //public string index;
            aux.index = jsonObject[i].index;
            //public string name;
            aux.name = jsonObject[i].name;
            //public string desc;
            aux.type = jsonObject[i].type;
            //public List<JS_Class> classes;
            aux.classes = new List<string>();
            foreach (JS_Class @class in jsonObject[i].classes)
            {
                aux.classes.Add(@class.index);
            }
            //public List<JS_Race> races;
            aux.races = new List<string>();
            foreach (JS_Race race in jsonObject[i].races)
            {
                aux.races.Add(race.index);
            }
            //public List<JS_Reference> references;
            aux.references = new List<DB.ProficienyReference>();
            foreach (JS_Reference reference in jsonObject[i].references)
            {
                DB.ProficienyReference auxiliar = new DB.ProficienyReference();
                auxiliar.index = reference.index;
                auxiliar.name = reference.name;
                auxiliar.type = reference.type;
                aux.references.Add(auxiliar);
            }

            proficiencies[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(proficiencies);
        string outputPath = Application.dataPath + "/Files/Definitive/Proficiencies.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

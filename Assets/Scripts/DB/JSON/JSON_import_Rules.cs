using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Rules : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_Subsection
    {
        public string name;
        public string index;
        public string url;
    }

    [System.Serializable]
    public class JS_Root
    {
        public string name;
        public string index;
        public string desc;
        public List<JS_Subsection> subsections;
        public string url;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Rules.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Rule[] rules = new DB.Rule[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Rule aux = new DB.Rule();

            //public string index;
            aux.index = jsonObject[i].index;
            //public string name;
            aux.name = jsonObject[i].name;
            //public string desc;
            aux.desc = jsonObject[i].desc;
            // subsections
            aux.subsections = new List<string>();
            foreach (JS_Subsection subsection in jsonObject[i].subsections)
            {
                aux.subsections.Add(subsection.index);
            }

            rules[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(rules);
        string outputPath = Application.dataPath + "/Files/Definitive/Rules.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

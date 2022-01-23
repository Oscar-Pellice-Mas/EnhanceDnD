using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_RuleSections : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_Root
    {
        public string name;
        public string index;
        public string desc;
        public string url;
    }



    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Rule-Sections.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.RuleSection[] ruleSections = new DB.RuleSection[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.RuleSection aux = new DB.RuleSection();

            //public string index;
            aux.index = jsonObject[i].index;
            //public string name;
            aux.name = jsonObject[i].name;
            //public string desc;
            aux.desc = jsonObject[i].desc;

            ruleSections[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(ruleSections);
        string outputPath = Application.dataPath + "/Files/Definitive/RuleSections.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

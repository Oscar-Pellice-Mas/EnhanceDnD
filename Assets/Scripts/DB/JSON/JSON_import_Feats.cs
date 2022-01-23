using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Feats : MonoBehaviour
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
    public class JS_Prerequisite
    {
        public JS_AbilityScore ability_score;
        public int minimum_score;
    }

    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public string name;
        public List<JS_Prerequisite> prerequisites;
        public List<string> desc;
        public string url;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Feats.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Feat[] feats = new DB.Feat[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Feat aux = new DB.Feat();
            aux.index = jsonObject[i].index;
            aux.name = jsonObject[i].name;
            aux.desc = new List<string>();
            foreach (string description in jsonObject[i].desc)
            {
                aux.desc.Add(description);
            }
            aux.prerequisites = new List<DB.Prerequisite>();
            foreach (JS_Prerequisite prerequisite in jsonObject[i].prerequisites)
            {
                DB.Prerequisite auxiliar = new DB.Prerequisite();
                auxiliar.ability_score = prerequisite.ability_score.index;
                auxiliar.minimum_score = prerequisite.minimum_score;
                aux.prerequisites.Add(auxiliar);
            }

            feats[i] = aux;

        }

        string jsonStringOut = JsonHelper.ToJson(feats);
        string outputPath = Application.dataPath + "/Files/Definitive/Feats.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

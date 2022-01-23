using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Skills : MonoBehaviour
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
    public class JS_Root
    {
        public string index;
        public string name;
        public List<string> desc;
        public JS_AbilityScore ability_score;
        public string url;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Skills.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Skill[] skills = new DB.Skill[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Skill aux = new DB.Skill();

            //public string index;
            aux.index = jsonObject[i].index;
            //public string name;
            aux.name = jsonObject[i].name;
            //public string desc;
            aux.desc = new List<string>();
            foreach (string description in jsonObject[i].desc)
            {
                aux.desc.Add(description);
            }
            //public JS_AbilityScore ability_score
            aux.ability_score = jsonObject[i].ability_score.index;

            skills[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(skills);
        string outputPath = Application.dataPath + "/Files/Definitive/Skills.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

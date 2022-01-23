using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Abilites : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_Skill
    {
        public string name;
        public string index;
        public string url;
    }

    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public string name;
        public string full_name;
        public List<string> desc;
        public List<JS_Skill> skills;
        public string url;
    }

    // Start is called before the first frame update
    void Start()
    {
        TemporalJSONRead();
    }

    private static void TemporalJSONRead()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Ability-Scores.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.AbilityScore[] abilities = new DB.AbilityScore[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.AbilityScore aux = new DB.AbilityScore();
            aux.index = jsonObject[i].index;
            aux.name = jsonObject[i].name;
            aux.full_name = jsonObject[i].full_name;
            aux.desc = jsonObject[i].desc;
            aux.skills = new List<string>();
            foreach (JS_Skill skill in jsonObject[i].skills)
            {
                aux.skills.Add(skill.index);
            }

            abilities[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(abilities);
        string outputPath = Application.dataPath + "/Files/Definitive/Ability-Scores.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Lenguages : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public string name;
        public string type;
        public List<string> typical_speakers;
        public string script;
        public string url;
        public string desc;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Languages.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Languages[] lenguages = new DB.Languages[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Languages aux = new DB.Languages();
            aux.index = jsonObject[i].index;
            aux.name = jsonObject[i].name;
            aux.type = jsonObject[i].type;
            aux.typical_speakers = new List<string>();
            foreach (string description in jsonObject[i].typical_speakers)
            {
                aux.typical_speakers.Add(description);
            }
            aux.script = jsonObject[i].script;
            aux.desc = jsonObject[i].desc;

            lenguages[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(lenguages);
        string outputPath = Application.dataPath + "/Files/Definitive/Languages.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

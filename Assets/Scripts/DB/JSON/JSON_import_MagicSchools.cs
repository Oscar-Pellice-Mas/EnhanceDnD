using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_MagicSchools : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public string name;
        public string desc;
        public string url;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Magic-Schools.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.MagicSchool[] magicSchools = new DB.MagicSchool[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.MagicSchool aux = new DB.MagicSchool();

            //public string index;
            aux.index = jsonObject[i].index;
            //public string name;
            aux.name = jsonObject[i].name;
            //public string desc;
            aux.desc = jsonObject[i].desc;

            magicSchools[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(magicSchools);
        string outputPath = Application.dataPath + "/Files/Definitive/MagicSchools.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

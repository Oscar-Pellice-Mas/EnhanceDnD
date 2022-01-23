using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Alignments : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public string name;
        public string abbreviation;
        public string desc;
        public string url;
    }

    // Start is called before the first frame update
    void Start()
    {
        TemporalJSONRead();
    }

    private static void TemporalJSONRead()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Alignments.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Alignment[] alignment = new DB.Alignment[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Alignment aux = new DB.Alignment();
            aux.index = jsonObject[i].index;
            aux.name = jsonObject[i].name;
            aux.abbreviation = jsonObject[i].abbreviation;
            aux.desc = jsonObject[i].desc;

            alignment[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(alignment);
        string outputPath = Application.dataPath + "/Files/Definitive/Alignments.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);
    }
}

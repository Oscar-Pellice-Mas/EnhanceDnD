using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_DamageTypes : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public string name;
        public List<string> desc;
        public string url;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Damage-Types.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.DamageType[] damageTypes = new DB.DamageType[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.DamageType aux = new DB.DamageType();
            aux.index = jsonObject[i].index;
            aux.name = jsonObject[i].name;
            aux.desc = new List<string>();
            foreach (string description in jsonObject[i].desc)
            {
                aux.desc.Add(description);
            }
            damageTypes[i] = aux;

        }

        string jsonStringOut = JsonHelper.ToJson(damageTypes);
        string outputPath = Application.dataPath + "/Files/Definitive/DamageTypes.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

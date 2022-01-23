using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_WeaponProperties : MonoBehaviour
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
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Weapon-Properties.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.WeaponProperties[] traits = new DB.WeaponProperties[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.WeaponProperties aux = new DB.WeaponProperties();

            //public string index;
            aux.index = jsonObject[i].index;
            //public string name;
            aux.name = jsonObject[i].name;
            //public List<string> desc;
            aux.desc = jsonObject[i].desc;
            

            traits[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(traits);
        string outputPath = Application.dataPath + "/Files/Definitive/WeaponProperties.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

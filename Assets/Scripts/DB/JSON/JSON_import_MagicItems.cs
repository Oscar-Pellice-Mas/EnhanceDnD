using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_MagicItems : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_EquipmentCategory
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
        public JS_EquipmentCategory equipment_category;
        public List<string> desc;
        public string url;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Magic-Items.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.MagicItem[] magicItems = new DB.MagicItem[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.MagicItem aux = new DB.MagicItem();

            //public string index;
            aux.index = jsonObject[i].index;
            //public string name;
            aux.name = jsonObject[i].name;
            //public string equipment_category;
            aux.equipment_category = jsonObject[i].equipment_category.index;
            //public List<string> desc;
            aux.desc = new List<string>();
            foreach (string description in jsonObject[i].desc)
            {
                aux.desc.Add(description);
            }

            magicItems[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(magicItems);
        string outputPath = Application.dataPath + "/Files/Definitive/MagicItems.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

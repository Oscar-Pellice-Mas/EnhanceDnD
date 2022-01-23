using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_EquipmentCategories : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_Equipment
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
        public List<JS_Equipment> equipment;
        public string url;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Equipment-Categories.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.EquipmentCategory[] equipmentCategory = new DB.EquipmentCategory[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.EquipmentCategory aux = new DB.EquipmentCategory();
            aux.index = jsonObject[i].index;
            aux.name = jsonObject[i].name;
            aux.equipment = new List<string>();
            foreach (JS_Equipment description in jsonObject[i].equipment)
            {
                aux.equipment.Add(description.index);
            }

            equipmentCategory[i] = aux;

        }

        string jsonStringOut = JsonHelper.ToJson(equipmentCategory);
        string outputPath = Application.dataPath + "/Files/Definitive/EquipmentCategories.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

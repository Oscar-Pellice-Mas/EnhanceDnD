using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Equipment : MonoBehaviour
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
    public class JS_Cost
    {
        public int quantity;
        public string unit;
    }
    [System.Serializable]
    public class JS_DamageType
    {
        public string index;
        public string name;
        public string url;
    }
    [System.Serializable]
    public class JS_Damage
    {
        public string damage_dice;
        public JS_DamageType damage_type;
    }
    [System.Serializable]
    public class JS_Range
    {
        public int normal;
        public int? @long;
    }
    [System.Serializable]
    public class JS_Property
    {
        public string index;
        public string name;
        public string url;
    }
    [System.Serializable]
    public class JS_ThrowRange
    {
        public int normal;
        public int @long;
    }
    [System.Serializable]
    public class JS_TwoHandedDamage
    {
        public string damage_dice;
        public JS_DamageType damage_type;
    }
    [System.Serializable]
    public class JS_ArmorClass
    {
        public int @base;
        public bool dex_bonus;
        public int? max_bonus;
    }
    [System.Serializable]
    public class JS_GearCategory
    {
        public string index;
        public string name;
        public string url;
    }
    [System.Serializable]
    public class JS_Item
    {
        public string index;
        public string name;
        public string url;
    }
    [System.Serializable]
    public class JS_Content
    {
        public JS_Item item;
        public int quantity;
    }
    [System.Serializable]
    public class JS_Speed
    {
        public double quantity;
        public string unit;
    }
    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public string name;
        public JS_EquipmentCategory equipment_category;
        public string weapon_category;
        public string weapon_range;
        public string category_range;
        public JS_Cost cost;
        public JS_Damage damage;
        public JS_Range range;
        public double weight;
        public List<JS_Property> properties;
        public string url;
        public JS_ThrowRange throw_range;
        public JS_TwoHandedDamage two_handed_damage;
        public List<string> special;
        public string armor_category;
        public JS_ArmorClass armor_class;
        public int? str_minimum;
        public bool? stealth_disadvantage;
        public JS_GearCategory gear_category;
        public List<string> desc;
        public int? quantity;
        public List<JS_Content> contents;
        public string tool_category;
        public string vehicle_category;
        public JS_Speed speed;
        public string capacity;
    }


    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Equipment.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Equipment[] dnd_class = new DB.Equipment[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Equipment aux = new DB.Equipment();

            //index;
            aux.index = jsonObject[i].index;
            //name;
            aux.name = jsonObject[i].name;
            //equipment_category;
            aux.equipment_category = jsonObject[i].equipment_category.index;
            //weapon_category;
            aux.weapon_category = jsonObject[i].weapon_category;
            //weapon_range;
            aux.weapon_range = jsonObject[i].weapon_range;
            //category_range;
            aux.category_range = jsonObject[i].category_range;
            //cost;
            aux.cost = new DB.Cost();
            aux.cost.quantity = jsonObject[i].cost.quantity;
            aux.cost.unit = jsonObject[i].cost.unit;
            //damage;
            aux.damage = new DB.Damage();
            aux.damage.damage_dice = jsonObject[i].damage.damage_dice;
            if (jsonObject[i].damage.damage_type != null)
                aux.damage.damage_type = jsonObject[i].damage.damage_type.index;
            //range;
            aux.range = new DB.Range();
            aux.range.normal_range = jsonObject[i].range.normal;
            if (jsonObject[i].range.@long != null)
                aux.range.long_range = jsonObject[i].range.@long;
            //weight;
            aux.weight = jsonObject[i].weight;
            //properties;
            aux.properties = new List<string>();
            foreach (JS_Property property in jsonObject[i].properties)
            {
                aux.properties.Add(property.index);
            }
            //throw_range;
            aux.throw_range = new DB.Range();
            aux.throw_range.normal_range = jsonObject[i].throw_range.normal;
            aux.throw_range.long_range = jsonObject[i].throw_range.@long;
            //two_handed_damage;
            aux.two_handed_damage = new DB.TwoHandedDamage();
            aux.two_handed_damage.damage_dice  = jsonObject[i].two_handed_damage.damage_dice;
            if (jsonObject[i].two_handed_damage.damage_dice != null)
                aux.two_handed_damage.damage_type = jsonObject[i].two_handed_damage.damage_type.index;
            //special;
            aux.special = new List<string>();
            foreach (string special in jsonObject[i].special)
            {
                aux.special.Add(special);
            }
            //armor_category;
            aux.armor_category = jsonObject[i].armor_category;
            //armor_class;
            aux.armor_class = new DB.ArmorClass();
            aux.armor_class.base_armor = jsonObject[i].armor_class.@base;
            aux.armor_class.dex_bonus = jsonObject[i].armor_class.dex_bonus;
            aux.armor_class.max_bonus = jsonObject[i].armor_class.max_bonus;
            //str_minimum;
            aux.str_minimum = jsonObject[i].str_minimum;
            //stealth_disadvantage;
            aux.stealth_disadvantage = jsonObject[i].stealth_disadvantage;
            //gear_category;
            aux.gear_category = jsonObject[i].gear_category.index;
            //desc;
            aux.desc = new List<string>();
            foreach (string decription in aux.desc)
            {
                aux.desc.Add(decription);
            }
            //quantity;
            aux.quantity = jsonObject[i].quantity;
            //contents;
            aux.contents = new List<DB.Content>();
            foreach (JS_Content content in jsonObject[i].contents)
            {
                DB.Content auxiliar = new DB.Content();
                auxiliar.item = content.item.index;
                auxiliar.quantity = content.quantity;
            }
            //tool_category;
            aux.tool_category = jsonObject[i].tool_category;
            //vehicle_category;
            aux.vehicle_category = jsonObject[i].vehicle_category;
            //speed;
            aux.speed = new DB.Speed();
            aux.speed.quantity = jsonObject[i].speed.quantity;
            aux.speed.unit = jsonObject[i].speed.unit;
            //capacity;
            aux.capacity = jsonObject[i].capacity;

            dnd_class[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(dnd_class);
        string outputPath = Application.dataPath + "/Files/Definitive/Equipment.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

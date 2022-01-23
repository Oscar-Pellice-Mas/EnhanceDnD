using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Spells : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_DamageType
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_DamageAtSlotLevel
    {
        public string _2;
        public string _3;
        public string _4;
        public string _5;
        public string _6;
        public string _7;
        public string _8;
        public string _9;
        public string _1;
    }

    [System.Serializable]
    public class JS_DamageAtCharacterLevel
    {
        public string _1;
        public string _5;
        public string _11;
        public string _17;
    }

    [System.Serializable]
    public class JS_Damage
    {
        public JS_DamageType damage_type;
        public JS_DamageAtSlotLevel damage_at_slot_level;
        public JS_DamageAtCharacterLevel damage_at_character_level;
    }

    [System.Serializable]
    public class JS_School
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Class
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Subclass
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_DcType
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Dc
    {
        public JS_DcType dc_type;
        public string dc_success;
        public string desc;
    }

    [System.Serializable]
    public class JS_HealAtSlotLevel
    {
        public string _2;
        public string _3;
        public string _4;
        public string _5;
        public string _6;
        public string _7;
        public string _8;
        public string _9;
        public string _1;
    }

    [System.Serializable]
    public class JS_AreaOfEffect
    {
        public string type;
        public int size;
    }

    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public string name;
        public List<string> desc;
        public List<string> higher_level;
        public string range;
        public List<string> components;
        public string material;
        public bool ritual;
        public string duration;
        public bool concentration;
        public string casting_time;
        public int level;
        public string attack_type;
        public JS_Damage damage;
        public JS_School school;
        public List<JS_Class> classes;
        public List<JS_Subclass> subclasses;
        public string url;
        public JS_Dc dc;
        public JS_HealAtSlotLevel heal_at_slot_level;
        public JS_AreaOfEffect area_of_effect;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Spells.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Spell[] spells = new DB.Spell[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Spell aux = new DB.Spell();

            //public string index;
            aux.index = jsonObject[i].index;
            //public string name;
            aux.name = jsonObject[i].name;
            //public List<string> desc;
            aux.desc = jsonObject[i].desc;
            //public List<string> higher_level;
            aux.higher_level = jsonObject[i].higher_level;
            //public string range;
            aux.range = jsonObject[i].range;
            //public List<string> components;
            aux.components = jsonObject[i].components;
            //public string material;
            aux.material = jsonObject[i].material;
            //public bool ritual;
            aux.ritual = jsonObject[i].ritual;
            //public string duration;
            aux.duration = jsonObject[i].duration;
            //public bool concentration;
            aux.concentration = jsonObject[i].concentration;
            //public string casting_time;
            aux.casting_time = jsonObject[i].casting_time;
            //public int level;
            aux.level = jsonObject[i].level;
            //public string attack_type;
            aux.attack_type = jsonObject[i].attack_type;
            //public JS_Damage damage;
            aux.damage = new DB.SpellDamage();
            if (jsonObject[i].damage.damage_at_character_level != null)
            {
                aux.damage.damage_at_character_level = new DB.DamageAtCharacterLevel();
                aux.damage.damage_at_character_level._1 = jsonObject[i].damage.damage_at_character_level._1;
                aux.damage.damage_at_character_level._5 = jsonObject[i].damage.damage_at_character_level._5;
                aux.damage.damage_at_character_level._11 = jsonObject[i].damage.damage_at_character_level._11;
                aux.damage.damage_at_character_level._17 = jsonObject[i].damage.damage_at_character_level._17;
            }

            if (jsonObject[i].damage.damage_at_slot_level != null)
            {
                aux.damage.damage_at_slot_level = new DB.DamageAtSlotLevel();
                aux.damage.damage_at_slot_level._1 = jsonObject[i].damage.damage_at_slot_level._1;
                aux.damage.damage_at_slot_level._2 = jsonObject[i].damage.damage_at_slot_level._2;
                aux.damage.damage_at_slot_level._3 = jsonObject[i].damage.damage_at_slot_level._3;
                aux.damage.damage_at_slot_level._4 = jsonObject[i].damage.damage_at_slot_level._4;
                aux.damage.damage_at_slot_level._5 = jsonObject[i].damage.damage_at_slot_level._5;
                aux.damage.damage_at_slot_level._6 = jsonObject[i].damage.damage_at_slot_level._6;
                aux.damage.damage_at_slot_level._7 = jsonObject[i].damage.damage_at_slot_level._7;
                aux.damage.damage_at_slot_level._8 = jsonObject[i].damage.damage_at_slot_level._8;
                aux.damage.damage_at_slot_level._9 = jsonObject[i].damage.damage_at_slot_level._9;
            }
            if (jsonObject[i].damage.damage_type != null)
                aux.damage.damage_type = jsonObject[i].damage.damage_type.index;
            
            //public JS_School school;
            aux.school = jsonObject[i].school.index;
            //public List<JS_Class> classes;
            aux.classes = new List<string>();
            foreach (JS_Class @class in jsonObject[i].classes)
            {
                
                aux.classes.Add(@class.index);
            }
            //public List<JS_Subclass> subclasses;
            aux.subclasses = new List<string>();
            foreach (JS_Subclass @class in jsonObject[i].subclasses)
            {
                aux.subclasses.Add(@class.index);
            }
            //public JS_Dc dc;
            aux.dc = new DB.Dc();
            aux.dc.dc_success = jsonObject[i].dc.dc_success;
            if (jsonObject[i].dc.dc_type != null)
            {
                aux.dc.dc_type = jsonObject[i].dc.dc_type.index;
            }
            aux.dc.desc = jsonObject[i].dc.desc;
            //public JS_HealAtSlotLevel heal_at_slot_level;
            aux.heal_at_slot_level = new DB.HealAtSlotLevel();
            aux.heal_at_slot_level._1 = jsonObject[i].heal_at_slot_level._1;
            aux.heal_at_slot_level._2 = jsonObject[i].heal_at_slot_level._2;
            aux.heal_at_slot_level._3 = jsonObject[i].heal_at_slot_level._3;
            aux.heal_at_slot_level._4 = jsonObject[i].heal_at_slot_level._4;
            aux.heal_at_slot_level._5 = jsonObject[i].heal_at_slot_level._5;
            aux.heal_at_slot_level._6 = jsonObject[i].heal_at_slot_level._6;
            aux.heal_at_slot_level._7 = jsonObject[i].heal_at_slot_level._7;
            aux.heal_at_slot_level._8 = jsonObject[i].heal_at_slot_level._8;
            aux.heal_at_slot_level._9 = jsonObject[i].heal_at_slot_level._9;
            //public JS_AreaOfEffect area_of_effect;
            aux.area_of_effect = new DB.AreaOfEffect();
            aux.area_of_effect.size = jsonObject[i].area_of_effect.size;
            aux.area_of_effect.type = jsonObject[i].area_of_effect.type;

            spells[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(spells);
        string outputPath = Application.dataPath + "/Files/Definitive/Spells.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

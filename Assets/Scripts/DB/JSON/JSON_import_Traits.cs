using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Traits : MonoBehaviour
{
    // Temporal
    [System.Serializable] 
    public class JS_Race
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Subrace
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Proficiency
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_From
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_ProficiencyChoices
    {
        public int choose;
        public string type;
        public List<JS_From> from;
    }

    [System.Serializable]
    public class JS_SpellOptions
    {
        public int choose;
        public List<JS_From> from;
        public string type;
    }

    [System.Serializable]
    public class JS_SubtraitOptions
    {
        public int choose;
        public List<JS_From> from;
        public string type;
    }

    [System.Serializable]
    public class JS_DamageType
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Usage
    {
        public string type;
        public int times;
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
        public string success_type;
    }

    [System.Serializable]
    public class JS_DamageAtCharacterLevel
    {
        public string _1;
        public string _6;
        public string _11;
        public string _16;
    }

    [System.Serializable]
    public class JS_Damage
    {
        public JS_DamageType damage_type;
        public JS_DamageAtCharacterLevel damage_at_character_level;
    }

    [System.Serializable]
    public class JS_BreathWeapon
    {
        public string name;
        public string desc;
        public JS_Usage usage;
        public JS_Dc dc;
        public List<JS_Damage> damage;
    }

    [System.Serializable]
    public class JS_TraitSpecific
    {
        public JS_SpellOptions spell_options;
        public JS_SubtraitOptions subtrait_options;
        public JS_DamageType damage_type;
        public JS_BreathWeapon breath_weapon;
    }

    [System.Serializable]
    public class JS_Parent
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public List<JS_Race> races;
        public List<JS_Subrace> subraces;
        public string name;
        public List<string> desc;
        public List<JS_Proficiency> proficiencies;
        public string url;
        public JS_ProficiencyChoices proficiency_choices;
        public JS_TraitSpecific trait_specific;
        public JS_Parent parent;
    }


    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Traits.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Trait[] traits = new DB.Trait[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Trait aux = new DB.Trait();

            //public string index;
            aux.index = jsonObject[i].index;
            //public string name;
            aux.name = jsonObject[i].name;
            //public List<string> desc;
            aux.desc = jsonObject[i].desc;
            //public List<JS_Race> races;
            aux.races = new List<string>();
            foreach (JS_Race race in jsonObject[i].races)
            {
                aux.races.Add(race.index);
            }
            //public List<JS_Subrace> subraces;
            aux.subraces = new List<string>();
            foreach (JS_Subrace subrace in jsonObject[i].subraces)
            {
                aux.subraces.Add(subrace.index);
            }
            //public List<JS_Proficiency> proficiencies;
            aux.proficiencies = new List<string>();
            foreach (JS_Proficiency proficiency in jsonObject[i].proficiencies)
            {
                aux.proficiencies.Add(proficiency.index);
            }
            //public JS_ProficiencyChoices proficiency_choices;
            if (jsonObject[i].proficiency_choices.from != null)
            {
                aux.proficiency_choices = new DB.ProficiencyChoice();
                aux.proficiency_choices.choose = jsonObject[i].proficiency_choices.choose;
                aux.proficiency_choices.type = jsonObject[i].proficiency_choices.type;
                aux.proficiency_choices.fromProficiencies = new List<string>();
                foreach (JS_From from in jsonObject[i].proficiency_choices.from)
                {
                    aux.proficiency_choices.fromProficiencies.Add(from.index);
                }
            }

            //public JS_TraitSpecific trait_specific;
            if (jsonObject[i].trait_specific != null)
            {
                aux.trait_specific = new DB.TraitSpecific();
                if (aux.trait_specific.breath_weapon != null)
                {
                    aux.trait_specific.breath_weapon = new DB.BreathWeapon();
                    aux.trait_specific.breath_weapon.damage = new List<DB.BreathWeaponDamage>();
                    foreach (JS_Damage damage in jsonObject[i].trait_specific.breath_weapon.damage)
                    {
                        DB.BreathWeaponDamage breathWeaponDamage = new DB.BreathWeaponDamage();
                        breathWeaponDamage.damage_type = damage.damage_type.index;
                        breathWeaponDamage.damage_at_1 = damage.damage_at_character_level._1;
                        breathWeaponDamage.damage_at_6 = damage.damage_at_character_level._6;
                        breathWeaponDamage.damage_at_11 = damage.damage_at_character_level._11;
                        breathWeaponDamage.damage_at_16 = damage.damage_at_character_level._16;
                        aux.trait_specific.breath_weapon.damage.Add(breathWeaponDamage);
                    }
                }

                if (jsonObject[i].trait_specific.damage_type != null)
                {
                    aux.trait_specific.damage_type = jsonObject[i].trait_specific.damage_type.index;
                }

                if (jsonObject[i].trait_specific.spell_options != null)
                {
                    aux.trait_specific.spell_options = new DB.SpellOptions(); 
                    aux.trait_specific.spell_options.choose = jsonObject[i].trait_specific.spell_options.choose;
                    aux.trait_specific.spell_options.type = jsonObject[i].trait_specific.spell_options.type;
                    if (jsonObject[i].trait_specific.spell_options.from != null)
                    {
                        aux.trait_specific.spell_options.fromSpells = new List<string>();
                        foreach (JS_From from in jsonObject[i].trait_specific.spell_options.from)
                        {
                            aux.trait_specific.spell_options.fromSpells.Add(from.index);
                        }
                    }
                    
                }

                if (jsonObject[i].trait_specific.subtrait_options != null)
                {
                    aux.trait_specific.subtrait_options = new DB.SubtraitOptions(); 
                    aux.trait_specific.subtrait_options.choose = jsonObject[i].trait_specific.subtrait_options.choose;
                    aux.trait_specific.subtrait_options.type = jsonObject[i].trait_specific.subtrait_options.type;
                    if (jsonObject[i].trait_specific.subtrait_options.from != null)
                    {
                        aux.trait_specific.subtrait_options.fromSubtraits = new List<string>();
                        foreach (JS_From from in jsonObject[i].trait_specific.subtrait_options.from)
                        {
                            aux.trait_specific.subtrait_options.fromSubtraits.Add(from.index);
                        }
                    }
                }
            }
            
            //public JS_Parent parent;
            aux.parent = jsonObject[i].parent.index;

            traits[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(traits);
        string outputPath = Application.dataPath + "/Files/Definitive/Traits.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

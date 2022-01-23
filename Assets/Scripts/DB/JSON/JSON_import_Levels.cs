using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Levels : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_Feature
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_MartialArts
    {
        public int dice_count;
        public int dice_value;
    }

    [System.Serializable]
    public class JS_SneakAttack
    {
        public int dice_count;
        public int dice_value;
    }

    [System.Serializable]
    public class JS_CreatingSpellSlot
    {
        public int spell_slot_level;
        public int sorcery_point_cost;
    }

    [System.Serializable]
    public class JS_ClassSpecific
    {
        public int rage_count;
        public int rage_damage_bonus;
        public int brutal_critical_dice;
        public int? bardic_inspiration_die;
        public int? song_of_rest_die;
        public int? magical_secrets_max_5;
        public int? magical_secrets_max_7;
        public int? magical_secrets_max_9;
        public int? channel_divinity_charges;
        public double? destroy_undead_cr;
        public double? wild_shape_max_cr;
        public bool? wild_shape_swim;
        public bool? wild_shape_fly;
        public int? action_surges;
        public int? indomitable_uses;
        public int? extra_attacks;
        public JS_MartialArts martial_arts;
        public int? ki_points;
        public int? unarmored_movement;
        public int? aura_range;
        public int? favored_enemies;
        public int? favored_terrain;
        public JS_SneakAttack sneak_attack;
        public int? sorcery_points;
        public int? metamagic_known;
        public List<JS_CreatingSpellSlot> creating_spell_slots;
        public int? invocations_known;
        public int? mystic_arcanum_level_6;
        public int? mystic_arcanum_level_7;
        public int? mystic_arcanum_level_8;
        public int? mystic_arcanum_level_9;
        public int? arcane_recovery_levels;
    }

    [System.Serializable]
    public class JS_Class
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Spellcasting
    {
        public int cantrips_known;
        public int spells_known;
        public int spell_slots_level_1;
        public int spell_slots_level_2;
        public int spell_slots_level_3;
        public int spell_slots_level_4;
        public int spell_slots_level_5;
        public int spell_slots_level_6;
        public int spell_slots_level_7;
        public int spell_slots_level_8;
        public int spell_slots_level_9;
    }

    [System.Serializable]
    public class JS_Subclass
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_SubclassSpecific
    {
        public int additional_magical_secrets_max_lvl;
        public int? aura_range;
    }

    [System.Serializable]
    public class JS_Root
    {
        public int level;
        public int ability_score_bonuses;
        public int prof_bonus;
        public List<JS_Feature> features;
        public JS_ClassSpecific class_specific;
        public string index;
        public JS_Class @class;
        public string url;
        public JS_Spellcasting spellcasting;
        public JS_Subclass subclass;
        public JS_SubclassSpecific subclass_specific;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Levels.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Level[] levels = new DB.Level[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Level aux = new DB.Level();

            //public int level;
            aux.level = jsonObject[i].level;
            //public int ability_score_bonuses;
            aux.ability_score_bonuses = jsonObject[i].ability_score_bonuses;
            //public int prof_bonus;
            aux.prof_bonus = jsonObject[i].prof_bonus;
            //public List<string> features;
            aux.features = new List<string>();
            foreach (JS_Feature feature in jsonObject[i].features)
            {
                aux.features.Add(feature.index);
            }
            //public ClassSpecific class_specific;
            aux.class_specific = new DB.ClassSpecific();
            aux.class_specific.action_surges = jsonObject[i].class_specific.action_surges;
            aux.class_specific.arcane_recovery_levels = jsonObject[i].class_specific.arcane_recovery_levels;
            aux.class_specific.aura_range = jsonObject[i].class_specific.aura_range;
            aux.class_specific.bardic_inspiration_die = jsonObject[i].class_specific.bardic_inspiration_die;
            aux.class_specific.brutal_critical_dice = jsonObject[i].class_specific.brutal_critical_dice;
            aux.class_specific.channel_divinity_charges = jsonObject[i].class_specific.channel_divinity_charges;
            if (jsonObject[i].class_specific.creating_spell_slots != null)
            {
                aux.class_specific.creating_spell_slots = new List<DB.CreatingSpellSlot>();
                foreach (JS_CreatingSpellSlot creatingSpellSlot in jsonObject[i].class_specific.creating_spell_slots)
                {
                    DB.CreatingSpellSlot spellSlot = new DB.CreatingSpellSlot();
                    spellSlot.sorcery_point_cost = creatingSpellSlot.sorcery_point_cost;
                    spellSlot.spell_slot_level = creatingSpellSlot.spell_slot_level;
                    aux.class_specific.creating_spell_slots.Add(spellSlot);
                }
            }
            aux.class_specific.destroy_undead_cr = jsonObject[i].class_specific.destroy_undead_cr;
            aux.class_specific.extra_attacks = jsonObject[i].class_specific.extra_attacks;
            aux.class_specific.favored_enemies = jsonObject[i].class_specific.favored_enemies;
            aux.class_specific.indomitable_uses = jsonObject[i].class_specific.indomitable_uses;
            aux.class_specific.invocations_known = jsonObject[i].class_specific.invocations_known;
            aux.class_specific.ki_points = jsonObject[i].class_specific.ki_points;
            aux.class_specific.magical_secrets_max_5 = jsonObject[i].class_specific.magical_secrets_max_5;
            aux.class_specific.magical_secrets_max_7 = jsonObject[i].class_specific.magical_secrets_max_7;
            aux.class_specific.magical_secrets_max_9 = jsonObject[i].class_specific.magical_secrets_max_9;
            if (jsonObject[i].class_specific.martial_arts != null)
            {
                aux.class_specific.martial_arts = new DB.Dice();
                aux.class_specific.martial_arts.dice_count = jsonObject[i].class_specific.martial_arts.dice_count;
                aux.class_specific.martial_arts.dice_value = jsonObject[i].class_specific.martial_arts.dice_value;
            }
            aux.class_specific.metamagic_known = jsonObject[i].class_specific.metamagic_known;
            aux.class_specific.mystic_arcanum_level_6 = jsonObject[i].class_specific.mystic_arcanum_level_6;
            aux.class_specific.mystic_arcanum_level_7 = jsonObject[i].class_specific.mystic_arcanum_level_7;
            aux.class_specific.mystic_arcanum_level_8 = jsonObject[i].class_specific.mystic_arcanum_level_8;
            aux.class_specific.mystic_arcanum_level_9 = jsonObject[i].class_specific.mystic_arcanum_level_9;
            aux.class_specific.rage_count = jsonObject[i].class_specific.rage_count;
            aux.class_specific.rage_damage_bonus = jsonObject[i].class_specific.rage_damage_bonus;
            if (jsonObject[i].class_specific.sneak_attack != null)
            {
                aux.class_specific.sneak_attack = new DB.Dice();
                aux.class_specific.sneak_attack.dice_count = jsonObject[i].class_specific.sneak_attack.dice_count;
                aux.class_specific.sneak_attack.dice_value = jsonObject[i].class_specific.sneak_attack.dice_value;
            }
            aux.class_specific.song_of_rest_die = jsonObject[i].class_specific.song_of_rest_die;
            aux.class_specific.sorcery_points = jsonObject[i].class_specific.sorcery_points;
            aux.class_specific.unarmored_movement = jsonObject[i].class_specific.unarmored_movement;
            aux.class_specific.wild_shape_fly = jsonObject[i].class_specific.wild_shape_fly;
            aux.class_specific.wild_shape_max_cr = jsonObject[i].class_specific.wild_shape_max_cr;
            aux.class_specific.wild_shape_swim = jsonObject[i].class_specific.wild_shape_swim;

            //public string index;
            aux.index = jsonObject[i].index;
            //public string @class;
            aux.class_level = jsonObject[i].@class.index;
            //public Spellcasting spellcasting;
            aux.spellcasting = new DB.Spellslots();
            aux.spellcasting.cantrips_known = jsonObject[i].spellcasting.cantrips_known;
            aux.spellcasting.spells_known = jsonObject[i].spellcasting.spells_known;
            aux.spellcasting.spell_slots_level_1 = jsonObject[i].spellcasting.spell_slots_level_1;
            aux.spellcasting.spell_slots_level_2 = jsonObject[i].spellcasting.spell_slots_level_2;
            aux.spellcasting.spell_slots_level_3 = jsonObject[i].spellcasting.spell_slots_level_3;
            aux.spellcasting.spell_slots_level_4 = jsonObject[i].spellcasting.spell_slots_level_4;
            aux.spellcasting.spell_slots_level_5 = jsonObject[i].spellcasting.spell_slots_level_5;
            aux.spellcasting.spell_slots_level_6 = jsonObject[i].spellcasting.spell_slots_level_6;
            aux.spellcasting.spell_slots_level_7 = jsonObject[i].spellcasting.spell_slots_level_7;
            aux.spellcasting.spell_slots_level_8 = jsonObject[i].spellcasting.spell_slots_level_8;
            aux.spellcasting.spell_slots_level_9 = jsonObject[i].spellcasting.spell_slots_level_9;
            //public string subclass;
            aux.subclass = jsonObject[i].subclass.index;
            //public SubclassSpecific subclass_specific;
            aux.subclass_specific = new DB.SubclassSpecific();
            aux.subclass_specific.additional_magical_secrets_max_lvl = jsonObject[i].subclass_specific.additional_magical_secrets_max_lvl;
            aux.subclass_specific.aura_range = jsonObject[i].subclass_specific.aura_range;

            levels[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(levels);
        string outputPath = Application.dataPath + "/Files/Definitive/Levels.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

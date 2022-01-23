using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB : MonoBehaviour
{
    //Instance
    public static DB Instance;

    [SerializeField] private List<TextAsset> database;
    private Dictionary<string, TextAsset> dictAssets;

    private void Awake()
    {
        // Instance creation
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        // Class initialization
        dictAssets = new Dictionary<string, TextAsset>();
        foreach (TextAsset textAsset in database)
        {
            dictAssets.Add(textAsset.name, textAsset); 
        }

        Dictionary_initialization();
        Importer();
    }

    // Classes
    #region Class Definition
    [System.Serializable]
    public class AbilityScore
    {
        public string index;
        public string name;
        public string full_name;
        public List<string> desc;
        public List<string> skills;
    }

    [System.Serializable]
    public class Alignment
    {
        public string index;
        public string name;
        public string abbreviation;
        public string desc;
    }

    [System.Serializable]
    public class From
    {
        public int choose;
        public string type;
        public List<string> from;
    }

    [System.Serializable]
    public class StartingEquipment
    {
        public string equipment;
        public int quantity;
    }

    [System.Serializable]
    public class StartingEquipmentOption
    {
        public int choose;
        public string type;
        public List<StartingEquipment> from;
    }

    [System.Serializable]
    public class FromIdeals
    {
        public string desc;
        public List<string> alignments;
    }

    [System.Serializable]
    public class Ideals
    {
        public int choose;
        public string type;
        public List<FromIdeals> fromIdeals;
    }

    [System.Serializable]
    public class Background
    {
        public string index;
        public string name;
        public List<string> starting_proficiencies;
        public From language_options;
        public List<StartingEquipment> starting_equipment;
        public List<StartingEquipmentOption> starting_equipment_options;
        public Info feature;
        public From personality_traits;
        public Ideals ideals;
        public From bonds;
        public From flaws;
    }

    [System.Serializable]
    public class ProficiencyChoice
    {
        public int choose;
        public string type;
        public List<string> fromProficiencies;
    }

    [System.Serializable]
    public class Prerequisite
    {
        public string ability_score;
        public int minimum_score;
    }

    [System.Serializable]
    public class PrerequisiteOptions
    {
        public string type;
        public int choose;
        public List<Prerequisite> fromPrerequisite;
    }

    [System.Serializable]
    public class Info
    {
        public string name;
        public List<string> desc;
    }

    [System.Serializable]
    public class Spellcasting
    {
        public int level;
        public string spellcasting_ability;
        public List<Info> info;
    }

    [System.Serializable]
    public class MultiClassing
    {
        public List<Prerequisite> prerequisites;
        public List<string> proficiencies;
        public List<ProficiencyChoice> proficiency_choices;
        public PrerequisiteOptions prerequisite_options;
    }

    [System.Serializable]
    public class Condition
    {
        public string index;
        public string name;
        public List<string> desc;
    }

    [System.Serializable]
    public class Class
    {
        public string index;
        public string name;
        public string desc;
        public int hit_die;
        public List<ProficiencyChoice> proficiency_choices;
        public List<string> proficiencies;
        public List<string> saving_throws;
        public List<StartingEquipment> starting_equipment;
        public List<From> starting_equipment_options;
        public MultiClassing multi_classing;
        public List<string> subclasses;
        public Spellcasting spellcasting;
    }

    [System.Serializable]
    public class DamageType
    {
        public string index;
        public string name;
        public List<string> desc;
    }
    [System.Serializable]
    public class Cost
    {
        public int quantity;
        public string unit;
    }

    [System.Serializable]
    public class Damage
    {
        public string damage_dice;
        public string damage_type;
    }

    [System.Serializable]
    public class Range
    {
        public int normal_range;
        public int? long_range;
    }

    [System.Serializable]
    public class TwoHandedDamage
    {
        public string damage_dice;
        public string damage_type;
    }

    [System.Serializable]
    public class ArmorClass
    {
        public int base_armor;
        public bool dex_bonus;
        public int? max_bonus;
    }

    [System.Serializable]
    public class Content
    {
        public string item;
        public int quantity;
    }

    [System.Serializable]
    public class Speed
    {
        public double quantity;
        public string unit;
    }

    [System.Serializable]
    public class Equipment
    {
        public string index;
        public string name;
        public string equipment_category;
        public string weapon_category;
        public string weapon_range;
        public string category_range;
        public Cost cost;
        public Damage damage;
        public Range range;
        public double weight;
        public List<string> properties;
        public Range throw_range;
        public TwoHandedDamage two_handed_damage;
        public List<string> special;
        public string armor_category;
        public ArmorClass armor_class;
        public int? str_minimum;
        public bool? stealth_disadvantage;
        public string gear_category;
        public List<string> desc;
        public int? quantity;
        public List<Content> contents;
        public string tool_category;
        public string vehicle_category;
        public Speed speed;
        public string capacity;
    }

    [System.Serializable]
    public class EquipmentCategory
    {
        public string index;
        public string name;
        public List<string> equipment;
    }

    [System.Serializable]
    public class Feat
    {
        public string index;
        public string name;
        public List<Prerequisite> prerequisites;
        public List<string> desc;
    }

    [System.Serializable]
    public class FeaturePrerequisite
    {
        public string type;
        public string spell;
        public string feature;
        public int? level;
    }

    [System.Serializable]
    public class ExpertiseOptions
    {
        public int choose;
        public string type;
        public List<string> fromExpertises;
    }

    [System.Serializable]
    public class SubfeatureOptions
    {
        public int choose;
        public string type;
        public List<string> fromFeatures;
    }

    [System.Serializable]
    public class FeatureSpecific
    {
        public ExpertiseOptions expertise_options;
        public SubfeatureOptions subfeature_options;
    }

    [System.Serializable]
    public class Feature
    {
        public string index;
        public string feature_class;
        public string name;
        public int level;
        public List<FeaturePrerequisite> prerequisites;
        public List<string> desc;
        public string subclass;
        public string reference;
        public FeatureSpecific feature_specific;
        public string parent;
    }

    [System.Serializable]
    public class Languages
    {
        public string index;
        public string name;
        public string type;
        public List<string> typical_speakers;
        public string script;
        public string desc;
    }

    [System.Serializable]
    public class Dice
    {
        public int dice_count;
        public int dice_value;
    }

    [System.Serializable]
    public class CreatingSpellSlot
    {
        public int spell_slot_level;
        public int sorcery_point_cost;
    }

    [System.Serializable]
    public class ClassSpecific
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
        public Dice martial_arts;
        public int? ki_points;
        public int? unarmored_movement;
        public int? aura_range;
        public int? favored_enemies;
        public int? favored_terrain;
        public Dice sneak_attack;
        public int? sorcery_points;
        public int? metamagic_known;
        public List<CreatingSpellSlot> creating_spell_slots;
        public int? invocations_known;
        public int? mystic_arcanum_level_6;
        public int? mystic_arcanum_level_7;
        public int? mystic_arcanum_level_8;
        public int? mystic_arcanum_level_9;
        public int? arcane_recovery_levels;
    }

    [System.Serializable]
    public class Spellslots
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
    public class SubclassSpecific
    {
        public int additional_magical_secrets_max_lvl;
        public int? aura_range;
    }

    [System.Serializable]
    public class Level
    {
        public int level;
        public int ability_score_bonuses;
        public int prof_bonus;
        public List<string> features;
        public ClassSpecific class_specific;
        public string index;
        public string class_level;
        public Spellslots spellcasting;
        public string subclass;
        public SubclassSpecific subclass_specific;
    }

    [System.Serializable]
    public class MagicItem
    {
        public string index;
        public string name;
        public string equipment_category;
        public List<string> desc;
    }

    [System.Serializable]
    public class MagicSchool
    {
        public string index;
        public string name;
        public string desc;
    }

    [System.Serializable]
    public class ProficienyReference
    {
        public string index;
        public string type;
        public string name;
    }

    [System.Serializable]
    public class Proficiency
    {
        public string index;
        public string type;
        public string name;
        public List<string> classes;
        public List<string> races;
        public string url;
        public List<ProficienyReference> references;
    }

    [System.Serializable]
    public class AbilityBonus
    {
        public string ability_score;
        public int bonus;
    }

    [System.Serializable]
    public class StartingProficiencyOptions
    {
        public int choose;
        public string type;
        public List<string> fromProficiencies;
    }

    [System.Serializable]
    public class AbilityBonusOptions
    {
        public int choose;
        public string type;
        public List<AbilityBonus> fromAbilityBonus;
    }

    [System.Serializable]
    public class Race
    {
        public string index;
        public string name;
        public int speed;
        public List<AbilityBonus> ability_bonuses;
        public AbilityBonusOptions ability_bonus_options;
        public string alignment;
        public string age;
        public string size;
        public string size_description;
        public List<string> starting_proficiencies;
        public StartingProficiencyOptions starting_proficiency_options;
        public List<string> languages;
        public string language_desc;
        public List<string> traits;
        public List<string> subraces;
        public From language_options;
    }

    [System.Serializable]
    public class Rule
    {
        public string name;
        public string index;
        public string desc;
        public List<string> subsections;
    }

    [System.Serializable]
    public class RuleSection
    {
        public string name;
        public string index;
        public string desc;
    }

    [System.Serializable]
    public class Skill
    {
        public string index;
        public string name;
        public List<string> desc;
        public string ability_score;
    }

    [System.Serializable]
    public class DamageAtSlotLevel
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
    public class DamageAtCharacterLevel
    {
        public string _1;
        public string _5;
        public string _11;
        public string _17;
    }

    [System.Serializable]
    public class HealAtSlotLevel
    {
        public string _1;
        public string _2;
        public string _3;
        public string _4;
        public string _5;
        public string _6;
        public string _7;
        public string _8;
        public string _9;
    }

    [System.Serializable]
    public class SpellDamage
    {
        public string damage_type;
        public DamageAtSlotLevel damage_at_slot_level;
        public DamageAtCharacterLevel damage_at_character_level;
    }

    [System.Serializable]
    public class Dc
    {
        public string dc_type;
        public string dc_success;
        public string desc;
    }

    [System.Serializable]
    public class AreaOfEffect
    {
        public string type;
        public int size;
    }

    [System.Serializable]
    public class Spell
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
        public SpellDamage damage;
        public string school;
        public List<string> classes;
        public List<string> subclasses;
        public string url;
        public Dc dc;
        public HealAtSlotLevel heal_at_slot_level;
        public AreaOfEffect area_of_effect;
    }

    [System.Serializable]
    public class SpellPrerequisite
    {
        public string index;
        public string type;
        public string name;
    }

    [System.Serializable]
    public class SubclassSpell
    {
        public List<SpellPrerequisite> prerequisites;
        public string spell;
    }

    [System.Serializable]
    public class Subclass
    {
        public string index;
        public string parent_class;
        public string name;
        public string subclass_flavor;
        public List<string> desc;
        public string subclass_levels;
        public List<SubclassSpell> spells;
    }

    [System.Serializable]
    public class Subrace
    {
        public string index;
        public string name;
        public string parent_race;
        public string desc;
        public List<AbilityBonus> ability_bonuses;
        public List<string> starting_proficiencies;
        public List<string> languages;
        public List<string> racial_traits;
        public From language_options;
    }

    [System.Serializable]
    public class SpellOptions
    {
        public int choose;
        public string type;
        public List<string> fromSpells;
    }

    [System.Serializable]
    public class SubtraitOptions
    {
        public int choose;
        public string type;
        public List<string> fromSubtraits;
    }

    [System.Serializable]
    public class Usage
    {
        public string type;
        public int times;
    }

    [System.Serializable]
    public class BreathWeaponDamage
    {
        public string damage_type;
        public string damage_at_1;
        public string damage_at_6;
        public string damage_at_11;
        public string damage_at_16;
    }

    [System.Serializable]
    public class BreathWeapon
    {
        public string name;
        public string desc;
        public Usage usage;
        public Dc dc;
        public List<BreathWeaponDamage> damage;
    }

    [System.Serializable]
    public class TraitSpecific
    {
        public SpellOptions spell_options;
        public SubtraitOptions subtrait_options;
        public string damage_type;
        public BreathWeapon breath_weapon;
    }

    [System.Serializable]
    public class Trait
    {
        public string index;
        public List<string> races;
        public List<string> subraces;
        public string name;
        public List<string> desc;
        public List<string> proficiencies;
        public ProficiencyChoice proficiency_choices;
        public TraitSpecific trait_specific;
        public string parent;
    }

    [System.Serializable]
    public class WeaponProperties
    {
        public string index;
        public string name;
        public List<string> desc;
    }
    #endregion 

    // --------------------- DICTIONARIES ------------------------------

    public Dictionary<string, AbilityScore> dictionary_AbilityScore;
    public Dictionary<string, Alignment> dictionary_Alignment;
    public Dictionary<string, Background> dictionary_Background;
    public Dictionary<string, Class> dictionary_Class;
    public Dictionary<string, Condition> dictionary_Condition;
    public Dictionary<string, DamageType> dictionary_DamageType;
    public Dictionary<string, Equipment> dictionary_Equipment;
    public Dictionary<string, EquipmentCategory> dictionary_EquipmentCategory;
    public Dictionary<string, Feat> dictionary_Feat;
    public Dictionary<string, Feature> dictionary_Feature;
    public Dictionary<string, Languages> dictionary_Languages;
    public Dictionary<string, Level> dictionary_Level;
    public Dictionary<string, MagicItem> dictionary_MagicItem;
    public Dictionary<string, MagicSchool> dictionary_MagicSchool;
    public Dictionary<string, Proficiency> dictionary_Proficiency;
    public Dictionary<string, Race> dictionary_Race;
    public Dictionary<string, Rule> dictionary_Rule;
    public Dictionary<string, RuleSection> dictionary_RuleSection;
    public Dictionary<string, Skill> dictionary_Skill;
    public Dictionary<string, Spell> dictionary_Spell;
    public Dictionary<string, Subclass> dictionary_Subclass;
    public Dictionary<string, Subrace> dictionary_Subrace;
    public Dictionary<string, Trait> dictionary_Trait;
    public Dictionary<string, WeaponProperties> dictionary_WeaponProperties;

    private void Dictionary_initialization()
    {
        dictionary_AbilityScore = new Dictionary<string, AbilityScore>();
        dictionary_Alignment = new Dictionary<string, Alignment>();
        dictionary_Background = new Dictionary<string, Background>();
        dictionary_Class = new Dictionary<string, Class>();
        dictionary_Condition = new Dictionary<string, Condition>();
        dictionary_DamageType = new Dictionary<string, DamageType>();
        dictionary_Equipment = new Dictionary<string, Equipment>();
        dictionary_EquipmentCategory = new Dictionary<string, EquipmentCategory>();
        dictionary_Feat = new Dictionary<string, Feat>();
        dictionary_Feature = new Dictionary<string, Feature>();
        dictionary_Languages = new Dictionary<string, Languages>();
        dictionary_Level = new Dictionary<string, Level>();
        dictionary_MagicItem = new Dictionary<string, MagicItem>();
        dictionary_MagicSchool = new Dictionary<string, MagicSchool>();
        dictionary_Proficiency = new Dictionary<string, Proficiency>();
        dictionary_Race = new Dictionary<string, Race>();
        dictionary_Rule = new Dictionary<string, Rule>();
        dictionary_RuleSection = new Dictionary<string, RuleSection>();
        dictionary_Skill = new Dictionary<string, Skill>();
        dictionary_Spell = new Dictionary<string, Spell>();
        dictionary_Subclass = new Dictionary<string, Subclass>();
        dictionary_Subrace = new Dictionary<string, Subrace>();
        dictionary_Trait = new Dictionary<string, Trait>();
        dictionary_WeaponProperties = new Dictionary<string, WeaponProperties>();
    }

    private void Importer()
    {
        string fileToString;

        // Ability
        fileToString = dictAssets["Ability-Scores"].text;
        AbilityScore[] AbilityObject = JsonHelper.FromJson<AbilityScore>(fileToString);
        foreach (AbilityScore abilityScore in AbilityObject)
        {
            AddAbilityScore(abilityScore);
        }

        // Alignment
        fileToString = dictAssets["Alignments"].text; 
        Alignment[] AlignmentObject = JsonHelper.FromJson<Alignment>(fileToString);
        foreach (Alignment alignment in AlignmentObject)
        {
            AddAlignment(alignment);
        }

        // Background
        fileToString = dictAssets["Backgrounds"].text;
        Background[] BackgroundObject = JsonHelper.FromJson<Background>(fileToString);
        foreach (Background background in BackgroundObject)
        {
            AddBackground(background);
        }

        // Class
        fileToString = dictAssets["Classes"].text;  
        Class[] ClassObject = JsonHelper.FromJson<Class>(fileToString);
        foreach (Class _class in ClassObject)
        {
            AddClass(_class);
        }

        // Condition
        fileToString = dictAssets["Conditions"].text; 
        Condition[] ConditionObject = JsonHelper.FromJson<Condition>(fileToString);
        foreach (Condition condition in ConditionObject)
        {
            AddCondition(condition);
        }

        // DamageTypes
        fileToString = dictAssets["DamageTypes"].text;
        DamageType[] DamageTypeObject = JsonHelper.FromJson<DamageType>(fileToString);
        foreach (DamageType damageType in DamageTypeObject)
        {
            AddDamageType(damageType);
        }

        // Equipment
        fileToString = dictAssets["Equipment"].text;
        Equipment[] EquipmentObject = JsonHelper.FromJson<Equipment>(fileToString);
        foreach (Equipment equipment in EquipmentObject)
        {
            AddEquipment(equipment);
        }

        // Equipment
        fileToString = dictAssets["EquipmentCategories"].text; 
        EquipmentCategory[] EquipmentCategoryObject = JsonHelper.FromJson<EquipmentCategory>(fileToString);
        foreach (EquipmentCategory equipmentCategory in EquipmentCategoryObject)
        {
            AddEquipmentCategory(equipmentCategory);
        }

        // Feat
        fileToString = dictAssets["Feats"].text;
        Feat[] FeatObject = JsonHelper.FromJson<Feat>(fileToString);
        foreach (Feat feat in FeatObject)
        {
            AddFeat(feat);
        }

        // Features
        fileToString = dictAssets["Features"].text; 
        Feature[] FeatureObject = JsonHelper.FromJson<Feature>(fileToString);
        foreach (Feature feature in FeatureObject)
        {
            AddFeature(feature);
        }

        // Languages
        fileToString = dictAssets["Languages"].text;
        Languages[] LanguagesObject = JsonHelper.FromJson<Languages>(fileToString);
        foreach (Languages languages in LanguagesObject)
        {
            AddLanguages(languages);
        }

        // Levels
        fileToString = dictAssets["Levels"].text; 
        Level[] LevelObject = JsonHelper.FromJson<Level>(fileToString);
        foreach (Level level in LevelObject)
        {
            AddLevel(level);
        }

        // MagicItem
        fileToString = dictAssets["MagicItems"].text;
        MagicItem[] MagicItemObject = JsonHelper.FromJson<MagicItem>(fileToString);
        foreach (MagicItem magicItem in MagicItemObject)
        {
            AddMagicItem(magicItem);
        }

        // MagicItem
        fileToString = dictAssets["MagicSchools"].text; 
        MagicSchool[] MagicSchoolObject = JsonHelper.FromJson<MagicSchool>(fileToString);
        foreach (MagicSchool magicSchool in MagicSchoolObject)
        {
            AddMagicSchool(magicSchool);
        }

        // Proficiency
        fileToString = dictAssets["Proficiencies"].text; 
        Proficiency[] ProficiencyObject = JsonHelper.FromJson<Proficiency>(fileToString);
        foreach (Proficiency proficiency in ProficiencyObject)
        {
            AddProficiency(proficiency);
        }

        // Race
        fileToString = dictAssets["Races"].text;
        Race[] RaceObject = JsonHelper.FromJson<Race>(fileToString);
        foreach (Race race in RaceObject)
        {
            AddRace(race);
        }

        // Rules
        fileToString = dictAssets["Rules"].text;
        Rule[] RuleObject = JsonHelper.FromJson<Rule>(fileToString);
        foreach (Rule rule in RuleObject)
        {
            AddRule(rule);
        }

        // Rules Section
        fileToString = dictAssets["RuleSections"].text;
        RuleSection[] RuleSectionObject = JsonHelper.FromJson<RuleSection>(fileToString);
        foreach (RuleSection ruleSection in RuleSectionObject)
        {
            AddRuleSection(ruleSection);
        }

        // Skills
        fileToString = dictAssets["Skills"].text;
        Skill[] SkillObject = JsonHelper.FromJson<Skill>(fileToString);
        foreach (Skill skill in SkillObject)
        {
            AddSkill(skill);
        }

        // Spells
        fileToString = dictAssets["Spells"].text;
        Spell[] SpellObject = JsonHelper.FromJson<Spell>(fileToString);
        foreach (Spell spell in SpellObject)
        {
            AddSpell(spell);
        }

        // Subclass
        fileToString = dictAssets["Subclasses"].text;
        Subclass[] SubclassObject = JsonHelper.FromJson<Subclass>(fileToString);
        foreach (Subclass subclass in SubclassObject)
        {
            AddSubclass(subclass);
        }

        // Subrace
        fileToString = dictAssets["Subraces"].text;
        Subrace[] SubraceObject = JsonHelper.FromJson<Subrace>(fileToString);
        foreach (Subrace subrace in SubraceObject)
        {
            AddSubrace(subrace);
        }

        // Trait
        fileToString = dictAssets["Traits"].text;
        Trait[] TraitObject = JsonHelper.FromJson<Trait>(fileToString);
        foreach (Trait trait in TraitObject)
        {
            AddTrait(trait);
        }

        // WeaponProperties
        fileToString = dictAssets["WeaponProperties"].text;
        WeaponProperties[] weaponPropertiesObject = JsonHelper.FromJson<WeaponProperties>(fileToString);
        foreach (WeaponProperties weaponProperties in weaponPropertiesObject)
        {
            AddWeaponProperties(weaponProperties);
        }
    }

    #region Getters/Setters
    // AbilityScores
    public  AbilityScore GetAbilityScore(string index)
    {
        AbilityScore temp;
        if (dictionary_AbilityScore.TryGetValue(index, out temp)){
            return temp;
        } else {
            return null;
        }
    }

    public  void AddAbilityScore(AbilityScore item)
    {
        dictionary_AbilityScore.Add(item.index,item);
    }

    public  bool RemoveAbilityScore(string index)
    {
        return dictionary_AbilityScore.Remove(index);
    }

    // Alignment
    public  Alignment GetAlignment(string index)
    {
        Alignment temp;
        if (dictionary_Alignment.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddAlignment(Alignment item)
    {
        dictionary_Alignment.Add(item.index, item);
    }

    public  bool RemoveAlignment(string index)
    {
        return dictionary_Alignment.Remove(index);
    }

    // Background
    public  Background GetBackground(string index)
    {
        Background temp;
        if (dictionary_Background.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddBackground(Background item)
    {
        dictionary_Background.Add(item.index, item);
    }

    public  bool RemoveBackground(string index)
    {
        return dictionary_Background.Remove(index);
    }

    // Class
    public  Class GetClass(string index)
    {
        Class temp;
        if (dictionary_Class.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddClass(Class item)
    {
        dictionary_Class.Add(item.index, item);
    }

    public  bool RemoveClass(string index)
    {
        return dictionary_Class.Remove(index);
    }

    // Condition
    public  Condition GetCondition(string index)
    {
        Condition temp;
        if (dictionary_Condition.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddCondition(Condition item)
    {
        dictionary_Condition.Add(item.index, item);
    }

    public  bool RemoveCondition(string index)
    {
        return dictionary_Condition.Remove(index);
    }

    // DamageType
    public  DamageType GetDamageType(string index)
    {
        DamageType temp;
        if (dictionary_DamageType.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddDamageType(DamageType item)
    {
        dictionary_DamageType.Add(item.index, item);
    }

    public  bool RemoveDamageType(string index)
    {
        return dictionary_DamageType.Remove(index);
    }
    // Equipment
    public  Equipment GetEquipment(string index)
    {
        Equipment temp;
        if (dictionary_Equipment.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddEquipment(Equipment item)
    {
        dictionary_Equipment.Add(item.index, item);
    }

    public  bool RemoveEquipment(string index)
    {
        return dictionary_Equipment.Remove(index);
    }

    // EquipmentCategory
    public  EquipmentCategory GetEquipmentCategory(string index)
    {
        EquipmentCategory temp;
        if (dictionary_EquipmentCategory.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddEquipmentCategory(EquipmentCategory item)
    {
        dictionary_EquipmentCategory.Add(item.index, item);
    }

    public  bool RemoveEquipmentCategory(string index)
    {
        return dictionary_EquipmentCategory.Remove(index);
    }

    // Feat
    public  Feat GetFeat(string index)
    {
        Feat temp;
        if (dictionary_Feat.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddFeat(Feat item)
    {
        dictionary_Feat.Add(item.index, item);
    }

    public  bool RemoveFeat(string index)
    {
        return dictionary_Feat.Remove(index);
    }

    // Feature
    public  Feature GetFeature(string index)
    {
        Feature temp;
        if (dictionary_Feature.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddFeature(Feature item)
    {
        dictionary_Feature.Add(item.index, item);
    }

    public  bool RemoveFeature(string index)
    {
        return dictionary_Feature.Remove(index);
    }

    // Languages
    public  Languages GetLanguages(string index)
    {
        Languages temp;
        if (dictionary_Languages.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddLanguages(Languages item)
    {
        dictionary_Languages.Add(item.index, item);
    }

    public  bool RemoveLanguages(string index)
    {
        return dictionary_Languages.Remove(index);
    }

    // Level
    public  Level GetLevel(string index)
    {
        Level temp;
        if (dictionary_Level.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddLevel(Level item)
    {
        dictionary_Level.Add(item.index, item);
    }

    public  bool RemoveLevel(string index)
    {
        return dictionary_Level.Remove(index);
    }

    // MagicItem
    public  MagicItem GetMagicItem(string index)
    {
        MagicItem temp;
        if (dictionary_MagicItem.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddMagicItem(MagicItem item)
    {
        dictionary_MagicItem.Add(item.index, item);
    }

    public  bool RemoveMagicItem(string index)
    {
        return dictionary_MagicItem.Remove(index);
    }

    // MagicSchool
    public  MagicSchool GetMagicSchool(string index)
    {
        MagicSchool temp;
        if (dictionary_MagicSchool.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddMagicSchool(MagicSchool item)
    {
        dictionary_MagicSchool.Add(item.index, item);
    }

    public  bool RemoveMagicSchool(string index)
    {
        return dictionary_MagicSchool.Remove(index);
    }

    // Proficiency
    public  Proficiency GetProficiency(string index)
    {
        Proficiency temp;
        if (dictionary_Proficiency.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddProficiency(Proficiency item)
    {
        dictionary_Proficiency.Add(item.index, item);
    }

    public  bool RemoveProficiency(string index)
    {
        return dictionary_Proficiency.Remove(index);
    }

    // Race
    public  Race GetRace(string index)
    {
        Race temp;
        if (dictionary_Race.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddRace(Race item)
    {
        dictionary_Race.Add(item.index, item);
    }

    public  bool RemoveRace(string index)
    {
        return dictionary_Race.Remove(index);
    }

    // Rule
    public  Rule GetRule(string index)
    {
        Rule temp;
        if (dictionary_Rule.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddRule(Rule item)
    {
        dictionary_Rule.Add(item.index, item);
    }

    public  bool RemoveRule(string index)
    {
        return dictionary_Rule.Remove(index);
    }

    // RuleSection
    public  RuleSection GetRuleSection(string index)
    {
        RuleSection temp;
        if (dictionary_RuleSection.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddRuleSection(RuleSection item)
    {
        dictionary_RuleSection.Add(item.index, item);
    }

    public  bool RemoveRuleSection(string index)
    {
        return dictionary_RuleSection.Remove(index);
    }

    // Skill
    public  Skill GetSkill(string index)
    {
        Skill temp;
        if (dictionary_Skill.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddSkill(Skill item)
    {
        dictionary_Skill.Add(item.index, item);
    }

    public  bool RemoveSkill(string index)
    {
        return dictionary_Skill.Remove(index);
    }

    // Spell
    public  Spell GetSpell(string index)
    {
        Spell temp;
        if (dictionary_Spell.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddSpell(Spell item)
    {
        dictionary_Spell.Add(item.index, item);
    }

    public  bool RemoveSpell(string index)
    {
        return dictionary_Spell.Remove(index);
    }

    // Condition
    public  Subclass GetSubclass(string index)
    {
        Subclass temp;
        if (dictionary_Subclass.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddSubclass(Subclass item)
    {
        dictionary_Subclass.Add(item.index, item);
    }

    public  bool RemoveSubclass(string index)
    {
        return dictionary_Subclass.Remove(index);
    }

    // Subrace
    public  Subrace GetSubrace(string index)
    {
        Subrace temp;
        if (dictionary_Subrace.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddSubrace(Subrace item)
    {
        dictionary_Subrace.Add(item.index, item);
    }

    public  bool RemoveSubrace(string index)
    {
        return dictionary_Subrace.Remove(index);
    }

    // Condition
    public  Trait GetTrait(string index)
    {
        Trait temp;
        if (dictionary_Trait.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddTrait(Trait item)
    {
        dictionary_Trait.Add(item.index, item);
    }

    public  bool RemoveTrait(string index)
    {
        return dictionary_Trait.Remove(index);
    }

    // Condition
    public  WeaponProperties GetWeaponProperties(string index)
    {
        WeaponProperties temp;
        if (dictionary_WeaponProperties.TryGetValue(index, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    public  void AddWeaponProperties(WeaponProperties item)
    {
        dictionary_WeaponProperties.Add(item.index, item);
    }

    public  bool RemoveWeaponProperties(string index)
    {
        return dictionary_WeaponProperties.Remove(index);
    }
    #endregion
}

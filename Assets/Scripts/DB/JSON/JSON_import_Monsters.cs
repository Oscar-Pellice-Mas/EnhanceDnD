using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JSON_import_Monsters : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_Speed
    {
        public string walk;
        public string swim;
        public string fly;
        public string burrow;
        public string climb;
        public bool? hover;
    }

    [System.Serializable]
    public class JS_Proficiency2
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Proficiency
    {
        public int value;
        public JS_Proficiency2 proficiency;
    }

    [System.Serializable]
    public class JS_ConditionImmunity
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Senses
    {
        public string darkvision;
        public int passive_perception;
        public string blindsight;
        public string truesight;
        public string tremorsense;
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
        public int dc_value;
        public string success_type;
    }

    [System.Serializable]
    public class JS_Ability
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Slots
    {
        public int _1;
        public int? _2;
        public int? _3;
        public int? _4;
        public int? _5;
        public int? _6;
        public int? _7;
        public int? _8;
        public int? _9;
    }

    [System.Serializable]
    public class JS_Usage
    {
        public string type;
        public int? times;
        public List<string> rest_types;
        public string dice;
        public int? min_value;
    }

    [System.Serializable]
    public class JS_Spell
    {
        public string name;
        public int level;
        public string url;
        public JS_Usage usage;
        public string notes;
    }

    [System.Serializable]
    public class JS_Spellcasting
    {
        public int level;
        public JS_Ability ability;
        public int dc;
        public int modifier;
        public List<string> components_required;
        public string school;
        public JS_Slots slots;
        public List<JS_Spell> spells;
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
        public JS_DamageType damage_type;
        public string damage_dice;
        public JS_Dc dc;
        public int? choose;
        public string type;
        public List<JS_From> from;
    }

    [System.Serializable]
    public class JS_SpecialAbility
    {
        public string name;
        public string desc;
        public JS_Dc dc;
        public JS_Spellcasting spellcasting;
        public JS_Usage usage;
        public List<JS_Damage> damage;
        public int? attack_bonus;
    }

    [System.Serializable]
    public class JS_MultiOptions
    {
        public string name;
        public int count;
        public string type;
    }

    [System.Serializable]
    public class JS_Options
    {
        public int choose;
        public List<List<JS_MultiOptions>> from;
    }

    [System.Serializable]
    public class JS_From
    {
        public JS_DamageType damage_type;
        public string damage_dice;
        public string name;
        public JS_Dc dc;
        public List<JS_Damage> damage;
    }

    [System.Serializable]
    public class JS_AttackOptions
    {
        public int choose;
        public string type;
        public List<JS_From> from;
    }

    [System.Serializable]
    public class JS_Attack
    {
        public string name;
        public JS_Dc dc;
        public List<JS_Damage> damage;
    }

    [System.Serializable]
    public class JS_Action
    {
        public string name;
        public string desc;
        public JS_Options options;
        public int? attack_bonus;
        public JS_Dc dc;
        public List<JS_Damage> damage;
        public JS_Usage usage;
        public JS_AttackOptions attack_options;
        public List<JS_Attack> attacks;
        public string damage_dice;
    }

    [System.Serializable]
    public class JS_LegendaryAction
    {
        public string name;
        public string desc;
        public int? attack_bonus;
        public List<JS_Damage> damage;
        public JS_Dc dc;
    }

    [System.Serializable]
    public class JS_Reaction
    {
        public string name;
        public string desc;
        public JS_Dc dc;
        public int? attack_bonus;
    }

    [System.Serializable]
    public class JS_Form
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
        public string size;
        public string type;
        public string subtype;
        public string alignment;
        public int armor_class;
        public int hit_points;
        public string hit_dice;
        public JS_Speed speed;
        public int strength;
        public int dexterity;
        public int constitution;
        public int intelligence;
        public int wisdom;
        public int charisma;
        public List<JS_Proficiency> proficiencies;
        public List<string> damage_vulnerabilities;
        public List<string> damage_resistances;
        public List<string> damage_immunities;
        public List<JS_ConditionImmunity> condition_immunities;
        public JS_Senses senses;
        public string languages;
        public double challenge_rating;
        public int xp;
        public List<JS_SpecialAbility> special_abilities;
        public List<JS_Action> actions;
        public List<JS_LegendaryAction> legendary_actions;
        public string url;
        public List<JS_Reaction> reactions;
        public List<JS_Form> forms;
    }



    // Definitive -------------------------------------------------
    [System.Serializable]
    public class Speed
    {
        public string walk;
        public string swim;
        public string fly;
        public string burrow;
        public string climb;
        public bool? hover;
    }

    [System.Serializable]
    public class ProficiencyValue
    {
        public int value;
        public string proficiency;
    }

    [System.Serializable]
    public class Senses
    {
        public string darkvision;
        public int passive_perception;
        public string blindsight;
        public string truesight;
        public string tremorsense;
    }

    [System.Serializable]
    public class Dc
    {
        public string dc_type;
        public int dc_value;
        public string success_type;
    }

    [System.Serializable]
    public class Slots
    {
        public int _1;
        public int? _2;
        public int? _3;
        public int? _4;
        public int? _5;
        public int? _6;
        public int? _7;
        public int? _8;
        public int? _9;
    }

    [System.Serializable]
    public class Usage
    {
        public string type;
        public int? times;
        public List<string> rest_types;
        public string dice;
        public int? min_value;
    }

    [System.Serializable]
    public class Spell
    {
        public string name;
        public int level;
        public JS_Usage usage;
        public string notes;
    }

    [System.Serializable]
    public class Spellcasting
    {
        public int level;
        public string ability;
        public int dc;
        public int modifier;
        public List<string> components_required;
        public string school;
        public Slots slots;
        public List<Spell> spells;
    }

    [System.Serializable]
    public class Damage
    {
        public string damage_type;
        public string damage_dice;
        public Dc dc;
        public int? choose;
        public string type;
        public List<string> from;
    }

    [System.Serializable]
    public class SpecialAbility
    {
        public string name;
        public string desc;
        public Dc dc;
        public Spellcasting spellcasting;
        public Usage usage;
        public List<Damage> damage;
        public int? attack_bonus;
    }

    [System.Serializable]
    public class MultiOptions
    {
        public string name;
        public int count;
        public string type;
    }

    [System.Serializable]
    public class Options
    {
        public int choose;
        public List<List<MultiOptions>> from;
    }

    [System.Serializable]
    public class From
    {
        public string damage_type;
        public string damage_dice;
        public string name;
        public Dc dc;
        public List<Damage> damage;
    }

    [System.Serializable]
    public class AttackOptions
    {
        public int choose;
        public string type;
        public List<From> from;
    }

    [System.Serializable]
    public class Attack
    {
        public string name;
        public Dc dc;
        public List<Damage> damage;
    }

    [System.Serializable]
    public class Action
    {
        public string name;
        public string desc;
        public Options options;
        public int? attack_bonus;
        public Dc dc;
        public List<Damage> damage;
        public Usage usage;
        public AttackOptions attack_options;
        public List<Attack> attacks;
        public string damage_dice;
    }

    [System.Serializable]
    public class LegendaryAction
    {
        public string name;
        public string desc;
        public int? attack_bonus;
        public List<Damage> damage;
        public Dc dc;
    }

    [System.Serializable]
    public class Reaction
    {
        public string name;
        public string desc;
        public Dc dc;
        public int? attack_bonus;
    }

    [System.Serializable]
    public class Monster
    {
        public string index;
        public string name;
        public string size;
        public string type;
        public string subtype;
        public string alignment;
        public int armor_class;
        public int hit_points;
        public string hit_dice;
        public Speed speed;
        public int strength;
        public int dexterity;
        public int constitution;
        public int intelligence;
        public int wisdom;
        public int charisma;
        public List<ProficiencyValue> proficiencies;
        public List<string> damage_vulnerabilities;
        public List<string> damage_resistances;
        public List<string> damage_immunities;
        public List<string> condition_immunities;
        public Senses senses;
        public string languages;
        public double challenge_rating;
        public int xp;
        public List<SpecialAbility> special_abilities;
        public List<Action> actions;
        public List<LegendaryAction> legendary_actions;
        public string url;
        public List<Reaction> reactions;
        public List<string> forms;
    }



    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Monsters.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        Monster[] monsters = new Monster[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            Monster aux = new Monster();

            //public string index;
            aux.index = jsonObject[i].index;
            //public string name;
            aux.name = jsonObject[i].name;
            //public string size;
            aux.size = jsonObject[i].size;
            //public string type;
            aux.type = jsonObject[i].type;
            //public string subtype;
            aux.subtype = jsonObject[i].subtype;
            //public string alignment;
            aux.alignment = jsonObject[i].alignment;
            //public int armor_class;
            aux.armor_class = jsonObject[i].armor_class;
            //public int hit_points;
            aux.hit_points = jsonObject[i].hit_points;
            //public string hit_dice;
            aux.hit_dice = jsonObject[i].hit_dice;
            //public Speed speed;
            aux.speed = new Speed();
            aux.speed.burrow = jsonObject[i].speed.burrow;
            aux.speed.climb = jsonObject[i].speed.climb;
            aux.speed.fly = jsonObject[i].speed.fly;
            aux.speed.hover = jsonObject[i].speed.hover;
            aux.speed.swim = jsonObject[i].speed.swim;
            aux.speed.walk = jsonObject[i].speed.walk;
            //public int strength;
            aux.strength = jsonObject[i].strength;
            //public int dexterity;
            aux.dexterity = jsonObject[i].dexterity;
            //public int constitution;
            aux.constitution = jsonObject[i].constitution;
            //public int intelligence;
            aux.intelligence = jsonObject[i].intelligence;
            //public int wisdom;
            aux.wisdom = jsonObject[i].wisdom;
            //public int charisma;
            aux.charisma = jsonObject[i].charisma;
            //public List<ProficiencyValue> proficiencies;
            aux.proficiencies = new List<ProficiencyValue>();
            foreach (JS_Proficiency prof in jsonObject[i].proficiencies)
            {
                ProficiencyValue auxiliar = new ProficiencyValue();
                auxiliar.proficiency = prof.proficiency.index;
                auxiliar.value = prof.value;
                aux.proficiencies.Add(auxiliar);
            }
            //public List<string> damage_vulnerabilities;
            aux.damage_vulnerabilities = (jsonObject[i].damage_vulnerabilities).ToList();
            //public List<string> damage_resistances;
            aux.damage_resistances = (jsonObject[i].damage_resistances).ToList();
            //public List<string> damage_immunities;
            aux.damage_immunities = (jsonObject[i].damage_immunities).ToList();
            //public List<string> condition_immunities;
            aux.condition_immunities = (jsonObject[i].condition_immunities.Select(conditionImmunity => conditionImmunity.index)).ToList();
            //public Senses senses;
            aux.senses = new Senses();
            aux.senses.blindsight = jsonObject[i].senses.blindsight;
            aux.senses.darkvision = jsonObject[i].senses.darkvision;
            aux.senses.passive_perception = jsonObject[i].senses.passive_perception;
            aux.senses.tremorsense = jsonObject[i].senses.tremorsense;
            aux.senses.truesight = jsonObject[i].senses.truesight;
            //public string languages;
            aux.languages = jsonObject[i].languages;
            //public double challenge_rating;
            aux.challenge_rating = jsonObject[i].challenge_rating;
            //public int xp;
            aux.xp = jsonObject[i].xp;
            //public List<SpecialAbility> special_abilities;
            aux.special_abilities = new List<SpecialAbility>();
            foreach (JS_SpecialAbility specialAbility in jsonObject[i].special_abilities)
            {
                SpecialAbility auxiliar = new SpecialAbility();
                auxiliar.attack_bonus = specialAbility.attack_bonus;
                auxiliar.damage = new List<Damage>();
                foreach (JS_Damage damage in specialAbility.damage)
                {
                    Damage auxiliarDamage = new Damage();
                    auxiliarDamage.choose = damage.choose;
                    auxiliarDamage.damage_dice = damage.damage_dice;
                    auxiliarDamage.damage_type = damage.damage_type.index;
                    auxiliarDamage.dc = new Dc();
                    if (damage.dc.dc_type != null)
                        auxiliarDamage.dc.dc_type = damage.dc.dc_type.index;
                    auxiliarDamage.dc.dc_value = damage.dc.dc_value;
                    auxiliarDamage.dc.success_type = damage.dc.success_type;
                    auxiliarDamage.from = new List<string>();
                    foreach (JS_From from2 in damage.from)
                    {
                        auxiliarDamage.from.Add(from2.name);
                    }
                    auxiliarDamage.type = damage.type;
                    auxiliar.damage.Add(auxiliarDamage);
                } //Loop --------------------------
                if (specialAbility.dc != null)
                {
                    auxiliar.dc = new Dc();
                    if (specialAbility.dc.dc_type != null)
                        auxiliar.dc.dc_type = specialAbility.dc.dc_type.index;
                    auxiliar.dc.dc_value = specialAbility.dc.dc_value;
                    auxiliar.dc.success_type = specialAbility.dc.success_type;
                }
                
                auxiliar.desc = specialAbility.desc;
                auxiliar.name = specialAbility.name;
                auxiliar.spellcasting = new Spellcasting();
                if (specialAbility.spellcasting.ability != null)
                    auxiliar.spellcasting.ability = specialAbility.spellcasting.ability.index;
                auxiliar.spellcasting.components_required = specialAbility.spellcasting.components_required;
                auxiliar.spellcasting.dc = specialAbility.spellcasting.dc;
                auxiliar.spellcasting.level = specialAbility.spellcasting.level;
                auxiliar.spellcasting.modifier = specialAbility.spellcasting.modifier;
                auxiliar.spellcasting.school = specialAbility.spellcasting.school;
                if (specialAbility.spellcasting != null)
                {
                    auxiliar.spellcasting.slots = new Slots();
                    if (specialAbility.spellcasting.slots != null)
                    {
                        auxiliar.spellcasting.slots._1 = specialAbility.spellcasting.slots._1;
                        auxiliar.spellcasting.slots._2 = specialAbility.spellcasting.slots._2;
                        auxiliar.spellcasting.slots._3 = specialAbility.spellcasting.slots._3;
                        auxiliar.spellcasting.slots._4 = specialAbility.spellcasting.slots._4;
                        auxiliar.spellcasting.slots._5 = specialAbility.spellcasting.slots._5;
                        auxiliar.spellcasting.slots._6 = specialAbility.spellcasting.slots._6;
                        auxiliar.spellcasting.slots._7 = specialAbility.spellcasting.slots._7;
                        auxiliar.spellcasting.slots._8 = specialAbility.spellcasting.slots._8;
                        auxiliar.spellcasting.slots._9 = specialAbility.spellcasting.slots._9;
                    }

                    if (specialAbility.spellcasting.spells != null)
                    {
                        auxiliar.spellcasting.spells = new List<Spell>();
                        foreach (JS_Spell spell in specialAbility.spellcasting.spells)
                        {
                            Spell auxiliarSpell = new Spell();
                            auxiliarSpell.level = spell.level;
                            auxiliarSpell.name = spell.name;
                            auxiliarSpell.notes = spell.notes;
                            auxiliarSpell.usage = spell.usage;
                            auxiliar.spellcasting.spells.Add(auxiliarSpell);
                        }
                    }
                    
                }
                
                auxiliar.usage = new Usage();
                auxiliar.usage.dice = specialAbility.usage.dice;
                auxiliar.usage.min_value = specialAbility.usage.min_value;
                auxiliar.usage.rest_types = specialAbility.usage.rest_types;
                auxiliar.usage.times = specialAbility.usage.times;
                auxiliar.usage.type = specialAbility.usage.type;
                aux.special_abilities.Add(auxiliar);
            }
            //public List<Action> actions;
            aux.actions = new List<Action>();
            foreach (JS_Action action in jsonObject[i].actions)
            {
                Action auxiliar = new Action();
                auxiliar.attacks = new List<Attack>();
                foreach (JS_Attack attack in action.attacks)
                {
                    Attack auxiliarAttack = new Attack();
                    auxiliarAttack.damage = new List<Damage>();
                    foreach (JS_Damage damage in attack.damage)
                    {
                        Damage auxiliarDamage = new Damage();
                        auxiliarDamage.choose = damage.choose;
                        auxiliarDamage.damage_dice = damage.damage_dice;
                        auxiliarDamage.damage_type = damage.damage_type.index;
                        auxiliarDamage.dc = new Dc();
                        if (damage.dc.dc_type != null)
                            auxiliarDamage.dc.dc_type = damage.dc.dc_type.index;
                        auxiliarDamage.dc.dc_value = damage.dc.dc_value;
                        auxiliarDamage.dc.success_type = damage.dc.success_type;
                        auxiliarDamage.from = new List<string>();
                        foreach (JS_From from2 in damage.from)
                        {
                            auxiliarDamage.from.Add(from2.name);
                        }
                        auxiliarDamage.type = damage.type;
                        auxiliarAttack.damage.Add(auxiliarDamage);
                    } //Loop --------------------------
                    auxiliarAttack.dc = new Dc();
                    auxiliarAttack.dc.dc_type = attack.dc.dc_type.index;
                    auxiliarAttack.dc.dc_value = attack.dc.dc_value;
                    auxiliarAttack.dc.success_type = attack.dc.success_type;
                    auxiliarAttack.name = attack.name;
                }
                auxiliar.attack_bonus = action.attack_bonus;
                auxiliar.attack_options = new AttackOptions();
                auxiliar.attack_options.choose = action.attack_options.choose;
                auxiliar.attack_options.type = action.attack_options.type;

                if (action.attack_options.from != null)
                {
                    auxiliar.attack_options.from = new List<From>();
                    foreach (JS_From from in action.attack_options.from)
                    {
                        From auxiliarFrom = new From();
                        auxiliarFrom.damage = new List<Damage>();
                        foreach (JS_Damage damage in from.damage)
                        {
                            Damage auxiliarDamage = new Damage();
                            auxiliarDamage.choose = damage.choose;
                            auxiliarDamage.damage_dice = damage.damage_dice;
                            auxiliarDamage.damage_type = damage.damage_type.index;
                            auxiliarDamage.dc = new Dc();
                            if (damage.dc.dc_type != null)
                                auxiliarDamage.dc.dc_type = damage.dc.dc_type.index;
                            auxiliarDamage.dc.dc_value = damage.dc.dc_value;
                            auxiliarDamage.dc.success_type = damage.dc.success_type;
                            auxiliarDamage.from = new List<string>();
                            foreach (JS_From from2 in damage.from)
                            {
                                auxiliarDamage.from.Add(from2.name);
                            }
                            auxiliarDamage.type = damage.type;
                            auxiliarFrom.damage.Add(auxiliarDamage);
                        } //Loop --------------------------
                        auxiliarFrom.damage_dice = from.damage_dice;
                        auxiliarFrom.damage_type = from.damage_type.index;
                        auxiliarFrom.dc = new Dc();
                        auxiliarFrom.dc.dc_type = from.dc.dc_type.index;
                        auxiliarFrom.dc.dc_value = from.dc.dc_value;
                        auxiliarFrom.dc.success_type = from.dc.success_type;
                        auxiliarFrom.name = from.name;
                    }
                }
                

                auxiliar.damage = new List<Damage>();
                foreach (JS_Damage damage in action.damage)
                {
                    Damage auxiliarDamage = new Damage();
                    auxiliarDamage.choose = damage.choose;
                    auxiliarDamage.damage_dice = damage.damage_dice;
                    auxiliarDamage.damage_type = damage.damage_type.index;
                    auxiliarDamage.dc = new Dc();
                    if(damage.dc.dc_type != null)
                        auxiliarDamage.dc.dc_type = damage.dc.dc_type.index;
                    auxiliarDamage.dc.dc_value = damage.dc.dc_value;
                    auxiliarDamage.dc.success_type = damage.dc.success_type;
                    auxiliarDamage.from = new List<string>();
                    foreach (JS_From from2 in damage.from)
                    {
                        auxiliarDamage.from.Add(from2.name);
                    }
                    auxiliarDamage.type = damage.type;
                    auxiliar.damage.Add(auxiliarDamage);
                } //Loop --------------------------
                auxiliar.damage_dice = action.damage_dice;
                auxiliar.dc = new Dc();
                if(action.dc.dc_type != null)
                    auxiliar.dc.dc_type = action.dc.dc_type.index;
                auxiliar.dc.dc_value = action.dc.dc_value;
                auxiliar.dc.success_type = action.dc.success_type;
                auxiliar.desc = action.desc;
                auxiliar.name = action.name;
                auxiliar.options = new Options();
                auxiliar.options.choose = action.options.choose;
                if (action.options.from != null)
                {
                    auxiliar.options.from = new List<List<MultiOptions>>();
                    foreach (List<JS_MultiOptions> multiOptions in action.options.from)
                    {
                        List<MultiOptions> auxiliarMulti = new List<MultiOptions>();
                        foreach (JS_MultiOptions multi in multiOptions)
                        {
                            MultiOptions auxiliarOption = new MultiOptions();
                            auxiliarOption.count = multi.count;
                            auxiliarOption.name = multi.name;
                            auxiliarOption.type = multi.type;
                            auxiliarMulti.Add(auxiliarOption);
                        }
                        auxiliar.options.from.Add(auxiliarMulti);
                    }
                }
                
                auxiliar.usage = new Usage();
                auxiliar.usage.dice = action.usage.dice;
                auxiliar.usage.min_value = action.usage.min_value;
                auxiliar.usage.rest_types = action.usage.rest_types;
                auxiliar.usage.times = action.usage.times;
                auxiliar.usage.type = action.usage.type;

                aux.actions.Add(auxiliar);
            }
            //public List<LegendaryAction> legendary_actions;
            aux.legendary_actions = new List<LegendaryAction>();
            foreach (JS_LegendaryAction legendaryAction in jsonObject[i].legendary_actions)
            {
                LegendaryAction auxiliar = new LegendaryAction();
                auxiliar.attack_bonus = legendaryAction.attack_bonus;
                auxiliar.damage = new List<Damage>();
                foreach (JS_Damage damage in legendaryAction.damage)
                {
                    Damage auxiliarDamage = new Damage();
                    auxiliarDamage.choose = damage.choose;
                    auxiliarDamage.damage_dice = damage.damage_dice;
                    auxiliarDamage.damage_type = damage.damage_type.index;
                    auxiliarDamage.dc = new Dc();
                    if (damage.dc.dc_type != null)
                        auxiliarDamage.dc.dc_type = damage.dc.dc_type.index;
                    auxiliarDamage.dc.dc_value = damage.dc.dc_value;
                    auxiliarDamage.dc.success_type = damage.dc.success_type;
                    auxiliarDamage.from = new List<string>();
                    foreach (JS_From from in damage.from)
                    {
                        auxiliarDamage.from.Add(from.name);
                    }
                    auxiliarDamage.type = damage.type;
                    auxiliar.damage.Add(auxiliarDamage);
                }
                auxiliar.dc = new Dc();
                if(legendaryAction.dc.dc_type != null)
                    auxiliar.dc.dc_type = legendaryAction.dc.dc_type.index;
                auxiliar.dc.dc_value = legendaryAction.dc.dc_value;
                auxiliar.dc.success_type = legendaryAction.dc.success_type;
                auxiliar.desc = legendaryAction.desc;
                auxiliar.name = legendaryAction.name;

                aux.legendary_actions.Add(auxiliar);
            }
            //public List<Reaction> reactions;
            aux.reactions = new List<Reaction>();
            foreach (JS_Reaction reaction in jsonObject[i].reactions)
            {
                Reaction auxiliar = new Reaction();
                auxiliar.attack_bonus = reaction.attack_bonus;
                auxiliar.dc = new Dc();
                if(reaction.dc.dc_type != null)
                    auxiliar.dc.dc_type = reaction.dc.dc_type.index; 
                auxiliar.dc.dc_value = reaction.dc.dc_value;
                auxiliar.dc.success_type = reaction.dc.success_type;
                auxiliar.desc = reaction.desc;
                auxiliar.name = reaction.name;

                aux.reactions.Add(auxiliar);
            }
            //public List<string> forms;
            aux.forms = (jsonObject[i].forms.Select(form => form.index)).ToList();

            monsters[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(monsters, true);
        string outputPath = Application.dataPath + "/Files/Definitive/Monsters.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}

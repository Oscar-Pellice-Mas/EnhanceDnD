using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public static PlayerCharacter Instance;

    public BasicPC basicPC;

    private void Awake()
    {
        basicPC = new BasicPC();
        basicPC.RolledHP = 10;
        basicPC.Race = "Something";
        SetArmorClass(10);
    }

    public PlayerCharacter()
    {
    }

    public void GenerateSheet()
    {
        SetupAbilityScores(basicPC.Scores);
        SetupSkills();

        classes = basicPC.Classes;
        GenerateClass();

        race = basicPC.Race;
        GenerateRace();

        
    }

    private void GenerateClass()
    {
        foreach (KeyValuePair<string,int> item in classes)
        {
            DB.Class itemClass = DB.Instance.GetClass(item.Key);

            List<string> proficiencies = itemClass.proficiencies;
            foreach (string proficiency in proficiencies)
            {
                AddAnyProficiency(proficiency);
            }
            
            List<string> savings = itemClass.saving_throws;
            foreach (string saving in savings)
            {
                AddAnyProficiency("saving-throw-" + saving);
            }

            List<DB.StartingEquipment> starting_equipments = itemClass.starting_equipment;
            foreach (DB.StartingEquipment starting_equipment in starting_equipments)
            {
                AddEquipment(starting_equipment.equipment, starting_equipment.quantity);
            }
        }
    }

    private void GenerateRace()
    {
        DB.Race raceItem = DB.Instance.GetRace(race);

        List<string> languages = raceItem.languages;
        foreach (string lang in languages)
        {
            AddProficiencyLanguages(lang);
        }

        SetSpeed("walk",raceItem.speed);
        
        List<string> start_profs = raceItem.starting_proficiencies;
        foreach (string prof in start_profs)
        {
            AddAnyProficiency(prof);
        }

        List<string> traits = raceItem.traits;
        foreach (string trait in traits)
        {
            AddTrait(trait);
        }
    }

    private void AddAnyProficiency(string proficiency)
    {
        Debug.Log(proficiency);
        DB.Proficiency auxiliar = DB.Instance.GetProficiency(proficiency);
        switch (auxiliar.type)
        {
            case "Armor":
                AddProficiencyArmor(auxiliar.index);
                break;
            case "Weapons":
                AddProficiencyWeapons(auxiliar.index);
                break;
            case "Artisan's Tools":
                AddProficiencyTools(auxiliar.index);
                break;
            case "Saving Throws":
                SetSavingThrowProf(auxiliar.index, true);
                break;
            case "Skills":
                SetSkillProf(auxiliar.index, true);
                break;
            default:
                AddProficiencyOthers(auxiliar.index);
                break;
        }
    }


    // Region: ABILITY SCORES
    // Contains definition and implementation of AbilityScores, SavingThrow and Senses
    #region abilityScores

    /// <summary>
    /// Class defines the AbilityScores (str, dex, con, int, wis, cha)
    /// </summary>
    private class AbilityScore
    {
        private int score;
        private bool savingProficiency;

        public AbilityScore(int score)
        {
            this.score = score;
            this.savingProficiency = false;
        }

        public int GetScore()
        {
            return score;
        }

        public bool GetSavingProf()
        {
            return savingProficiency;
        }

        public void SetScore(int score)
        {
            this.score = score;
        }

        public void SetProficiency(bool prof)
        {
            this.savingProficiency = prof;
        }
    }

    /// <summary>
    /// Dictionary containing all the score values
    /// </summary>
    private Dictionary<string, AbilityScore> abilityScores = new Dictionary<string, AbilityScore>();

    /// <summary>
    /// List of player senses not including the pasives
    /// </summary>
    private List<string> senses = new List<string>();

    /// <summary>
    /// Sets all given scores in the player AbilityScores
    /// </summary>
    /// <param name="scores">Dictionary of wanted scores</param>
    public void SetupAbilityScores(Dictionary<string, int> scores)
    {
        int value = 0;
        foreach (DB.AbilityScore abilityScore in DB.Instance.dictionary_AbilityScore.Values)
        {
            if (abilityScores.ContainsKey(abilityScore.index))
            {
                abilityScores[abilityScore.index].SetScore(scores[abilityScore.index]);
            } else
            {
                if (scores.ContainsKey(abilityScore.index))
                {
                    scores.TryGetValue(abilityScore.index, out value);
                    abilityScores.Add(abilityScore.index, new AbilityScore(value));
                } else
                {
                    continue;
                }
            }
        }
    }
    
    /// <summary>
    /// Sets the score to the given value
    /// </summary>
    /// <param name="name">Name of the score</param>
    /// <param name="value">New value for the score</param>
    /// <returns>The set was done succesfully</returns>
    public bool SetAbilityScore(string name, int value)
    {
        if (abilityScores.ContainsKey(name)) 
        { 
            abilityScores[name].SetScore(value);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the proficiency of the player in a SavingThrow
    /// </summary>
    /// <param name="name">Name of the SavingThrow</param>
    /// <param name="value">Value of the proficiency</param>
    /// <returns>The set was done succesfully</returns>
    public bool SetSavingThrowProf(string name, bool value)
    {
        if (abilityScores.ContainsKey(name))
        {
            abilityScores[name.Substring(Math.Max(0, name.Length - 3))].SetProficiency(value);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Gets the AbilityScore that matches the given name
    /// </summary>
    /// <param name="scoreName">Name of the AbilityScore</param>
    /// <returns>Returns the value or -1 on failure</returns>
    public int GetAbilityScore(string scoreName)
    {
        if (abilityScores.ContainsKey(scoreName))
        {
            abilityScores.TryGetValue(scoreName, out AbilityScore ability);
            return ability.GetScore();
        } else
        {
            return -1;
        }
    }

    /// <summary>
    /// Gets the AbilityScore modifier that matches the given name
    /// </summary>
    /// <param name="scoreName">Name of the AbilityScore</param>
    /// <returns>Rreturns modifier or -1 or failure</returns>
    public int GetAbilityModifier(string scoreName)
    {
        int score = GetAbilityScore(scoreName);
        if (score < 0) return -1;
        return (score - 10) / 2;
    }
    
    /// <summary>
    /// Gets the SavingThrow value that matches the given AbilityScore
    /// </summary>
    /// <param name="savingName">Name of the AbilityScore</param>
    /// <returns>Returns the SavingThrow or .1 on failure</returns>
    public int GetSavingThrow(string savingName)
    {
        if (abilityScores.ContainsKey(savingName))
        {
            abilityScores.TryGetValue(savingName, out AbilityScore abilityScore);
            int value = (abilityScore.GetScore() - 10) / 2;
            if (abilityScore.GetSavingProf())
            {
                value += proficiencyBonus;
            }
            return value;
        }
        return -1;
    }

    /// <summary>
    /// Gets the desired passive Sense
    /// </summary>
    /// <param name="senseName">Name of the Skil</param>
    /// <returns>Returns the passive sense or -1 on failure</returns>
    public int GetSense(string senseName)
    {
        int value = GetSkillModifier(senseName) + 10;
        if (value > 0) return value;
        return -1;
    }

    /// <summary>
    /// Gets all the non-passive player Senses
    /// </summary>
    /// <returns>Returns a list of Senses</returns>
    public List<string> GetSenses()
    {
        return senses;
    }

    /// <summary>
    /// Adds a Sense
    /// </summary>
    /// <param name="sense">Name of the Sense</param>
    public bool AddSense(string sense)
    {
        if (senses != null & !senses.Contains(sense))
        {
            senses.Add(sense);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Remove a Sense
    /// </summary>
    /// <param name="sense">Name of the Sense</param>
    public bool RemoveSense(string sense)
    {
        if (senses != null & !senses.Contains(sense))
        {
            senses.Remove(sense);
            return true;
        }
        return false;
    }

    #endregion abilityScores

    // Region: SKILLS
    // Contains definition and implementation of Skills
    #region skills

    /// <summary>
    /// Class defines the player Skills
    /// </summary>
    private class Skill
    {
        private bool proficency = false;
        private string ability;

        public Skill(string ability)
        {
            this.ability = ability;
        }

        public bool GetProficiency()
        {
            return proficency;
        }

        public string GetAbility()
        {
            return ability;
        }

        public void SetProfiency(bool proficency)
        {
            this.proficency = proficency;
        }
    }

    /// <summary>
    /// Skills of the player
    /// </summary>
    private Dictionary<string, Skill> skills = new Dictionary<string, Skill>();

    /// <summary>
    /// Sets all the skills of the player
    /// </summary>
    public void SetupSkills()
    {
        foreach (DB.Skill skill in DB.Instance.dictionary_Skill.Values) {
            skills.Add(skill.index, new Skill(skill.ability_score));
        }
    }

    /// <summary>
    /// Gets the Skill modifier of a given Skill
    /// </summary>
    /// <param name="name">Name of the Skill</param>
    /// <returns>Returns the skill modifier or -1 on failure</returns>
    public int GetSkillModifier(string name)
    {
        int value = 0;
        if (skills.ContainsKey(name))
        {
            skills.TryGetValue(name, out Skill skill);
            if (skill.GetProficiency()) value += GetProficiencyBonus();
            value += GetAbilityModifier(skill.GetAbility());
            return value;
        }
        return -1;
    }

    /// <summary>
    /// Gets the Ability of a given Skill
    /// </summary>
    /// <param name="name">Name of the Ability</param>
    /// <returns>Returns the Ability of the Skill</returns>
    public string GetSkillAbility(string name)
    {
        if (skills.ContainsKey(name))
        {
            skills.TryGetValue(name, out Skill value);
            return value.GetAbility();
        }
        return null;
    }

    /// <summary>
    /// Gets the Skill proficiency
    /// </summary>
    /// <param name="name">Name of the skill</param>
    /// <returns>Returns the proficiency</returns>
    public bool GetSkillProf(string name)
    {
        if (skills.ContainsKey(name))
        {
            skills.TryGetValue(name, out Skill skill);
            return skill.GetProficiency();
        }
        return false;
    }

    /// <summary>
    /// Sets the Skill proficiency
    /// </summary>
    /// <param name="name">Name of the skill</param>
    /// <param name="value">Value of the proficiency</param>
    public bool SetSkillProf(string name, bool value)
    {
        if (skills.ContainsKey(name))
        {
            skills.TryGetValue (name, out Skill skill);
            skill.SetProfiency(value);
            return true;
        }
        return false;
    }

    // Skill info getter on DB

    #endregion skills

    // ------------- HP and Basic Values --------------------
    #region basic

    private int initiative;
    private int armorClass;
    private int hitPoints;
    private int maxPoints;
    private int tempPoints;

    public int GetInitiative()
    {
        return initiative;
    }

    public void SetInitiative(int value)
    {
        initiative = value;
    }

    public int GetArmorClass()
    {
        return armorClass;
    }

    public void SetArmorClass(int value)
    {
        armorClass = value;
    }

    public int GetHitpoints()
    {
        return hitPoints;
    }

    public void SetHitpoints(int value)
    {
        hitPoints = value;
    }

    public int GetMaxHitpoints()
    {
        return maxPoints;
    }

    public void SetMaxHitpoints(int value)
    {
        maxPoints = value;
    }

    public int GetTempHitpoints()
    {
        return tempPoints;
    }

    public void SetTempHitpoints(int value)
    {
        tempPoints = value;
    }

    public void AddTempHitpoints(int value)
    {
        tempPoints += value;
    }

    public void RemoveTempHitpoints(int value)
    {
        tempPoints -= value;
    }

    public void DeleteTempHitpoints()
    {
        tempPoints = 0;
    }

    public void HP_heal(int amount)
    {
        hitPoints = Math.Min(hitPoints + amount, maxPoints);
    }

    public void HP_damage(int amount)
    {
        int damageLeft = amount;
        if (tempPoints > 0)
        {
            damageLeft -= tempPoints;
            tempPoints = Math.Max(0, tempPoints - amount);
        }

        if (damageLeft > 0)
        {
            hitPoints = Math.Max(0, hitPoints - damageLeft);
        }
    }

    #endregion basic

    // ------------ RESISTANCES ----------------
    #region resistances

    private List<string> resistances = new List<string>();
    private List<string> vulnerabilities = new List<string>();
    private List<string> immunities = new List<string>();
    private List<string> conditions = new List<string>();

    public void AddResistance(string resistance)
    {
        if (!resistances.Contains(resistance))
        {
            resistances.Add(resistance);
        }
    }

    public void AddVulnerability(string vulnerability)
    {
        if (!vulnerabilities.Contains(vulnerability))
        {
            vulnerabilities.Add(vulnerability);
        }
    }

    public void AddCondition(string condition)
    {
        if (!conditions.Contains(condition))
        {
            conditions.Add(condition);
        }
    }

    public void AddImmunity(string immunity)
    {
        if (!immunities.Contains(immunity))
        {
            immunities.Add(immunity);
        }
    }

    public void RemoveResistance(string resistance)
    {
        resistances.Remove(resistance);
    }

    public void RemoveVulnerability(string vulnerability)
    {
        vulnerabilities.Remove(vulnerability);
    }

    public void RemoveCondition(string condition)
    {
        conditions.Remove(condition);
    }

    public void RemoveImmunity(string immunity)
    {
        immunities.Remove(immunity);
    }

    public List<string> GetResistances()
    {
        return resistances;
    }

    public List<string> GetVulnerabilities()
    {
        return vulnerabilities;
    }

    public List<string> GetConditions()
    {
        return conditions;
    }

    public List<string> GetImmunities()
    {
        return immunities;
    }

    // -------- Speed ----------

    private Dictionary<string, int> speeds = new Dictionary<string, int>();

    public void SetSpeed(string name, int value)
    {
        speeds[name] = value;
    }

    public int GetSpeed(string name)
    {
        return speeds[name];
    }
    #endregion resistances

    // ---------- Class --------------
    #region class

    private Dictionary<string, int> classes = new Dictionary<string, int>();

    public void SetClass(string name, int value)
    {
        classes[name] = value;
    }

    public int GetLevel()
    {
        int level = 0;
        foreach (int lvl in classes.Values)
        {
            level += lvl;
        }
        return level;
    }

    public void AddClass(string name)
    {
        classes.Add(name, 0);
    }

    public void RemoveClass(string name)
    {
        classes.Remove(name);
    }

    public List<(string, int)> GetClasses()
    {
        List<(string, int)> returnable = new List<(string, int)>();
        foreach (KeyValuePair<string,int> obj in classes)
        {
            returnable.Add((obj.Key,obj.Value));
        }

        return returnable;
    }

    public int GetClassLevel(string name)
    {
        return classes[name];
    }
    #endregion class

    // ---------- Race ------------------
    #region race
    private string race;

    public string GetRace()
    {
        return basicPC.Race;
    }

    public void SetRace(string name)
    {
        basicPC.Race = name;
    }

    // -------------- TRAITS ----------------
    private List<string> traits { get; set; } = new List<string>();

    public void AddTrait(string trait)
    {
        traits.Add(trait);
    }

    public void RemoveTrait(string trait)
    {
        traits.Remove(trait);
    }

    public void DeleteTraits()
    {
        traits.Clear();
    }


    #endregion race

    // ----------- EQUIPMENT --------------
    #region equipment
    private Dictionary<string,int> equipment = new Dictionary<string, int>();

    public Dictionary<string,int> GetEquipments()
    {
        return equipment;
    }

    public void AddEquipment(string name, int value)
    {
        if (equipment.ContainsKey(name))
        {
            equipment[name] += value;
        } 
        else
        {
            equipment.Add(name, value);
        }
    }

    public void RemoveEquipment(string name)
    {
        equipment.Remove(name);
    }
    #endregion equipment

    // -------------------- OTHERS --------------------
    #region others
    private int proficiencyBonus;

    public int GetProficiencyBonus()
    {
        if (proficiencyBonus == 0)
        {
            proficiencyBonus = GetLevel() / 4 + 1;
        }
        return proficiencyBonus;
    }

    // -------------------- ALIGNMENT --------------------

    private string alignment;

    public void SetAlignment(string name)
    {
        alignment = name;
    }

    public string GetAlignment()
    {
        return alignment;
    }

    // -------------------- BACKGROUND --------------------

    private string background;
    private string personalTraits;
    private string ideals;
    private string bonds;
    private string flaws;

    public string GetBackground()
    {
        return background;
    }

    public void SetBackground(string background)
    {
        this.background = background;
    }

    public string GetPersonalTraits()
    {
        return personalTraits;
    }

    public void SetPersonalTraits(string personalTraits)
    {
        this.personalTraits = personalTraits;
    }

    public string GetIdeals()
    {
        return ideals;
    }

    public void SetIdeals(string ideals)
    {
        this.ideals = ideals;
    }

    public string GetBonds()
    {
        return bonds;
    }

    public void SetBonds(string bonds)
    {
        this.bonds = bonds;
    }

    public string GetFlaws()
    {
        return flaws;
    }

    public void SetFlaws(string flaws)
    {
        this.flaws = flaws;
    }

    // ---------------- Proficiencies ----------------------
    private List<string> proficiencyArmor = new List<string>();
    private List<string> proficiencyWeapons = new List<string>();
    private List<string> proficiencyTools = new List<string>();
    private List<string> proficiencyLanguages = new List<string>();
    private List<string> proficiencyOthers = new List<string>();

    public List<string> GetProficiencyArmor()
    {
        return proficiencyArmor;
    }

    public void AddProficiencyArmor(string prof)
    {
        if (!proficiencyArmor.Contains(prof))
        {
            proficiencyArmor.Add(prof);
        }
    }

    public void RemoveProficiencyArmor(string prof)
    {
        proficiencyArmor.Remove(prof);
    }

    public List<string> GetProficiencyWeapons()
    {
        return proficiencyWeapons;
    }

    public void AddProficiencyWeapons(string prof)
    {
        if (!proficiencyWeapons.Contains(prof))
        {
            proficiencyWeapons.Add(prof);
        }
    }

    public void RemoveProficiencyWeapons(string prof)
    {
        proficiencyWeapons.Remove(prof);
    }

    public List<string> GetProficiencyTools()
    {
        return proficiencyTools;
    }

    public void AddProficiencyTools(string prof)
    {
        if (!proficiencyTools.Contains(prof))
        {
            proficiencyTools.Add(prof);
        }
    }

    public void RemoveProficiencyTools(string prof)
    {
        proficiencyTools.Remove(prof);
    }

    public List<string> GetProficiencyLanguages()
    {
        return proficiencyLanguages;
    }

    public void AddProficiencyLanguages(string prof)
    {
        if (!proficiencyLanguages.Contains(prof))
        {
            proficiencyLanguages.Add(prof);
        }
    }

    public void RemoveProficiencyLanguages(string prof)
    {
        proficiencyLanguages.Remove(prof);
    }

    public List<string> GetProficiencyOthers()
    {
        return proficiencyOthers;
    }

    public void AddProficiencyOthers(string prof)
    {
        if (!proficiencyOthers.Contains(prof))
        {
            proficiencyOthers.Add(prof);
        }
    }

    public void RemoveProficiencyOthers(string prof)
    {
        proficiencyOthers.Remove(prof);
    }
    #endregion others

    public string Name { 
        get {
            return basicPC.Name;
        } 
        set
        {
            basicPC.Name = value;
        } 
    }
}

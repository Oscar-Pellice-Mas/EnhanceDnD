using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPC
{
    public BasicPC()
    {
        Name = "Character Name";
        Classes = new Dictionary<string, int>();
        Subclasses = new Dictionary<string, string>();
        RaceChoices = new List<(string, string)>();
        ClassChoices = new List<(string, string)>();
        Scores = new Dictionary<string, int>();
    }

    public BasicPC(string path)
    {
        Load(path);
    }

    public void Save()
    {
        PC2Save pC2Save = new PC2Save();
        pC2Save.Name = Name;

        string json = JsonUtility.ToJson(pC2Save);
        string outputPath = Application.dataPath + "/Files/Characters/" + this.Name + ".json";
        System.IO.File.WriteAllText(outputPath, json);
    }

    public void Load(string name)
    {
        //string inputPath = Application.persistentDataPath + "/Files/Characters/" + name + ".json";
        string jsonString = System.IO.File.ReadAllText(name);
        PC2Save jsonObject = JsonUtility.FromJson<PC2Save>(jsonString);
        Name = jsonObject.Name;
        /*Sprite = jsonObject.Sprite;
        RolledHP = jsonObject[0].RolledHP;
        Race = jsonObject[0].Race;
        Subrace = jsonObject[0].Subrace;
        RaceChoices = jsonObject[0].RaceChoices;
        Classes = jsonObject[0].Classes;
        Subclasses = jsonObject[0].Subclasses;
        ClassChoices = jsonObject[0].ClassChoices;
        Scores = jsonObject[0].Scores;*/
    }


    // Savable player info
    public string Name { get; set; }
    public Sprite Sprite { get; set; }

    public int RolledHP { get; set; }

    // Race
    #region race
    public string Race { get; set; }
    public string Subrace { get; set; }

    // Elections
    // Languages, Trairs, Ability scores
    public List<(string, string)> RaceChoices { get; set; }
    #region choices
    public void AddRaceChoices(string type, string name)
    {
        RaceChoices.Add((type, name));
    }

    public void RemoveRaceChoices(string type, string name)
    {
        RaceChoices.Remove((type, name));
    }
    #endregion choices
    #endregion

    // Classes
    #region classes
    public Dictionary<string, int> Classes { get; set; }
    #region classes
    public void AddClass(string name, int lvl)
    {
        Classes[name] = lvl;
    }

    public void RemoveClass(string name)
    {
        Classes.Remove(name);
    }
    #endregion classes
    public Dictionary<string, string> Subclasses { get; set; }
    #region subclasses
    public void AddSubclass(string name, string subname)
    {
        Subclasses[name] = subname;
    }

    public void RemoveSubclass(string name)
    {
        Subclasses.Remove(name);
    }
    #endregion

    // Elections
    public List<(string, string)> ClassChoices { get; set; }
    #region choices
    public void AddClassChoices(string type, string name)
    {
        ClassChoices.Add((type, name));
    }

    public void RemoveClassChoices(string type, string name)
    {
        ClassChoices.Remove((type, name));
    }
    #endregion choices
    #endregion classes

    // Scores
    public Dictionary<string,int> Scores { get; set; }
    #region scores
    public void AddScore(string name, int value)
    {
        Scores.Add(name, value);
    }

    public void RemoveScores(string name)
    {
        Scores.Remove(name);
    }
    #endregion scores

    // Background
    // Equipment
    // Characteristics
    

    public static BasicPC GetExample()
    {
        BasicPC basicPC = new BasicPC();
        basicPC.Race = "dwarf";
        basicPC.AddClass("barbarian", 2);

        return basicPC;
    }

    [System.Serializable]
    public class PC2Save
    {
        public string Name;
    }
}
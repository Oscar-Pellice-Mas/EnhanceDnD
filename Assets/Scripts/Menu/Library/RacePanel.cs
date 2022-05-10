using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RacePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text alignment;
    [SerializeField] private TMP_Text subraces;
    [SerializeField] private TMP_Text speed;
    [SerializeField] private TMP_Text abilityBonus;
    [SerializeField] private TMP_Text age;
    [SerializeField] private TMP_Text size;
    [SerializeField] private TMP_Text profs;
    [SerializeField] private TMP_Text languages;
    [SerializeField] private TMP_Text traits;

    public void SetRace(DB.Race race)
    {
        string aux = "";

        // Title
        title.text = "<size=200%>" + race.name;

        // Alignment
        alignment.text = "\n<b>Alignment</b>\n" + race.alignment;

        // Subraces
        aux = "\n<b>Subraces:</b>";
        foreach (string str in race.subraces) aux += str + ", ";
        subraces.text = aux;

        // Speed
        speed.text = "\n<b>Speed:</b> " + race.speed.ToString();

        // Abilities Bonus
        aux = "\n<b>AbilitesBonus:</b> ";
        foreach (DB.AbilityBonus ab in race.ability_bonuses) aux += ab.ability_score + " +" + ab.bonus + ", ";
        aux += "\n";
        foreach (string str in race.ability_bonus_options.from) aux += str + ", ";
        abilityBonus.text = aux;

        // Age
        age.text = "\n<b>Age</b>\n" + race.age;

        // Size
        size.text = race.size_description;

        // Proficiencies
        aux = "\n<b>Proficiencies</b>\n";
        if (race.starting_proficiencies.Count == 0 && race.starting_proficiency_options.from.Count == 0)
        {
            aux += "No proficiencies";
        } 
        else
        {
            foreach (string str in race.starting_proficiencies) aux += str + ", ";
            aux += "\n";
            foreach (string str in race.starting_proficiency_options.from) aux += str + ", ";
        }
        profs.text = aux;

        // Lenguages
        languages.text = "\n<b>Languages</b>\n" + race.language_desc;

        // Traits
        aux = "\n<b>Traits</b>\n";
        if (race.traits.Count == 0)
        {
            aux += "No traits";
        }
        else
        {
            foreach (string str in race.traits) aux += str + ", ";
        }
        traits.text = aux;

        this.GetComponent<RectTransform>().ForceUpdateRectTransforms();
    }
}

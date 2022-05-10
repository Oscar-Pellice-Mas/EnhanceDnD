using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeatPanel : MonoBehaviour
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

    public void SetFeat(DB.Feat feat)
    {
        string aux = "";

        title.text = feat.name;
        /*alignment.text = "\nAlignment\n" + race.alignment;

        aux = "\nSubraces:";
        foreach (string str in race.subraces) aux += str + ", ";
        subraces.text = aux;

        speed.text = "\nSpeed: " + race.speed.ToString();

        aux = "\nAbilitesBonus: ";
        foreach (DB.AbilityBonus ab in race.ability_bonuses) aux += ab.ability_score + " +" + ab.bonus + ", ";
        aux += "\n";
        foreach (string str in race.ability_bonus_options.from) aux += str + ", ";
        abilityBonus.text = aux;

        age.text = "\nRace\n" + race.age;
        size.text = race.size_description;

        aux = "\nProficiencies\n";
        foreach (string str in race.starting_proficiencies) aux += str + ", ";
        aux += "\n";
        foreach (string str in race.starting_proficiency_options.from) aux += str + ", ";
        profs.text = aux;

        languages.text = "\nLanguages\n" + race.language_desc;

        aux = "\nTraits\n";
        foreach (string str in race.traits) aux += str + ", ";
        traits.text = aux;*/

        this.GetComponent<RectTransform>().ForceUpdateRectTransforms();
    }
}

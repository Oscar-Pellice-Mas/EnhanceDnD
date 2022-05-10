using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackgroundPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text profs;
    [SerializeField] private TMP_Text languages;
    [SerializeField] private TMP_Text equipment;
    [SerializeField] private TMP_Text feature;
    [SerializeField] private TMP_Text persoTraits;
    [SerializeField] private TMP_Text ideals;
    [SerializeField] private TMP_Text bonds;
    [SerializeField] private TMP_Text flaws;

    public void SetBackground(DB.Background background)
    {
        string aux = "";

        // Name
        title.text = "<size=200%>" + background.name;

        // Proficiencies
        aux = "\n<b>Proficiencies</b>\n";
        foreach (string str in background.starting_proficiencies) aux += str + ", ";
        profs.text = aux;

        // Languages
        aux = "\n<b>Languages:</b>\n";
        if (background.language_options.choose != 0)
        {
            aux += "Choose " + background.language_options.choose + "from: ";
            foreach (string language in background.language_options.from)
            {
                aux += language + ", ";
            }
        }
        aux = "\n";
        languages.text = aux;

        // Equipment
        aux = "\n<b>Equipment:</b>\n";
        if (background.starting_equipment.Count != 0)
        {
            foreach (DB.StartingEquipment equipment in background.starting_equipment)
            {
                aux += equipment.quantity + "x" + equipment.equipment + ";\n";
            }

            aux += "\nOptions:\n";
            foreach (DB.StartingEquipmentOptionCategory option in background.starting_equipment_options)
            {
                aux += "- Choose " + option.choose + " " + option.fromCategory;
            }
            aux += "\n";
        }
        else
        {
            aux += "No equipment";
        }
        equipment.text = aux;

        //Feature
        aux = "\n<b>Feature:</b>\n";
        aux += background.feature.name;
        foreach (string desc in background.feature.desc)
        {
            aux += desc + "\n\n";
        }
        feature.text = aux;

        //PersoTraits
        aux = "\n<b>Traits</b>\n";
        aux += "Choose " + background.personality_traits.choose + " from:\n";
        foreach (string trait in background.personality_traits.from)
        {
            aux += " - " + trait + "\n";
        }
        persoTraits.text = aux;

        //Ideals
        aux = "\n<b>Ideals</b>\n";
        aux += "Choose " + background.ideals.choose + " from:\n";
        foreach (DB.FromIdeals ideals in background.ideals.fromIdeals)
        {
            aux += " - " + ideals.desc + "\n";
        }
        ideals.text = aux;

        //Bonds
        aux = "\n<b>Bonds</b>\n";
        aux += "Choose " + background.ideals.choose + " from:\n";
        foreach (string bonds in background.bonds.from)
        {
            aux += " - " + bonds + "\n";
        }
        bonds.text = aux;

        //Flaws
        aux = "\n<b>Flaws</b>\n";
        aux += "Choose " + background.flaws.choose + " from:\n";
        foreach (string flaws in background.flaws.from)
        {
            aux += " - " + flaws + "\n";
        }
        flaws.text = aux;

        this.GetComponent<RectTransform>().ForceUpdateRectTransforms();
    }
}

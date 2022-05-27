using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClassPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text proficiencies;
    [SerializeField] private TMP_Text savingThrows;
    [SerializeField] private TMP_Text equipment;
    [SerializeField] private TMP_Text subclasses;
    [SerializeField] private TMP_Text spellcasting;
    [SerializeField] private TMP_Text levels;

    public void SetClass(DB.Class @class)
    {
        string aux = "";

        // Name
        title.text = "<size=200%>" + @class.name;

        // Description
        aux = "\n<b>Description</b>\n";
        aux += @class.desc;
        description.text = aux;

        // Proficiencies
        aux = "\n<b>Proficiencies</b>\n";
        if (@class.proficiencies.Count == 0 && @class.proficiency_choices.Count == 0)
        {
            aux += "No proficiencies";
        }
        else
        {
            foreach (string str in @class.proficiencies) 
                aux += str + ", ";
            aux += "\n\n";
            foreach (DB.ProficiencyChoice pc in @class.proficiency_choices)
            {
                aux += "Choose " + pc.choose + " from: ";
                foreach (string prof in pc.fromProficiencies)
                {
                    aux += prof + ", ";
                }
            }

        }
        proficiencies.text = aux;

        // Saving throws
        aux = "\n<b>Saving throws:</b>\n";
        if (@class.saving_throws.Count != 0)
        {
            foreach (string st in @class.saving_throws)
            {
                aux += st + ", ";
            }
        }
        aux = "\n" + "Hit die: " + @class.hit_die;
        aux = "\n";
        savingThrows.text = aux;

        // Equipment
        aux = "\n<b>Equipment:</b>\n";
        if (@class.starting_equipment.Count != 0)
        {
            foreach (DB.StartingEquipment equipment in @class.starting_equipment)
            {
                aux += equipment.quantity + "x" + equipment.equipment + ";\n";
            }

            aux += "\nOptions:\n";
            foreach (DB.From option in @class.starting_equipment_options)
            {
                aux += "- Choose " + option.choose + " " + option.type + ": ";
                foreach (string equipment in option.from)
                {
                    aux += equipment;
                }
            }
            aux += "\n";
        }
        else
        {
            aux += "No equipment";
        }
        equipment.text = aux;

        //Subclasses
        aux = "\n<b>Subclasses:</b>\n";
        foreach (string desc in @class.subclasses)
        {
            aux += desc + "\n\n";
        }
        subclasses.text = aux;

        //Spellcasting
        if (@class.spellcasting.level != 0)
        {
            aux = "\n<b>Spellcasting</b>\n";
            aux += "\nSpellcasting ability:" + @class.spellcasting.spellcasting_ability + "\n\n";
            foreach(DB.Info info in @class.spellcasting.info)
            {
                aux += info.name + "\n\n";
                foreach(string str in info.desc)
                {
                    aux += str + "\n";
                }
                aux += "\n";
            }
            spellcasting.text = aux;
        }

        // Levels
        aux = "\n<b>Levels</b>\n";
        for (int i = 1; i <= 20; i++)
        {
            DB.Level level = DB.Instance.GetLevel(@class.index + "-" + i);
            if (level != null && level.features.Count != 0)
            {
                aux += "Level " + i + "\n";
                foreach (string str in level.features)
                {
                    aux += str + "\n";
                }
            }
            aux += "\n";
        }
        levels.text = aux;
        

        this.GetComponent<RectTransform>().ForceUpdateRectTransforms();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text spellName;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text footer;
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text school;

    public void SetSpell(DB.Spell spell)
    {
        spellName.text = "<size=200%>" + spell.name;

        string aux = "";
        foreach (string str in spell.desc) aux += str + "\n\n";
        description.text = aux;

        level.text = spell.level.ToString();
        school.text = spell.school.ToString();

        footer.text = spell.range + " - " + spell.duration + " - " + spell.range;

        foreach (string str in spell.components) school.text += " " + str;

        this.GetComponent<RectTransform>().ForceUpdateRectTransforms();
    }
}
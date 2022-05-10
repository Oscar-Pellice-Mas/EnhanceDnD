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
        spellName.text = spell.name;

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


//descriptionObject.SetActive(false);
//RectTransform rt = descriptionObject.GetComponent<RectTransform>();

/*Rect rect = rt.rect;
Debug.Log(description.preferredHeight);
rect.Set(rect.x,rect.y,rect.width,description.preferredHeight);
Debug.Log(rect.height);
Debug.Log(descriptionObject.GetComponent<RectTransform>().rect.height);*/

//rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, description.preferredHeight+50);
//rt.ForceUpdateRectTransforms();
//descriptionObject.SetActive(true);
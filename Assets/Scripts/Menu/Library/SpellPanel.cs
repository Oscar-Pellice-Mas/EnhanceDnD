using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text spellName;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text range;
    [SerializeField] private TMP_Text duration;
    [SerializeField] private TMP_Text castingTime;
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text school;

    [SerializeField] private GameObject descriptionObject;

    public void SetSpell(DB.Spell spell)
    {
        spellName.text = spell.name;

        string aux = "";
        foreach (string str in spell.desc) aux += str + "\n\n";
        description.text = aux;

        descriptionObject.SetActive(false);
        RectTransform rt = descriptionObject.GetComponent<RectTransform>();

        /*Rect rect = rt.rect;
        Debug.Log(description.preferredHeight);
        rect.Set(rect.x,rect.y,rect.width,description.preferredHeight);
        Debug.Log(rect.height);
        Debug.Log(descriptionObject.GetComponent<RectTransform>().rect.height);*/

        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, description.preferredHeight+50);
        rt.ForceUpdateRectTransforms();
        descriptionObject.SetActive(true);

        range.text = spell.range;
        duration.text = spell.duration;
        castingTime.text = spell.range;
        level.text = spell.level.ToString();
        school.text = spell.school.ToString();

        foreach (string str in spell.components) school.text += " " + str;
    }
}

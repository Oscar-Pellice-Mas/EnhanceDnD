using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeatPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text desc;
    [SerializeField] private TMP_Text prerequisites;

    public void SetFeat(DB.Feat feat)
    {
        string aux = "";

        // Name
        title.text = feat.name;

        // Description
        aux = "\n<b>Description</b>\n";
        foreach (var d in feat.desc)
        {
            aux += d + "\n";
        }
        desc.text = aux;

        // Prerequisites
        aux = "\n<b>Description</b>\n";
        foreach (DB.Prerequisite prerequisite in feat.prerequisites)
        {
            if (prerequisite.ability_score != null || prerequisite.minimum_score != 0)
            {
                aux += "Ability score " + prerequisite.ability_score + " min: " + prerequisite.minimum_score;
            }
            aux += "\n";
        }
        prerequisites.text = aux;

        this.GetComponent<RectTransform>().ForceUpdateRectTransforms();
    }
}

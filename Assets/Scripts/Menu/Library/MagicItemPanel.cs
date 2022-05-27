using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MagicItemPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text category;
    [SerializeField] private TMP_Text description;

    public void SetMagicItem(DB.MagicItem magicItem)
    {
        string aux = "";

        // Name
        title.text = "<size=200%>" + magicItem.name;

        // Category
        aux = "\n<b>Category</b>\n";
        aux += magicItem.equipment_category;
        category.text = aux;

        // Description
        aux = "\n<b>Description</b>\n";
        foreach (string str in magicItem.desc)
        {
            aux += str + "\n";
        }
        description.text = aux + "\n";

        this.GetComponent<RectTransform>().ForceUpdateRectTransforms();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClassItem : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text text_HL;

    public void ItemInit(string name)
    {
        DB.Class @class = DB.Instance.GetClass(name);
        //TODO: Icon select -> Global icon select
        text.text = @class.name;
        text_HL.text = @class.name;
    }

    public void ButtonSelected()
    {
        ChEd_Class.Instance.ButtonSelected(this);
        highlight.SetActive(true);
    }

    public void ButtonUnselected()
    {
        highlight.SetActive(false);
    }
}

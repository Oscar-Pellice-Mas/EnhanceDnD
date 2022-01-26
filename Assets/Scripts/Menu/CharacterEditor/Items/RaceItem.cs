using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceItem : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text text_HL;

    public void ItemInit(string name)
    {
        DB.Race race = DB.Instance.GetRace(name);
        //TODO: Icon select -> Global icon select
        text.text = race.name;
        text_HL.text = race.name;
    }

    public void ButtonSelected()
    {
        ChEd_Race.Instance.ButtonSelected(this);
        highlight.SetActive(true);
    }

    public void ButtonUnselected()
    {
        highlight.SetActive(false);
    }
}

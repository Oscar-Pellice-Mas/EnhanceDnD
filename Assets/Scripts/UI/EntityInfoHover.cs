using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityInfoHover : MonoBehaviour
{
    //Entity
    public PlayerCharacter Character { set; get; }
    //Name text
    [SerializeField] private TMP_Text name_txt;
    //HP Text
    [SerializeField] private TMP_Text hp_txt;
    //AC Text
    [SerializeField] private TMP_Text ac_text;
    //Type Text
    [SerializeField] private TMP_Text type_text;

    public void EntityRefresh()
    {
        name_txt.text = Character.basicPC.Name;
        hp_txt.text = Character.basicPC.RolledHP.ToString();
        ac_text.text = Character.GetArmorClass().ToString();
        type_text.text = Character.GetRace();
    }
}
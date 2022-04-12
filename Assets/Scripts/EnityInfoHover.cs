using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnityInfoHover : MonoBehaviour
{
    //Entity
    public PlayerCharacter character { set; get; }
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
        name_txt.text = character.basicPC.Name;
        hp_txt.text = character.basicPC.RolledHP.ToString();
        ac_text.text = character.GetArmorClass().ToString();
        type_text.text = character.GetRace();
    }
}

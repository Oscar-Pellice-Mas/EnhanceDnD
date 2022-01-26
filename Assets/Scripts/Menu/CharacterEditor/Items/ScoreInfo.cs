using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text mainValue;

    public void SetValue(int value)
    {
        // TOTAL SCORE
        mainValue.text = 0.ToString() + "\n";
        // MODIFIER
        mainValue.text += ((value-10)/2).ToString() + "\n";
        // BASE VALUE
        mainValue.text += value.ToString() + "\n";
        // RACIAL BONUS
        mainValue.text += 0.ToString() + "\n";
        // ABILITY IMPRV
        mainValue.text += 0.ToString() + "\n";
        // MISC
        mainValue.text += 0.ToString() + "\n";

    }
}

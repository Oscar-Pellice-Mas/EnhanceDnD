using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChEd_Scores : MonoBehaviour
{
    [SerializeField] private ScoreInfo InfoStr;
    [SerializeField] private ScoreInfo InfoDex;
    [SerializeField] private ScoreInfo InfoCon;
    [SerializeField] private ScoreInfo InfoInt;
    [SerializeField] private ScoreInfo InfoWis;
    [SerializeField] private ScoreInfo InfoCha;

    public void ValidateScore(TMP_InputField input)
    {
        if(int.TryParse(input.text, out int value) == false) return;

        if (value > 20 || value < 0)
        {
            input.text = 0.ToString();
            return;
        }

        switch (input.gameObject.name)
        {
            case "STR":
                ChangeValues(InfoStr, value);
                break;
            case "DEX":
                ChangeValues(InfoDex, value);
                break;
            case "CON":
                ChangeValues(InfoCon, value);
                break;
            case "INT":
                ChangeValues(InfoInt, value);
                break;
            case "WIS":
                ChangeValues(InfoWis, value);
                break;
            case "CHA":
                ChangeValues(InfoCha, value);
                break;
            default:
                return;
        }
    }

    private void ChangeValues(ScoreInfo info, int value)
    {
        info.SetValue(value);
    }
}

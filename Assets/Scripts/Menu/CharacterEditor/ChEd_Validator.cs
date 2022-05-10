using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChEd_Validator : MonoBehaviour
{
    BasicPC basicPC = null;

    [SerializeField] private Button validateButton;
    [SerializeField] private TMP_Text validateText;

    private void Start()
    {
        basicPC = CharacterEditorMenu.Instance.basicPc;

        validateButton.onClick.AddListener(validateCharacter);
    }

    private void OnEnable()
    {
        basicPC = CharacterEditorMenu.Instance.basicPc;
    }

    public void validateCharacter()
    {
        if (basicPC != null)
        {
            validateText.text = "Saving...";
            basicPC.Save();
            MenuManager.Instance.OpenMenu("CharacterMenu");
        } else
        {
            validateText.text = "Error, couldn't save";
        }
    }
}

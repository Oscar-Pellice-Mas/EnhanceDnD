using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterEditorMenu : MonoBehaviour
{
    public static CharacterEditorMenu Instance;

    private string PcName; 
    public BasicPC basicPc;

    [SerializeField] private ChEd_Class chEd_Class;
    [SerializeField] private ChEd_Race chEd_Race;

    [SerializeField] private GameObject mainObject;
    [SerializeField] private GameObject classObject;
    [SerializeField] private GameObject raceObject;
    [SerializeField] private GameObject scoreObject;
    [SerializeField] private GameObject backgroundObject;
    [SerializeField] private GameObject equipmentObject;
    [SerializeField] private GameObject validationObject;

    private GameObject previousObject;

    private void Awake()
    {
        // Instance creation
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        // Pc Init
        if (PcName != null)
        {
            PcName = MenuManager.Instance.Pc_name;
            basicPc = new BasicPC(Application.dataPath + "/Files/Characters/" + PcName);
        } else
        {
            basicPc = new BasicPC();
        }
        
        // Menu Init
        chEd_Class.Init(basicPc);
        chEd_Race.Init(basicPc);

        // Initial Tab
        previousObject = mainObject;
        mainObject.SetActive(true);
        raceObject.SetActive(false);
        classObject.SetActive(false);
        scoreObject.SetActive(false);
        backgroundObject.SetActive(false);
        equipmentObject.SetActive(false);
        validationObject.SetActive(false);
    }

    public void Select(string name)
    {
        switch (name)
        {
            case "main":
                mainObject.SetActive(true);
                if (previousObject != null && previousObject != mainObject) previousObject.SetActive(false);
                previousObject = mainObject;
                break;
            case "class":
                classObject.SetActive(true);
                if (previousObject != null && previousObject != classObject) previousObject.SetActive(false);
                previousObject = classObject;
                break;
            case "race":
                raceObject.SetActive(true);
                if (previousObject != null && previousObject != raceObject) previousObject.SetActive(false);
                previousObject = raceObject;
                break;
            case "score":
                scoreObject.SetActive(true);
                if (previousObject != null && previousObject != scoreObject) previousObject.SetActive(false);
                previousObject = scoreObject;
                break;
            case "background":
                backgroundObject.SetActive(true);
                if (previousObject != null && previousObject != backgroundObject) previousObject.SetActive(false);
                previousObject = backgroundObject;
                break;
            case "equipment":
                equipmentObject.SetActive(true);
                if (previousObject != null && previousObject != equipmentObject) previousObject.SetActive(false);
                previousObject = equipmentObject;
                break;
            case "validation":
                validationObject.SetActive(true);
                if (previousObject != null && previousObject != validationObject) previousObject.SetActive(false);
                previousObject = validationObject;
                break;
            default:
                break;
        }
    }

    public void SetName(TMP_Text input)
    {
        basicPc.Name = input.text;
    }
}

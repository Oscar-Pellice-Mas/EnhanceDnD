using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEditorMenu : MonoBehaviour
{
    private string PcName;
    private BasicPC basicPc;

    [SerializeField] private ChEd_Class chEd_Class;
    [SerializeField] private ChEd_Race chEd_Race;

    [SerializeField] private GameObject classObject;
    [SerializeField] private GameObject raceObject;
    [SerializeField] private GameObject scoreObject;
    [SerializeField] private GameObject backgroundObject;
    [SerializeField] private GameObject equipmentObject;

    private GameObject previousObject;

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
        raceObject.SetActive(true);
        classObject.SetActive(false);
        scoreObject.SetActive(false);
        backgroundObject.SetActive(false);
        equipmentObject.SetActive(false);
    }

    public void Select(string name)
    {
        switch (name)
        {
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
            default:
                break;
        }
    }
}

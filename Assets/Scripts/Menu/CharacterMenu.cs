using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class CharacterMenu : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject emptyButtonPrefab;
    [SerializeField] private GameObject listParent;

    //private string fileRoot = Application.dataPath + "/Files/Pcs.json";
    private List<BasicPC> PCs = new List<BasicPC>();

    // Start is called before the first frame update
    void Start()
    {
        // Llegir totes les fitxes creades com a fitxers
        foreach (string file in Directory.GetFiles(Application.dataPath + "/Files/Characters"))
        {
            PCs.Add(new BasicPC(file));
        }

        for(int i = 0; i < listParent.transform.childCount; i++) Destroy(listParent.transform.GetChild(i).gameObject);

        GameObject go;
        // Crear els prefabs per cada fitxa
        foreach (BasicPC pc in PCs)
        {
            go = Instantiate(buttonPrefab, listParent.transform);
            go.name = pc.Name;
            go.transform.GetChild(1).GetComponent<Image>().sprite = pc.Sprite;
            go.transform.GetChild(2).GetComponent<TMP_Text>().text = pc.Name;
            go.GetComponentInChildren<Button>().onClick.AddListener(OnUseItem);
        }

        // Crear ultima amb simbol de "+"
        go = Instantiate(emptyButtonPrefab, listParent.transform);
        go.name = "Empty";
        go.GetComponentInChildren<Button>().onClick.AddListener(OnUseItem);
        

    }
    
    public void OnUseItem()
    {
        string callingFuncName = new StackFrame(1).GetMethod().Name;
        if (callingFuncName != "Empty") MenuManager.Instance.SetPcName(callingFuncName);
        MenuManager.Instance.OpenMenu("CharacterEditorMenu");
    }
    
}

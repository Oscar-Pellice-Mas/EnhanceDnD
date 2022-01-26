using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChEd_Class : MonoBehaviour
{
    public static ChEd_Class Instance;

    [SerializeField] private GameObject ClassItem;
    [SerializeField] private GameObject parent;

    private ClassItem previousButton;

    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;

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

    public void Init()
    {
        ResetItems();
        foreach (DB.Class @class in DB.Instance.dictionary_Class.Values)
        {
            GameObject go = Instantiate(ClassItem, parent.transform);

            go.transform.SetParent(parent.transform, false);
            go.name = @class.index;

            go.GetComponentInChildren<ClassItem>().ItemInit(@class.index);
            go.GetComponent<Button>().onClick.AddListener(() => { FoucsClass(go); });;
        }
    }

    private void ResetItems()
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ButtonSelected(ClassItem button)
    {
        if (previousButton != null) previousButton.ButtonUnselected();
        previousButton = button;
    }

    private void FoucsClass(GameObject go)
    {
        DB.Class @class = DB.Instance.GetClass(go.name);

        title.text = @class.name;
        description.text = @class.desc;
    }
}

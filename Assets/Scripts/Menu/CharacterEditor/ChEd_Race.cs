using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChEd_Race : MonoBehaviour
{
    public static ChEd_Race Instance;

    [SerializeField] private GameObject RaceItem;
    [SerializeField] private GameObject parent;

    private RaceItem previousButton;

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
        foreach (DB.Race race in DB.Instance.dictionary_Race.Values)
        {
            GameObject go = Instantiate(RaceItem, parent.transform);

            go.transform.SetParent(parent.transform, false);
            go.name = race.index;

            go.GetComponentInChildren<RaceItem>().ItemInit(race.index);
            go.GetComponent<Button>().onClick.AddListener(() => { FoucsRace(go); }); ;
        }
    }

    private void ResetItems()
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ButtonSelected(RaceItem button)
    {
        if (previousButton != null) previousButton.ButtonUnselected();
        previousButton = button;
    }

    private void FoucsRace(GameObject go)
    {
        DB.Race race = DB.Instance.GetRace(go.name);

        title.text = race.name;
        description.text = race.age;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LibraryMenu : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject LibraryItem;

    [SerializeField] private GameObject InfoPanel;

    private string LibraryName;

    public void SelectLibrary(string name)
    {
        LibraryName = name;
        ResetItems();
        switch (name)
        {
            case "backgrounds":
                ShowBackgrounds();
                break;
            case "classes":
                ShowClasses();
                break;
            case "equipment":
                ShowEquipment();
                break;
            case "feats":
                ShowFeats();
                break;
            case "magicItems":
                ShowMagic();
                break;
            case "monsters":
                ShowMonsters();
                break;
            case "races":
                ShowRaces();
                break;
            case "spells":
                ShowSpells();
                break;
            default:
                return;
        }
    }

    private void ResetItems()
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void ShowSpells()
    {
        foreach (DB.Spell spell in DB.Instance.dictionary_Spell.Values)
        {
            GameObject go = Instantiate(LibraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = spell.index;
            go.GetComponentInChildren<TMP_Text>().text = spell.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go);});
        }
    }

    private void ShowRaces()
    {
        foreach (DB.Race race in DB.Instance.dictionary_Race.Values)
        {
            GameObject go = Instantiate(LibraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = race.index;
            go.GetComponentInChildren<TMP_Text>().text = race.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go); });
        }
    }

    private void ShowMonsters()
    {
        /*foreach (DB.Monsters race in DB.Instance.dictionary_Race.Values)
        {
            GameObject go = Instantiate(LibraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = race.index;
            go.GetComponentInChildren<TMP_Text>().text = race.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go);});
        }*/
    }

    private void ShowMagic()
    {
        foreach (DB.MagicItem magicItem in DB.Instance.dictionary_MagicItem.Values)
        {
            GameObject go = Instantiate(LibraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = magicItem.index;
            go.GetComponentInChildren<TMP_Text>().text = magicItem.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go); });
        }
    }

    private void ShowFeats()
    {
        foreach (DB.Feat feat in DB.Instance.dictionary_Feat.Values)
        {
            GameObject go = Instantiate(LibraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = feat.index;
            go.GetComponentInChildren<TMP_Text>().text = feat.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go); });
        }
    }

    private void ShowEquipment()
    {
        foreach (DB.Equipment equipment in DB.Instance.dictionary_Equipment.Values)
        {
            GameObject go = Instantiate(LibraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = equipment.index;
            go.GetComponentInChildren<TMP_Text>().text = equipment.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go); });
        }
    }

    private void ShowClasses()
    {
        foreach (DB.Class @class in DB.Instance.dictionary_Class.Values)
        {
            GameObject go = Instantiate(LibraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = @class.index;
            go.GetComponentInChildren<TMP_Text>().text = @class.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go); });
        }
    }

    private void ShowBackgrounds()
    {
        foreach (DB.Background background in DB.Instance.dictionary_Background.Values)
        {
            GameObject go = Instantiate(LibraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = background.index;
            go.GetComponentInChildren<TMP_Text>().text = background.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go); });
        }
    }

    public void FoucsLibrary(GameObject gameObject)
    {
        string name = gameObject.name;
        InfoPanel.GetComponentInChildren<TMP_Text>().text = name;
        /*switch (LibraryName)
        {
            case "backgrounds":
                InfoBackground(name);
                break;
            case "classes":
                InfoClass(name);
                break;
            case "equipment":
                InfoEquipment(name);
                break;
            case "feats":
                InfoFeat(name);
                break;
            case "magicItems":
                InfoMagic(name);
                break;
            case "monsters":
                InfoMonster(name);
                break;
            case "races":
                InfoRace(name);
                break;
            case "spells":
                InfoSpell(name);
                break;
            default:
                return;
        }*/
    }

    private void InfoSpell(string name)
    {
        throw new NotImplementedException();
    }

    private void InfoRace(string name)
    {
        throw new NotImplementedException();
    }

    private void InfoMonster(string name)
    {
        throw new NotImplementedException();
    }

    private void InfoMagic(string name)
    {
        throw new NotImplementedException();
    }

    private void InfoFeat(string name)
    {
        throw new NotImplementedException();
    }

    private void InfoEquipment(string name)
    {
        throw new NotImplementedException();
    }

    private void InfoClass(string name)
    {
        throw new NotImplementedException();
    }

    private void InfoBackground(string name)
    {
        throw new NotImplementedException();
    }
}

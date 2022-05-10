using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LibraryMenu : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject libraryItem;

    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject scrollRect;

    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private GameObject racePrefab;
    [SerializeField] private GameObject magicItemsPrefab;
    [SerializeField] private GameObject featPrefab;
    [SerializeField] private GameObject equipmentPrefab;
    [SerializeField] private GameObject classPrefab;
    [SerializeField] private GameObject backgroundPrefab;

    [SerializeField] private TMP_InputField pattern;

    private string libraryName;

    public void SelectLibrary(string name)
    {
        libraryName = name;
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
            GameObject go = Instantiate(libraryItem, parent.transform);
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
            GameObject go = Instantiate(libraryItem, parent.transform);
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
            GameObject go = Instantiate(libraryItem, parent.transform);
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
            GameObject go = Instantiate(libraryItem, parent.transform);
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
            GameObject go = Instantiate(libraryItem, parent.transform);
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
            GameObject go = Instantiate(libraryItem, parent.transform);
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
            GameObject go = Instantiate(libraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = background.index;
            go.GetComponentInChildren<TMP_Text>().text = background.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go); });
        }
    }

    public void Search()
    {
        ResetItems();
        switch (libraryName)
        {
            case "backgrounds":
                SearchBackgrounds();
                break;
            case "classes":
                SearchClasses();
                break;
            case "equipment":
                SearchEquipment();
                break;
            case "feats":
                SearchFeats();
                break;
            case "magicItems":
                SearchMagic();
                break;
            case "monsters":
                SearchMonsters();
                break;
            case "races":
                SearchRaces();
                break;
            case "spells":
                SearchSpells();
                break;
            default:
                return;
        }
    }

    private void SearchSpells()
    {
        foreach (DB.Spell spell in DB.Instance.dictionary_Spell.Values)
        {
            if (pattern.text != "" && !spell.name.ToLower().Contains(pattern.text.ToLower())) continue;
            GameObject go = Instantiate(libraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = spell.index;
            go.GetComponentInChildren<TMP_Text>().text = spell.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go); });
        }
    }

    private void SearchRaces()
    {
        foreach (DB.Race race in DB.Instance.dictionary_Race.Values)
        {
            if (pattern.text != "" && !race.name.Contains(pattern.text)) continue;
            GameObject go = Instantiate(libraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = race.index;
            go.GetComponentInChildren<TMP_Text>().text = race.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go); });
        }
    }

    private void SearchMonsters()
    {
        /*foreach (DB.Monsters race in DB.Instance.dictionary_Race.Values)
        {
            if (pattern.text != "" && !spell.name.Contains(pattern.text)) continue;
            GameObject go = Instantiate(LibraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = race.index;
            go.GetComponentInChildren<TMP_Text>().text = race.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go);});
        }*/
    }

    private void SearchMagic()
    {
        foreach (DB.MagicItem magicItem in DB.Instance.dictionary_MagicItem.Values)
        {
            if (pattern.text != "" && !magicItem.name.Contains(pattern.text)) continue; 
            GameObject go = Instantiate(libraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = magicItem.index;
            go.GetComponentInChildren<TMP_Text>().text = magicItem.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go); });
        }
    }

    private void SearchFeats()
    {
        foreach (DB.Feat feat in DB.Instance.dictionary_Feat.Values)
        {
            if (pattern.text != "" && !feat.name.Contains(pattern.text)) continue; 
            GameObject go = Instantiate(libraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = feat.index;
            go.GetComponentInChildren<TMP_Text>().text = feat.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go); });
        }
    }

    private void SearchEquipment()
    {
        foreach (DB.Equipment equipment in DB.Instance.dictionary_Equipment.Values)
        {
            if (pattern.text != "" && !equipment.name.Contains(pattern.text)) continue; 
            GameObject go = Instantiate(libraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = equipment.index;
            go.GetComponentInChildren<TMP_Text>().text = equipment.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go); });
        }
    }

    private void SearchClasses()
    {
        foreach (DB.Class @class in DB.Instance.dictionary_Class.Values)
        {
            if (pattern.text != "" && !@class.name.Contains(pattern.text)) continue; 
            GameObject go = Instantiate(libraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = @class.index;
            go.GetComponentInChildren<TMP_Text>().text = @class.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go); });
        }
    }

    private void SearchBackgrounds()
    {
        foreach (DB.Background background in DB.Instance.dictionary_Background.Values)
        {
            if (pattern.text != "" && !background.name.Contains(pattern.text)) continue; 
            GameObject go = Instantiate(libraryItem, parent.transform);
            go.transform.SetParent(parent.transform, false);
            go.name = background.index;
            go.GetComponentInChildren<TMP_Text>().text = background.name;
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => { FoucsLibrary(go); });
        }
    }

    public void FoucsLibrary(GameObject gameObject)
    {
        foreach(Transform tf in infoPanel.transform)
        {
            Destroy(tf.gameObject);
        }
        string name = gameObject.name;
        switch (libraryName)
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
        }
    }

    private void InfoSpell(string name)
    {
        GameObject gameobject = Instantiate(spellPrefab, infoPanel.transform);
        gameobject.GetComponent<SpellPanel>().SetSpell(DB.Instance.GetSpell(name));
        scrollRect.GetComponent<ScrollRect>().content = gameobject.GetComponent<RectTransform>();
    }

    private void InfoRace(string name)
    {
        GameObject gameobject = Instantiate(racePrefab, infoPanel.transform);
        gameobject.GetComponent<RacePanel>().SetRace(DB.Instance.GetRace(name));
        scrollRect.GetComponent<ScrollRect>().content = gameobject.GetComponent<RectTransform>();
    }

    private void InfoMonster(string name)
    {
        /*GameObject gameobject = Instantiate(monsterPrefab, infoPanel.transform);
        gameobject.GetComponent<MonsterPanel>().SetRace(DB.Instance.GetMonster(name));
        scrollRect.GetComponent<ScrollRect>().content = gameobject.GetComponent<RectTransform>();*/
    }

    private void InfoMagic(string name)
    {
        GameObject gameobject = Instantiate(magicItemsPrefab, infoPanel.transform);
        gameobject.GetComponent<MagicItemPanel>().SetMagicItem(DB.Instance.GetMagicItem(name));
        scrollRect.GetComponent<ScrollRect>().content = gameobject.GetComponent<RectTransform>();
    }

    private void InfoFeat(string name)
    {
        GameObject gameobject = Instantiate(featPrefab, infoPanel.transform);
        gameobject.GetComponent<FeatPanel>().SetFeat(DB.Instance.GetFeat(name));
        scrollRect.GetComponent<ScrollRect>().content = gameobject.GetComponent<RectTransform>();
    }

    private void InfoEquipment(string name)
    {
        GameObject gameobject = Instantiate(equipmentPrefab, infoPanel.transform);
        gameobject.GetComponent<EquipmentPanel>().SetEquipment(DB.Instance.GetEquipment(name));
        scrollRect.GetComponent<ScrollRect>().content = gameobject.GetComponent<RectTransform>();
    }

    private void InfoClass(string name)
    {
        GameObject gameobject = Instantiate(classPrefab, infoPanel.transform);
        gameobject.GetComponent<ClassPanel>().SetClass(DB.Instance.GetClass(name));
        scrollRect.GetComponent<ScrollRect>().content = gameobject.GetComponent<RectTransform>();
    }

    private void InfoBackground(string name)
    {
        GameObject gameobject = Instantiate(backgroundPrefab, infoPanel.transform);
        gameobject.GetComponent<BackgroundPanel>().SetBackground(DB.Instance.GetBackground(name));
        scrollRect.GetComponent<ScrollRect>().content = gameobject.GetComponent<RectTransform>();
    }

    private void CreateNewItem()
    {
        switch (libraryName)
        {
            case "backgrounds":
                EditableBackground();
                break;
            case "classes":
                EditableClass();
                break;
            case "equipment":
                EditableEquipment();
                break;
            case "feats":
                EditableFeat();
                break;
            case "magicItems":
                EditableMagic();
                break;
            case "monsters":
                EditableMonster();
                break;
            case "races":
                EditableRace();
                break;
            case "spells":
                EditableSpell();
                break;
            default:
                return;
        }
    }

    private void EditableSpell()
    {
        infoPanel.GetComponent<SpellPanel>().SetSpell(DB.Instance.GetSpell(name));
    }

    private void EditableRace()
    {
        throw new NotImplementedException();
    }

    private void EditableMonster()
    {
        throw new NotImplementedException();
    }

    private void EditableMagic()
    {
        throw new NotImplementedException();
    }

    private void EditableFeat()
    {
        throw new NotImplementedException();
    }

    private void EditableEquipment()
    {
        throw new NotImplementedException();
    }

    private void EditableClass()
    {
        throw new NotImplementedException();
    }

    private void EditableBackground()
    {
        throw new NotImplementedException();
    }
}

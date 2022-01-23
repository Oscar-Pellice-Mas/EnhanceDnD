using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private Menu[] menus;

    private Dictionary<string, Menu> menuDict;

    private Menu previousMenu;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        menuDict = new Dictionary<string, Menu>();
        foreach (Menu menu in menus) menuDict.Add(menu.name, menu);
        foreach (Menu menu in menus) menu.Close();
        OpenMenu("TitleMenu");
    }

    public void OpenMenu(string menuName)
    {
        Menu menu;
        if (menuDict.TryGetValue(menuName, out menu) == true)
        {
            menu.Open();
        } else 
        {
            return;
        }
        if (previousMenu != null) previousMenu.Close();
        previousMenu = menu;
    }
}

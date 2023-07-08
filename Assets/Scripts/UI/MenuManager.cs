using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public List<Menu> menus;
    public void HideAll()
    {
        foreach (var menu in menus)
        {
            menu.Hide();
        }
    }
}

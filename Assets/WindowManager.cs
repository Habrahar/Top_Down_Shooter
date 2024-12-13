using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    // Событие для открытия окна выбора оружия
    public event Action OnWeaponSelectionOpened;
    public event Action OnQuestWindowOpened;

    public void OpenWeaponSelectionWindow()
    {
        OnWeaponSelectionOpened?.Invoke();
    }

    public void OpenQuestWindow()
    {
        OnQuestWindowOpened?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _menuWindow;
    [SerializeField] private GameObject _levelsWindow;
    [SerializeField] private GameObject _skinsWindow;
    [SerializeField] private GameObject _settingsWindow;
    [SerializeField] private GameObject _planesSkinsWindow;
    [SerializeField] private GameObject _backsSkinsWindow;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void ClickPlayBtn()
    {
        _menuWindow.SetActive(false);
        _levelsWindow.SetActive(true);
    }

    public void ClickCloseLevelsWindow()
    {
        _levelsWindow.SetActive(false);
        _menuWindow.SetActive(true);
    }

    public void ClickSkinsBtn()
    {
        _menuWindow.SetActive(false);
        _skinsWindow.SetActive(true);
    }

    public void ClickCloseSkinsBtn()
    {
        _skinsWindow.SetActive(false);
        _menuWindow.SetActive(true);
    }

    public void ClickSettingsBtn()
    {
        _menuWindow.SetActive(false);
        _settingsWindow.SetActive(true);
    }

    public void ClickCloseSettingsBtn()
    {
        _settingsWindow.SetActive(false);
        _menuWindow.SetActive(true);
    }

    public void OpenPlanes()
    {
        _backsSkinsWindow.SetActive(false);
        _planesSkinsWindow.SetActive(true);
    }

    public void OpenBacks()
    {
        _planesSkinsWindow.SetActive(false);
        _backsSkinsWindow.SetActive(true);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _menuWindow;
    [SerializeField] private GameObject _levelsWindow;
    [SerializeField] private GameObject _skinsWindow;
    [SerializeField] private GameObject _settingsWindow;

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
}
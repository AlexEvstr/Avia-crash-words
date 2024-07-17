using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Collections;

public class WordGameController : MonoBehaviour
{
    [SerializeField] private WordLevel[] _wordLevel;
    [SerializeField] private TMP_Text _currentWord;
    [SerializeField] private GameObject _win;
    private HashSet<int> coloredIndices = new HashSet<int>();
    public static int LevelIndex;

    private void Start()
    {
        LevelIndex = PlayerPrefs.GetInt("levelIndex", 1);
        Debug.Log(LevelIndex);
        _currentWord.text = _wordLevel[LevelIndex - 1].ShowWord();

        GameObject[] letters = _wordLevel[LevelIndex - 1].SetLetterPrefabs();

        foreach (var item in letters)
        {
            Instantiate(item);
        }
    }

    public void UnlockNewLetter()
    {
        string currentLetter = PlayerPrefs.GetString("CurrentLetter", "");
        if (string.IsNullOrEmpty(currentLetter) || currentLetter.Length != 1)
        {
            return;
        }

        string text = StripColorTags(_currentWord.text);
        char letterToFind = currentLetter[0];
        int nextIndexToColor = -1;

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == letterToFind && !coloredIndices.Contains(i))
            {
                nextIndexToColor = i;
                break;
            }
        }

        if (nextIndexToColor != -1)
        {
            coloredIndices.Add(nextIndexToColor);
        }

        UpdateColoredText(text);

        if (coloredIndices.Count == text.Length)
        {
            StartCoroutine(WinBehavior());
        }
    }

    private void UpdateColoredText(string text)
    {
        string coloredText = "";

        for (int i = 0; i < text.Length; i++)
        {
            if (coloredIndices.Contains(i))
            {
                coloredText += $"<color=#FF0200>{text[i]}</color>";
            }
            else
            {
                coloredText += text[i];
            }
        }

        _currentWord.text = coloredText;
    }

    private IEnumerator WinBehavior()
    {
        yield return new WaitForSeconds(0.5f);
        LevelIndex++;
        PlayerPrefs.SetInt("levelIndex", LevelIndex);
        _win.SetActive(true);
        Time.timeScale = 0;
    }

    private string StripColorTags(string input)
    {
        return Regex.Replace(input, "<.*?>", string.Empty);
    }
}
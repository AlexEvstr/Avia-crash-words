using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] private GameObject inputPanel;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_Text _welcomeText;
    [SerializeField] private GameObject _menuWindow;
    private const string PlayerNameKey = "PlayerName";

    private void Start()
    {
        string playerName = PlayerPrefs.GetString(PlayerNameKey);

        if (PlayerPrefs.HasKey(PlayerNameKey))
        {
            _welcomeText.text = "welcome back, " + playerName + "!";
            inputPanel.SetActive(false);
            _menuWindow.SetActive(true);
        }
        else
        {
            _menuWindow.SetActive(false);
            inputPanel.SetActive(true);
        }

        submitButton.onClick.AddListener(OnSubmitButtonClicked);
    }

    private void OnSubmitButtonClicked()
    {
        string playerName = nameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString(PlayerNameKey, playerName);
            PlayerPrefs.Save();
            inputPanel.SetActive(false);
            _menuWindow.SetActive(true);
            _welcomeText.text = "Hello, " + playerName + "!";
        }
        else
        {
            _welcomeText.text = "Player name cannot be empty.";
        }
    }
}

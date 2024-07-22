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
    [SerializeField] private GameObject _board_1;
    [SerializeField] private GameObject _board_2;
    [SerializeField] private GameObject _board_3;
    [SerializeField] private GameObject _board_4;
    private const string PlayerNameKey = "PlayerName";

    private void Start()
    {
        string playerName = PlayerPrefs.GetString(PlayerNameKey);

        if (PlayerPrefs.HasKey(PlayerNameKey))
        {
            _welcomeText.text = "welcome back, " + playerName + "!";
            _menuWindow.SetActive(true);
        }
        else
        {
            _menuWindow.SetActive(false);
            _board_1.SetActive(true);
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

    public void OpenBoard_2()
    {
        _board_1.SetActive(false);
        _board_2.SetActive(true);
    }

    public void OpenBoard_3()
    {
        _board_2.SetActive(false);
        _board_3.SetActive(true);
    }

    public void OpenBoard_4()
    {
        _board_3.SetActive(false);
        _board_4.SetActive(true);
    }

    public void OpenNameWindow()
    {
        _board_4.SetActive(false);
        inputPanel.SetActive(true);
    }
}
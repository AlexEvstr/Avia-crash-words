using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] private GameObject inputPanel;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_Text _welcomeText;
    private const string PlayerNameKey = "PlayerName";

    private void Start()
    {
        string playerName = PlayerPrefs.GetString(PlayerNameKey);
        // Проверка, если имя игрока уже сохранено
        if (PlayerPrefs.HasKey(PlayerNameKey))
        {
            _welcomeText.text = "Welcome back, " + playerName + "!";
            inputPanel.SetActive(false); // Скрыть панель ввода
        }
        else
        {
            inputPanel.SetActive(true); // Показать панель ввода
        }

        // Подписка на событие нажатия кнопки
        submitButton.onClick.AddListener(OnSubmitButtonClicked);
    }

    private void OnSubmitButtonClicked()
    {
        string playerName = nameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString(PlayerNameKey, playerName);
            PlayerPrefs.Save();
            Debug.Log("Player name saved: " + playerName);
            inputPanel.SetActive(false); // Скрыть панель ввода
            _welcomeText.text = "Hello, " + playerName + "!";
        }
        else
        {
            _welcomeText.text = "Player name cannot be empty.";
        }
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject winMenu;
    public GameObject looseMenu;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI keyText;

    public UnityEngine.UI.Button restartButton;
    public UnityEngine.UI.Button restartButton1;

    private void Start() {
        restartButton.onClick.AddListener(RestartLevel);
        restartButton1.onClick.AddListener(RestartLevel);
    }

    private void RestartLevel() {
        SceneManager.LoadScene(0);
    }

    public void ShowWinMenu() {
        winMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowLooseMenu() {
        looseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UpdateHP(string hp) {
        hpText.text = hp;
        
    }

    public void UpdateKeyText() {
        keyText.text = "Ключ найден";
    }
}

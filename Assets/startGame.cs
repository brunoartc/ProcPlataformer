using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class startGame : MonoBehaviour
{

    public Button yourButton;

    public TextMeshProUGUI _highScoreText;

    private int clickConfirm = 0;

    TextMeshProUGUI _buttonText;

    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        _buttonText = FindObjectOfType<TextMeshProUGUI>();
        _highScoreText.SetText($"High Score : { PlayerPrefs.GetInt("highScore")} (+{ PlayerPrefs.GetInt("coins")})");
    }

    void TaskOnClick()
    {
        clickConfirm += 1;
        if (clickConfirm>1)
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
        _buttonText.SetText("Certeza?");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exitGame : MonoBehaviour
{

    public Button yourButton;


    private int clickConfirm = 0;

    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
        Debug.Log("CLICK");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}

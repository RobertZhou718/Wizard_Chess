using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    void Start()
    {
        Button btnMount = GetComponent<Button>();
        //add a listener to ButtonMount, executing TaskOnClick() when click ButtonMount
        btnMount.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if (BoardManager.Instance)
            BoardManager.Instance.restart = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        //BoardManager.Instance.Highlevel = false;
        //BoardManager.Instance.playWithAI = true;
    }
}

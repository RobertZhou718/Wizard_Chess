using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleAI : MonoBehaviour
{
    void Start()
    {
        Button btnMount = GetComponent<Button>();
        //add a listener to ButtonMount, executing TaskOnClick() when click ButtonMount
        btnMount.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Attribute.High_Level = false;
        Attribute.AI = false;
        Attribute.DoubleAI = true;
        Attribute.Restart = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        //BoardManager.Instance.Highlevel = false;
        //BoardManager.Instance.playWithAI = true;
    }
}

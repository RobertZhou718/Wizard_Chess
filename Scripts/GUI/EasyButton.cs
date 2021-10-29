using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyButton : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Button btnMount = GetComponent<Button>();
        //add a listener to ButtonMount, executing TaskOnClick() when click ButtonMount
        btnMount.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Attribute.High_Level = false;
        Attribute.AI = true;
        Attribute.DoubleAI = false;
        Attribute.Restart = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        //BoardManager.Instance.SetEasy();
        //BoardManager.Instance.Highlevel = false;
        //BoardManager.Instance.playWithAI = true;
    }


}

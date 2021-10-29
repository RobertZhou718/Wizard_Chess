using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EXIT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(Exit);
    }

     void Exit()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }
}

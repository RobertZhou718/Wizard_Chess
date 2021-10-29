using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoSetting : MonoBehaviour
{

    public GameObject Settingcanvas;
    public GameObject Menucanvas;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        Button btnMount = button.GetComponent<Button>();
        Menucanvas.SetActive(true);
        Settingcanvas.SetActive(false);
        btnMount.onClick.AddListener(switchscene);
    }
    void switchscene() {
        Menucanvas.SetActive(false);
        Settingcanvas.SetActive(true);
    }
}

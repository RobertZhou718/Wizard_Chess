using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Back : MonoBehaviour
{
    public GameObject Settingcanvas;
    public GameObject Menucanvas;
    public Button button;

    public Slider volumn;
    // Start is called before the first frame update
    void Start()
    {
        Button btnMount = button.GetComponent<Button>();       
        btnMount.onClick.AddListener(switchscene);
    }

    private void Update()
    {
        Attribute.volumn = volumn.value / 100;
    }

    void switchscene()
    {
        Menucanvas.SetActive(true);
        Settingcanvas.SetActive(false);
    }
}

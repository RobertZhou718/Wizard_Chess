using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCam : MonoBehaviour
{
    
    Animator anim;
    float time = 2f;
    public GameObject menu;
   
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.finish == true) {
            anim.SetBool("move", true);
            time -= Time.deltaTime;
            if (time <= 0)
            {
                menu.SetActive(true);
            }
         
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    public Transform meetpoint;
    public Transform Endpoint;
    public Transform lookpoint;
    Animator anim;
    float time = 3f;
    bool end = false;
    public static bool finish = false;
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(end == false) {

            Vector3 dir = meetpoint.position - transform.position;
            transform.Translate(dir.normalized * 1f * Time.deltaTime, Space.World);
            anim.SetBool("walking", true);

            if (Vector3.Distance(transform.position, meetpoint.position) < 0.1f)
            {
                anim.SetBool("walking", false);
                anim.SetBool("attacking", true);
                time -= Time.deltaTime;
                if (time < 0f)
                {
                    end = true;
                }
            }
        }
       
        if (end == true)
        {
            anim.SetBool("attacking", false);
            Vector3 dir2 = transform.position - Endpoint.position; ;
            transform.Translate(-dir2.normalized * 0.8f * Time.deltaTime, Space.World);
            anim.SetBool("walking", true);
            transform.LookAt(Endpoint);
        }
        if (Vector3.Distance(transform.position, Endpoint.position) < 0.1f)
        {
            anim.SetBool("walking", false);
            anim.SetBool("dancing", true);
            transform.LookAt(lookpoint);
            finish = true;
        }
    }
}
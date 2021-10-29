using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject defaltBoard;
    public GameObject target { set; get; }
    public static CameraController Instance { set; get; }

    private bool isFollowing;

    AudioSource bgm;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        transform.position = new Vector3(4, 9, -1);
        transform.LookAt(defaltBoard.transform);
        isFollowing = true;
        bgm = GetComponent<AudioSource>();
        bgm.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget(target);
        bgm.volume = Attribute.volumn;
    }

    private void FollowTarget(GameObject tar)
    {
        if (tar == null)
        {
            transform.LookAt(defaltBoard.transform);
            transform.position = new Vector3(4, 9, -1);
            return;
        }
        if (isFollowing)
        {
            transform.LookAt(tar.transform);
            Vector3 targetPosition = tar.transform.position - tar.transform.forward * 3 + tar.transform.right * 3 + tar.transform.up * 3;

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * Vector3.Distance(transform.position, tar.transform.position) );
            //Camera.main.transform.LookAt(tar.transform);
        }
    }

}

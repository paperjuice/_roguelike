using UnityEngine;
using System.Collections;

public class bilboardGUI : MonoBehaviour {

    private GameObject mainCamera;


    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        transform.LookAt(mainCamera.transform.position );
    }

}

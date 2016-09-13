using UnityEngine;
using System.Collections;

public class trapBehaviour : MonoBehaviour {

    [SerializeField] Animator anim;

	

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "enemy")
        {
            anim.SetTrigger("trigger");
            print("asd");
        }
    }

}

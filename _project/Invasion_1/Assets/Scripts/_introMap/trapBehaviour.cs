using UnityEngine;
using System.Collections;

public class trapBehaviour : MonoBehaviour {

    [SerializeField] Animator anim;

	

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "enemy")
        {
            anim.SetTrigger("trigger");
            col.gameObject.GetComponent<enemySts>().enemyHP = 0f;
            //print("asd");
        }
    }

}

using UnityEngine;
using System.Collections;

public class trapOnAnimationScripts : MonoBehaviour {

    private Animator _camera;

	[SerializeField] GameObject blood_splatter;
	[SerializeField] GameObject blood_on_trap;


    void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "enemy")
        {
            print("yesasd");
            col.gameObject.GetComponent<enemySts>().enemyHP = 0f;
        }
    }


    void OnHitTheWall()
    {
        blood_splatter.gameObject.SetActive(true);
        blood_on_trap.gameObject.SetActive(true);
        _camera.SetTrigger("shake_2");
    }
}

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

   


    void OnHitTheWall()
    {
        blood_splatter.gameObject.SetActive(true);
        blood_on_trap.gameObject.SetActive(true);
        _camera.SetTrigger("shake_2");
    }
}

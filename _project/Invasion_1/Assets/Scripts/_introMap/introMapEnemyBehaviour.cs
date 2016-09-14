using UnityEngine;
using System.Collections;

public class introMapEnemyBehaviour : MonoBehaviour {

    private GameObject player;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        anim.SetTrigger("move");
        if (Vector3.Distance(transform.position, player.transform.position) < 70f)
        {
            transform.position += Time.deltaTime * 30f * transform.forward;
        }
    }
}

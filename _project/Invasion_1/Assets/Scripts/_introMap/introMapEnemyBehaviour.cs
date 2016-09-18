using UnityEngine;
using System.Collections;

public class introMapEnemyBehaviour : MonoBehaviour {

    [SerializeField] private float timeTillDeath;
    private bool isDyingSoon;
    private float currentTime;

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
            isDyingSoon = true;
        }

        if (isDyingSoon)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timeTillDeath)
            {
                GetComponent<enemySts>().enemyHP = 0f;
            }

        }
    }
}

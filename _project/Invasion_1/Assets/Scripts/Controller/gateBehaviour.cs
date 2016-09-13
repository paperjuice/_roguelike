using UnityEngine;
using UnityEngine.SceneManagement;

public class gateBehaviour : MonoBehaviour {


    [SerializeField] GameObject openedDoorLightEffect;

    private Animator anim;
    private GameObject player;
    private bool isOpened;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isOpened)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 15)
            {
                anim.SetTrigger("open");
                openedDoorLightEffect.gameObject.SetActive(true);
                isOpened = true;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == player)
        {
            SceneManager.LoadScene("Game_1");
        }

    }
}

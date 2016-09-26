using UnityEngine;
using System.Collections;

public class bulletGeneralBehaviour : MonoBehaviour {

    
    private Animator _camera;
    private playerStats _playerStats;
    private GameObject[] enemies;
    private GameObject[] props;

    private float dmg;

    [SerializeField] private TextMesh DmgText;
    [SerializeField] private float ms;



    void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();

        enemies = GameObject.FindGameObjectsWithTag("enemy");
        props = GameObject.FindGameObjectsWithTag("prop");
    }

    void Start()
    {
        dmg = _playerStats.CalculatedDmg();
    }

    void Update()
    {
        transform.position += Time.deltaTime * ms * transform.forward;

        EnemyContact();
    }

    void OnTriggerEnter(Collider col)
    {
       
        if (col.gameObject.tag == "prop")
        {
            col.gameObject.GetComponent<propBehaviour>().propIsHit = true;
            _camera.SetTrigger("shake");
            Destroy(gameObject);
        }
    }


    void EnemyContact()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < 2f)
                {
                    enemy.GetComponent<enemySts>().enemyHP -= dmg;
                    _camera.SetTrigger("shake");

                    enemy.GetComponent<enemySts>().isHit = true;

                    DmgText.text = dmg.ToString("N1");
                    Instantiate(DmgText, enemy.transform.position, DmgText.transform.rotation);

                    Destroy(gameObject);
                }
            }
        }

    }
   

}

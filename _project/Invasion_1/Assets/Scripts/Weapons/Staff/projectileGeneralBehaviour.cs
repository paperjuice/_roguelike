using UnityEngine;
using System.Collections;

public class projectileGeneralBehaviour : MonoBehaviour {

    private Animator _camera;
    private Rigidbody rigid;
    private playerStats _playerStats;
    private GameObject[] enemies;

    [SerializeField] private ParticleSystem particlesOnDestroy;
    [SerializeField] private MeshRenderer sphereMesh;
    [SerializeField] private TextMesh DmgText;
    [SerializeField] private float force;

    private float dmg;

    void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");

        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        dmg = _playerStats.CalculatedDmg();

        rigid.AddForce(transform.forward * force * 1000f);
        Destroy(gameObject, 0.8f);
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "enemy")
        {
            col.gameObject.GetComponent<enemySts>().enemyHP -= dmg;
            _camera.SetTrigger("shake");
            particlesOnDestroy.gameObject.SetActive(true);

            col.gameObject.GetComponent<enemySts>().isHit = true;

            DmgText.text = dmg.ToString("N1");
            Instantiate(DmgText, col.gameObject.transform.position, DmgText.transform.rotation);

           // GetComponent<SphereCollider>().enabled = false;
          //  GetComponent<Rigidbody>().isKinematic = true;
            //Destroy(sphereMesh.gameObject);
            Destroy(gameObject,0.9f);
        }

        if (col.gameObject.tag == "prop")
        {
            col.gameObject.GetComponent<propBehaviour>().propIsHit = true;
            _camera.SetTrigger("shake");

            particlesOnDestroy.gameObject.SetActive(true);
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            Destroy(sphereMesh.gameObject);
            Destroy(gameObject, 0.9f);
        }
    }
}

using UnityEngine;
using System.Collections;

public class projectileGeneralBehaviour : MonoBehaviour {

    private Animator _camera;
    private Rigidbody rigid;
    private playerSts playerSts;

    [SerializeField] private ParticleSystem particlesOnDestroy;
    [SerializeField] private MeshRenderer sphereMesh;
    [SerializeField] private TextMesh DmgText;
    [SerializeField] private float force;

    
    void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        playerSts = GameObject.FindGameObjectWithTag("Player").GetComponent<playerSts>();
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rigid.AddForce(transform.forward * force * 1000f);
        Destroy(gameObject, 0.8f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "enemy")
        {
            col.gameObject.GetComponent<enemySts>().enemyHP -= playerSts.CalculatedDmg();
            _camera.SetTrigger("shake");
            particlesOnDestroy.gameObject.SetActive(true);

            col.gameObject.GetComponent<enemySts>().isHit = true;

            DmgText.text = playerSts.CalculatedDmg().ToString("N1");
            Instantiate(DmgText, col.gameObject.transform.position, DmgText.transform.rotation);

            GetComponent<SphereCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            Destroy(sphereMesh.gameObject);
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

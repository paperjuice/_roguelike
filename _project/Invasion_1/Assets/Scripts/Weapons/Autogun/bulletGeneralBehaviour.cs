using UnityEngine;
using System.Collections;

public class bulletGeneralBehaviour : MonoBehaviour {

    private Animator _camera;
    private playerSts playerSts;

    [SerializeField] private TextMesh DmgText;
    [SerializeField] private float ms;



    void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        playerSts = GameObject.FindGameObjectWithTag("Player").GetComponent<playerSts>();
    }

    void Update()
    {
        transform.position += Time.deltaTime * ms * transform.forward;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "enemy")
        {
            col.gameObject.GetComponent<enemySts>().enemyHP -= playerSts.CalculatedDmg(); 
            _camera.SetTrigger("shake");

            col.gameObject.GetComponent<enemySts>().isHit = true;

            DmgText.text = playerSts.CalculatedDmg().ToString("N1");
            Instantiate(DmgText, col.gameObject.transform.position, DmgText.transform.rotation);

            Destroy(gameObject);
        }

        if (col.gameObject.tag == "prop")
        {
            col.gameObject.GetComponent<propBehaviour>().propIsHit = true;
            _camera.SetTrigger("shake");
            Destroy(gameObject);
        }
    }

   

}

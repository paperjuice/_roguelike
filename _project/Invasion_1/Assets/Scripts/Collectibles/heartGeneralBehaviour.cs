using UnityEngine;
using System.Collections;

public class heartGeneralBehaviour : MonoBehaviour {

    [SerializeField]private float rs = 20;

    private Rigidbody rigid;


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rigid.AddForce( new Vector3(Random.Range(-1f,1f),1f, Random.Range(-1f, 1f)) * 8000f);
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rs * Time.deltaTime);
    }
}

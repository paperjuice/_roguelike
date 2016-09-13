using UnityEngine;
using System.Collections;

public class crystalGeneralBehaviour : MonoBehaviour {
    
    [SerializeField] private Material[] mat;
    private MeshRenderer mesh;
    private Rigidbody rigid;
    private int random;

    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        random = Random.Range(0, 5);
        
        mesh.material = mat[random];
        rigid.AddForce(new Vector3(Random.Range(-1f, 1f), 0.5f, Random.Range(-1f, 1f)) * 12000f);
    }
}

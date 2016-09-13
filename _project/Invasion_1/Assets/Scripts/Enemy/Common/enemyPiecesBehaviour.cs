using UnityEngine;
using System.Collections;

public class enemyPiecesBehaviour : MonoBehaviour {

	
    [SerializeField] private GameObject[] pieces;
    [SerializeField] private float force;
    

    IEnumerator Start()
    {
        foreach (GameObject piece in pieces)
        {
            piece.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f,1f),0.3f,Random.Range(-1f, 1f)) * force );
        }



        yield return new WaitForSeconds(3f);

        foreach (GameObject piece in pieces)
        {
            piece.GetComponent<BoxCollider>().enabled = false;
        }
        Destroy(gameObject,1 );
    }

    
}

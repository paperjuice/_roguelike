using UnityEngine;
using System.Collections;

public class propBehaviour : MonoBehaviour {

    [SerializeField] Rigidbody[] props;
    [SerializeField] private float propHp;
    public bool propIsHit;

    
    void Update()
    {
        if (propIsHit)
        {
            propHp--;
            propIsHit = false;
        }

        if (propHp == 0f)
        {
            GetComponent<BoxCollider>().enabled = false;

            foreach (Rigidbody prop in props)
            {
                prop.isKinematic = false;
                prop.AddForce(new Vector3(Random.Range(-1f, 1f), 0.2f, Random.Range(-1f, 1f)) * 30000f);
                propHp = -1f;
            }

        }
    }

}

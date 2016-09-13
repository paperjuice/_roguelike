using UnityEngine;
using System.Collections;

public class enemyMeleeOnTriggerDamage : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<playerSts>().currentPlayerHP -= 1f;
           
        }
    }
}

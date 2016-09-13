using UnityEngine;
using System.Collections;

public class destroyWeaponButton : MonoBehaviour {



    [SerializeField] private GameObject objectToDestroy;
    [SerializeField]private GameObject crystal;
    [SerializeField] private int numberOfCrystals;



    void OnMouseDown()
    {
        Debug.Log("dsadas");
        for (int i = 0; i < numberOfCrystals; i++)
        {
            Instantiate(crystal, transform.position, transform.rotation);
        }
        Destroy(objectToDestroy.gameObject);
    }

    void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}

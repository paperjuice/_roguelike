using UnityEngine;
using System.Collections;

public class equipWeaponButton : MonoBehaviour {

	[SerializeField] private generalWeaponBehaviour generalWeaponBehaviour;
    private GameObject[] weapons;
    

    void OnMouseDown()
    {
        weapons = GameObject.FindGameObjectsWithTag("weapon");
        foreach (GameObject weapon in weapons)
        {
            weapon.GetComponent<generalWeaponBehaviour>().isEquipped = false;
        }
        generalWeaponBehaviour.isEquipped = true;
    }

    void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}

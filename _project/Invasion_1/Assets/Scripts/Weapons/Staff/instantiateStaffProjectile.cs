using UnityEngine;
using System.Collections;

public class instantiateStaffProjectile : MonoBehaviour {

    [SerializeField] private GameObject projectile;
    private GameObject player;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void InstantiateProjectiles()
    {
        Instantiate(projectile,transform.position+=transform.up *2.2f, player.transform.rotation);
    }
}
